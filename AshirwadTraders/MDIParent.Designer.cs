namespace AshirwadTraders
{
    partial class MDIParent
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
            this.menuStripMDIParent = new System.Windows.Forms.MenuStrip();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transactionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ledgerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMDIParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMDIParent
            // 
            this.menuStripMDIParent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem,
            this.transactionToolStripMenuItem,
            this.ledgerToolStripMenuItem});
            this.menuStripMDIParent.Location = new System.Drawing.Point(0, 0);
            this.menuStripMDIParent.Name = "menuStripMDIParent";
            this.menuStripMDIParent.Size = new System.Drawing.Size(898, 24);
            this.menuStripMDIParent.TabIndex = 1;
            this.menuStripMDIParent.Text = "Select Page Menu";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
            this.accountToolStripMenuItem.Text = "&Account";
            this.accountToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // transactionToolStripMenuItem
            // 
            this.transactionToolStripMenuItem.Name = "transactionToolStripMenuItem";
            this.transactionToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.transactionToolStripMenuItem.Text = "&Transaction";
            this.transactionToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // ledgerToolStripMenuItem
            // 
            this.ledgerToolStripMenuItem.Name = "ledgerToolStripMenuItem";
            this.ledgerToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.ledgerToolStripMenuItem.Text = "&Ledger";
            this.ledgerToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // MDIParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 556);
            this.Controls.Add(this.menuStripMDIParent);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStripMDIParent;
            this.Name = "MDIParent";
            this.Text = "Ashirwad Traders Ltd";
            this.menuStripMDIParent.ResumeLayout(false);
            this.menuStripMDIParent.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStripMDIParent;
        private System.Windows.Forms.ToolStripMenuItem accountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem transactionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ledgerToolStripMenuItem;
    }
}