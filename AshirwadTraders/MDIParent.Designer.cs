﻿namespace AshirwadTraders
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
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profitLossToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.materialsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleAllMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleByItemMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saleByAccountMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stockAvailableMaterialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allPaymentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentByModeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentByAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStripMDIParent.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStripMDIParent
            // 
            this.menuStripMDIParent.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.accountToolStripMenuItem,
            this.transactionToolStripMenuItem,
            this.ledgerToolStripMenuItem,
            this.reportsToolStripMenuItem});
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
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profitLossToolStripMenuItem,
            this.materialsToolStripMenuItem,
            this.paymentsToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // profitLossToolStripMenuItem
            // 
            this.profitLossToolStripMenuItem.Name = "profitLossToolStripMenuItem";
            this.profitLossToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.profitLossToolStripMenuItem.Text = "Profit / Loss";
            this.profitLossToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // materialsToolStripMenuItem
            // 
            this.materialsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saleAllMaterialToolStripMenuItem,
            this.saleByItemMaterialToolStripMenuItem,
            this.saleByAccountMaterialToolStripMenuItem,
            this.stockAvailableMaterialToolStripMenuItem});
            this.materialsToolStripMenuItem.Name = "materialsToolStripMenuItem";
            this.materialsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.materialsToolStripMenuItem.Text = "Materials";
            // 
            // saleAllMaterialToolStripMenuItem
            // 
            this.saleAllMaterialToolStripMenuItem.Name = "saleAllMaterialToolStripMenuItem";
            this.saleAllMaterialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saleAllMaterialToolStripMenuItem.Text = "All Sales";
            this.saleAllMaterialToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // saleByItemMaterialToolStripMenuItem
            // 
            this.saleByItemMaterialToolStripMenuItem.Name = "saleByItemMaterialToolStripMenuItem";
            this.saleByItemMaterialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saleByItemMaterialToolStripMenuItem.Text = "Sales By Item";
            this.saleByItemMaterialToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // saleByAccountMaterialToolStripMenuItem
            // 
            this.saleByAccountMaterialToolStripMenuItem.Name = "saleByAccountMaterialToolStripMenuItem";
            this.saleByAccountMaterialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saleByAccountMaterialToolStripMenuItem.Text = "Sales By Account";
            this.saleByAccountMaterialToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // stockAvailableMaterialToolStripMenuItem
            // 
            this.stockAvailableMaterialToolStripMenuItem.Name = "stockAvailableMaterialToolStripMenuItem";
            this.stockAvailableMaterialToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.stockAvailableMaterialToolStripMenuItem.Text = "Stock Available";
            this.stockAvailableMaterialToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // paymentsToolStripMenuItem
            // 
            this.paymentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allPaymentToolStripMenuItem,
            this.paymentByModeToolStripMenuItem,
            this.paymentByAccountToolStripMenuItem});
            this.paymentsToolStripMenuItem.Name = "paymentsToolStripMenuItem";
            this.paymentsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.paymentsToolStripMenuItem.Text = "Payments";
            // 
            // allPaymentToolStripMenuItem
            // 
            this.allPaymentToolStripMenuItem.Name = "allPaymentToolStripMenuItem";
            this.allPaymentToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.allPaymentToolStripMenuItem.Text = "All Payments";
            this.allPaymentToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // paymentByModeToolStripMenuItem
            // 
            this.paymentByModeToolStripMenuItem.Name = "paymentByModeToolStripMenuItem";
            this.paymentByModeToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.paymentByModeToolStripMenuItem.Text = "Payments By Mode";
            this.paymentByModeToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
            // 
            // paymentByAccountToolStripMenuItem
            // 
            this.paymentByAccountToolStripMenuItem.Name = "paymentByAccountToolStripMenuItem";
            this.paymentByAccountToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.paymentByAccountToolStripMenuItem.Text = "Payments By Account";
            this.paymentByAccountToolStripMenuItem.Click += new System.EventHandler(this.CommonToolStripMenuItem_Clicked);
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
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profitLossToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem materialsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleAllMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleByItemMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saleByAccountMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stockAvailableMaterialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allPaymentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentByModeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem paymentByAccountToolStripMenuItem;
    }
}