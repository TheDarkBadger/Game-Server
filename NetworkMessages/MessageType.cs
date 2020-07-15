using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkMessages
{
    public static class MessageType
    {
        public const byte Handshake = 0;
        public const byte Register = 1;
        public const byte RegisterConfirmed = 2;
        public const byte RegisterError = 3;
        public const byte Login = 4;
        public const byte LoginConfirmed = 5;
        public const byte LoginError = 6;
        public const byte Chat = 7;
        public const byte CharacterList = 8;
        public const byte CharacterInfo = 9;
    }
}
