using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Regions;

namespace Server
{
    public partial class FormMain : Form
    {
        public static FormMain get;
        public FormMain()
        {
            InitializeComponent();
            get = this;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Server.StartupServer();
            UpdateRegionsList();
            lbRegionSelector.SetSelected(0, true);
        }

        public static void OutputLogAdd(string toAdd)
        {
            get.lbLog.Items.Add(toAdd);
        }

        public static void UpdateConnectedCount()
        {
            get.lblClients.Text = $"Clients: {Server.connectedClients?.Count ?? 0}/{Server.maxConnected}";
            get.lblPlayers.Text = $"Players: {(from c in Server.connectedClients where c.IsLoggedIn() select c).Count()}/{Server.maxConnected}";
        }

        public static void UpdateRegionsList()
        {
            get.lbRegionSelector.Items.Clear();

            foreach (var s in RegionControl.LoadedRegions)
            {
                if (s.ActiveMobs.Count == 0)
                {
                    get.lbRegionSelector.Items.Add(s.Model.Name);
                }
            }
        }

        public void UpdateRegionInfo(Region r)
        {
            lblRegionPlayers.Text = "Players in region: " + r.ActiveMobs.Count;
            lblRegionMobs.Text =  "Mobs in region: " + r.ActiveMobs.Count;
            lblRegionNpcs.Text =  "NPCs in region: " + r.ActiveMobs.Count;
        }

        private void LbRegionSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateRegionInfo(RegionControl.FindRegionByName(lbRegionSelector.Items[0].ToString()));
        }
    }
}
