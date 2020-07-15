using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Resources;
using Models;
using Security;
using Utility;
using NetworkMessages;

namespace Server
{
    public static class Server
    {
        public static TcpListener listener { get; private set; }
        public static List<ConnectedClient> connectedClients { get; private set; }
        public static int maxConnected { get; private set; } = 1000;
        private const int port = 3000;

        public static void StartupServer()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, port);
                listener.Start();
                connectedClients = new List<ConnectedClient>();
                
                Task.Run(() => ListenForConnection());
                Task.Run(() => ListenForMessages());

                FormMain.UpdateConnectedCount();

                FormMain.OutputLogAdd("Server started successfully!");
            }
            catch (Exception e)
            {
                listener?.Stop();
                listener = null;
                connectedClients?.ForEach(x => x.stream.Close());
                connectedClients?.Clear();

                FormMain.UpdateConnectedCount();

                FormMain.OutputLogAdd("ERROR: Server statup failed:\n" + e.Message);
            }
        }

        public static void OutputLogAddInvoke(string toAdd)
        {
            FormMain.get.Invoke(new Action(() => FormMain.OutputLogAdd(toAdd)));

        }

        public static void UpdatePlayerCountInvoke()
        {
            FormMain.get.Invoke(new Action(() => FormMain.UpdateConnectedCount()));

        }

        public async static void ListenForConnection()
        {
            while (listener != null)
            {
                if (!listener.Pending())
                    continue;

                TcpClient client = await listener.AcceptTcpClientAsync();
                NetworkStream stream = client.GetStream();
                ConnectedClient newClient = new ConnectedClient(client, stream);
                connectedClients.Add(newClient);
                newClient.Write(MessageType.Handshake, new string[] { "RPGGameServer" });
                OutputLogAddInvoke("Incoming connection!");
            }
        }

        public async static void ListenForMessages()
        {
            while (listener != null)
            {
                for (int i = 0; i < connectedClients.Count; i++)
                {//For each client
                    var client = connectedClients[i];
                    while (client.stream.DataAvailable)
                    {
                        await OnNetworkMessageReceived(client);
                    }

                }
            }
        }

        public async static Task OnNetworkMessageReceived(ConnectedClient client)
        {
            byte messageType = client.reader.ReadByte();
            switch (messageType)
            {
                case MessageType.Handshake:
                    var test = client.reader.ReadString();
                    if (test == "RPGGameClient")
                    {
                        client.CompleteHandshake();
                        UpdatePlayerCountInvoke();
                    }
                    break;
                case MessageType.Login:
                    await Login(client);
                    SendCharacterList(client);
                    break;
                case MessageType.Register:
                    await Register(client);
                    break;
                case MessageType.CharacterList:
                    SendCharacterList(client);
                    break;
            }
        }

        private static string CheckAccountInfoIsValid(string username, string password, string email)
        {
            if (username.Length < 3)
                return ResErrors.RegisterUsernameInvalid;
            if (Passwords.IsPasswordValid(password))
                return ResErrors.RegisterPasswordInvalid;
            if (!IsValidEmail(email))
                return ResErrors.RegisterEmailInvalid;
            return "Valid";
        }

        public static bool IsValidEmail(string source)
        {
            if (string.IsNullOrEmpty(source))
                return false;
            return new System.ComponentModel.DataAnnotations.EmailAddressAttribute().IsValid(source);
        }

        private static async Task Login(ConnectedClient client)
        {
            string loginUsername = client.reader.ReadString();
            string loginSentPassword = client.reader.ReadString();

            var user = await Databases.Accounts.Users.FindAsync(loginUsername);

            //Username checks
            if (user == null)
            {
                client.Write(MessageType.LoginError, new string[] { ResErrors.ErrorUsernameDoesNotExist });
                return;
            }
            if (user.LoggedIn == true)
            {
                client.Write(MessageType.LoginError, new string[] { ResErrors.ErrorUserIsAlreadyLoggedIn });
                return;
            }
            if (!Passwords.Verify(loginSentPassword, user.PasswordHash))
            {
                client.Write(MessageType.LoginError, new string[] { ResErrors.ErrorPasswordIncorrect });
                return;
            }

            client.Write(MessageType.LoginConfirmed, new string[] { });
            client.SetUserName(loginUsername);
            OutputLogAddInvoke($"Player {client.UserName} has logged in!");

            UpdatePlayerCountInvoke();
        }

        private static async Task Register(ConnectedClient client)
        {
            string registerUsername = client.reader.ReadString();
            string registerPassword = client.reader.ReadString();
            string registerEmail = client.reader.ReadString();

            string isValid = CheckAccountInfoIsValid(registerUsername, registerPassword, registerEmail);
            if (isValid != "Valid")
            {
                client.Write(MessageType.RegisterError, new string[] { isValid });
                return;
            }

            string registerSalt = "";
            var registerHashedPassword = Passwords.Hash(registerPassword, out registerSalt);

            Databases.Accounts.Users.Add(new User()
            {
                Username = registerUsername,
                PasswordHash = registerHashedPassword,
                Email = registerEmail,
                Salt = registerSalt,
                CreationDate = DateTime.UtcNow,
                LastLoginDate = DateTime.UtcNow
            });
            await Databases.Accounts.SaveChangesAsync();

            OutputLogAddInvoke($"User account {registerUsername} has been created!");

            client.Write(MessageType.RegisterConfirmed, new string[] { });
        }

        private static void SendCharacterList(ConnectedClient client)
        {
            Character[] characters = Databases.GetCharactersForAccount(client.UserName).ToArray();
            List<string> messages = new List<string>();
            messages.Add(characters.Length.ToString());
            foreach (Character c in characters)
                messages.AddRange(GetCharacterMessagesToSend(c)); 
            client.Write(MessageType.CharacterList, messages.ToArray());
        }

        private static string[] GetCharacterMessagesToSend(Character c)
        {
            List<string> messages = new List<string>();

            messages.Add(c.Name);
            messages.Add(c.Region.DisplayName);
            messages.Add(c.CharLevel.ToString());
            return messages.ToArray();
        }
    }

    public class ConnectedClient
    {
        public TcpClient client { get; private set; }
        public NetworkStream stream { get; private set; }
        public bool HandshakeComplete { get; private set; } = false;
        public string UserName { get; private set; } = "";
        public LoadedPlayer Player { get; private set; }
        public BinaryReader reader { get; private set; }
        private BinaryWriter writer { get; set; }

        public ConnectedClient(TcpClient newClient, NetworkStream newStream)
        {
            client = newClient;
            stream = newStream;
            reader = new BinaryReader(stream, Encoding.UTF8, true);
            writer = new BinaryWriter(stream, Encoding.UTF8, true);
        }

        public void CompleteHandshake()
        {
            HandshakeComplete = true;
        }

        public Character[] GetCharacters()
        {
            return Databases.GetCharactersForAccount(UserName).ToArray();
        }

        public void LoadPlayer(string charName)
        {
            if (Player != null)
            {
                Server.OutputLogAddInvoke($"{UserName}" + ResErrors.PlayerAlreadyLoaded);
                return;
            }
            var characters = GetCharacters();
            foreach (Character c in characters)
            {
                if (charName == c.Name)
                {
                    Player = new LoadedPlayer(this, c, new Vector2(c.PosX, c.PosY));
                    return;
                }
            }
            Server.OutputLogAddInvoke($"({charName})" + ResErrors.UserCharacterDoesNotExist + $"({UserName})");
            
        }

        public void SetUserName(string newUserName)
        {
            if (string.IsNullOrEmpty(UserName))
                UserName = newUserName;
        }

        public bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(UserName);
        }

        public void Write(byte type, string[] toWrite)
        {
            writer.Write(type);
            foreach (string s in toWrite)
            {
                writer.Write(s);
            }
            writer.Flush();
        }

    }

    public class LoadedPlayer
    {
        public ConnectedClient User { get; private set; }
        public Character LoadedCharacter { get; private set; }
        public Vector2 Position { get; private set; }

        public LoadedPlayer(ConnectedClient user, Character character, Vector2 startPos)
        {
            User = user;
            LoadedCharacter = character;
            Position = startPos;
        }
    }

}
