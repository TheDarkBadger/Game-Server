namespace Server
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbLog = new System.Windows.Forms.ListBox();
            this.gbStats = new System.Windows.Forms.GroupBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblClients = new System.Windows.Forms.Label();
            this.lblPlayers = new System.Windows.Forms.Label();
            this.gbRegion = new System.Windows.Forms.GroupBox();
            this.lblRegionNpcs = new System.Windows.Forms.Label();
            this.lblRegionMobs = new System.Windows.Forms.Label();
            this.lblRegionPlayers = new System.Windows.Forms.Label();
            this.lbRegionSelector = new System.Windows.Forms.ListBox();
            this.gbStats.SuspendLayout();
            this.gbRegion.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLog
            // 
            this.lbLog.FormattingEnabled = true;
            this.lbLog.ItemHeight = 16;
            this.lbLog.Location = new System.Drawing.Point(13, 13);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(1032, 260);
            this.lbLog.TabIndex = 0;
            // 
            // gbStats
            // 
            this.gbStats.Controls.Add(this.lblDate);
            this.gbStats.Controls.Add(this.lblClients);
            this.gbStats.Controls.Add(this.lblPlayers);
            this.gbStats.Location = new System.Drawing.Point(1051, 13);
            this.gbStats.Name = "gbStats";
            this.gbStats.Size = new System.Drawing.Size(177, 564);
            this.gbStats.TabIndex = 1;
            this.gbStats.TabStop = false;
            this.gbStats.Text = "Stats";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(6, 52);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(101, 17);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "1E 00/00/0000";
            // 
            // lblClients
            // 
            this.lblClients.AutoSize = true;
            this.lblClients.Location = new System.Drawing.Point(6, 18);
            this.lblClients.Name = "lblClients";
            this.lblClients.Size = new System.Drawing.Size(126, 17);
            this.lblClients.TabIndex = 1;
            this.lblClients.Text = "Clients: 1000/1000";
            // 
            // lblPlayers
            // 
            this.lblPlayers.AutoSize = true;
            this.lblPlayers.Location = new System.Drawing.Point(6, 35);
            this.lblPlayers.Name = "lblPlayers";
            this.lblPlayers.Size = new System.Drawing.Size(131, 17);
            this.lblPlayers.TabIndex = 0;
            this.lblPlayers.Text = "Players: 1000/1000";
            // 
            // gbRegion
            // 
            this.gbRegion.Controls.Add(this.lblRegionNpcs);
            this.gbRegion.Controls.Add(this.lblRegionMobs);
            this.gbRegion.Controls.Add(this.lblRegionPlayers);
            this.gbRegion.Controls.Add(this.lbRegionSelector);
            this.gbRegion.Location = new System.Drawing.Point(13, 383);
            this.gbRegion.Name = "gbRegion";
            this.gbRegion.Size = new System.Drawing.Size(1032, 195);
            this.gbRegion.TabIndex = 2;
            this.gbRegion.TabStop = false;
            this.gbRegion.Text = "Region Info";
            // 
            // lblRegionNpcs
            // 
            this.lblRegionNpcs.AutoSize = true;
            this.lblRegionNpcs.Location = new System.Drawing.Point(160, 55);
            this.lblRegionNpcs.Name = "lblRegionNpcs";
            this.lblRegionNpcs.Size = new System.Drawing.Size(142, 17);
            this.lblRegionNpcs.TabIndex = 3;
            this.lblRegionNpcs.Text = "NPCs in region: 1000";
            // 
            // lblRegionMobs
            // 
            this.lblRegionMobs.AutoSize = true;
            this.lblRegionMobs.Location = new System.Drawing.Point(160, 38);
            this.lblRegionMobs.Name = "lblRegionMobs";
            this.lblRegionMobs.Size = new System.Drawing.Size(141, 17);
            this.lblRegionMobs.TabIndex = 2;
            this.lblRegionMobs.Text = "Mobs in region: 1000";
            // 
            // lblRegionPlayers
            // 
            this.lblRegionPlayers.AutoSize = true;
            this.lblRegionPlayers.Location = new System.Drawing.Point(160, 21);
            this.lblRegionPlayers.Name = "lblRegionPlayers";
            this.lblRegionPlayers.Size = new System.Drawing.Size(154, 17);
            this.lblRegionPlayers.TabIndex = 1;
            this.lblRegionPlayers.Text = "Players in region: 1000";
            // 
            // lbRegionSelector
            // 
            this.lbRegionSelector.FormattingEnabled = true;
            this.lbRegionSelector.ItemHeight = 16;
            this.lbRegionSelector.Location = new System.Drawing.Point(6, 21);
            this.lbRegionSelector.Name = "lbRegionSelector";
            this.lbRegionSelector.Size = new System.Drawing.Size(147, 164);
            this.lbRegionSelector.TabIndex = 0;
            this.lbRegionSelector.SelectedIndexChanged += new System.EventHandler(this.LbRegionSelector_SelectedIndexChanged);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 590);
            this.Controls.Add(this.gbRegion);
            this.Controls.Add(this.gbStats);
            this.Controls.Add(this.lbLog);
            this.Name = "FormMain";
            this.Text = "MMO Server";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.gbStats.ResumeLayout(false);
            this.gbStats.PerformLayout();
            this.gbRegion.ResumeLayout(false);
            this.gbRegion.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbLog;
        private System.Windows.Forms.GroupBox gbStats;
        private System.Windows.Forms.Label lblPlayers;
        private System.Windows.Forms.Label lblClients;
        private System.Windows.Forms.GroupBox gbRegion;
        private System.Windows.Forms.ListBox lbRegionSelector;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblRegionNpcs;
        private System.Windows.Forms.Label lblRegionMobs;
        private System.Windows.Forms.Label lblRegionPlayers;
    }
}

