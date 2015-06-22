namespace redbrick.csproj
{
    partial class RedBrick
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configurationSpecificPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.globalPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.machinePropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.operationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbMainTable = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(697, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configurationSpecificPropertiesToolStripMenuItem,
            this.globalPropertiesToolStripMenuItem,
            this.machinePropertiesToolStripMenuItem,
            this.operationsToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // configurationSpecificPropertiesToolStripMenuItem
            // 
            this.configurationSpecificPropertiesToolStripMenuItem.Checked = true;
            this.configurationSpecificPropertiesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.configurationSpecificPropertiesToolStripMenuItem.Name = "configurationSpecificPropertiesToolStripMenuItem";
            this.configurationSpecificPropertiesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.configurationSpecificPropertiesToolStripMenuItem.Text = "Configuration Specific Properties";
            this.configurationSpecificPropertiesToolStripMenuItem.Click += new System.EventHandler(this.configurationSpecificPropertiesToolStripMenuItem_Click);
            // 
            // globalPropertiesToolStripMenuItem
            // 
            this.globalPropertiesToolStripMenuItem.Checked = true;
            this.globalPropertiesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.globalPropertiesToolStripMenuItem.Name = "globalPropertiesToolStripMenuItem";
            this.globalPropertiesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.globalPropertiesToolStripMenuItem.Text = "Global Properties";
            this.globalPropertiesToolStripMenuItem.Click += new System.EventHandler(this.globalPropertiesToolStripMenuItem_Click);
            // 
            // machinePropertiesToolStripMenuItem
            // 
            this.machinePropertiesToolStripMenuItem.Checked = true;
            this.machinePropertiesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.machinePropertiesToolStripMenuItem.Name = "machinePropertiesToolStripMenuItem";
            this.machinePropertiesToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.machinePropertiesToolStripMenuItem.Text = "Machine Properties";
            this.machinePropertiesToolStripMenuItem.Click += new System.EventHandler(this.machinePropertiesToolStripMenuItem_Click);
            // 
            // operationsToolStripMenuItem
            // 
            this.operationsToolStripMenuItem.Checked = true;
            this.operationsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.operationsToolStripMenuItem.Name = "operationsToolStripMenuItem";
            this.operationsToolStripMenuItem.Size = new System.Drawing.Size(248, 22);
            this.operationsToolStripMenuItem.Text = "Operations";
            this.operationsToolStripMenuItem.Click += new System.EventHandler(this.operationsToolStripMenuItem_Click);
            // 
            // tbMainTable
            // 
            this.tbMainTable.AutoScroll = true;
            this.tbMainTable.AutoSize = true;
            this.tbMainTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbMainTable.ColumnCount = 3;
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tbMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMainTable.Location = new System.Drawing.Point(0, 24);
            this.tbMainTable.Name = "tbMainTable";
            this.tbMainTable.RowCount = 3;
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tbMainTable.Size = new System.Drawing.Size(697, 411);
            this.tbMainTable.TabIndex = 2;
            this.tbMainTable.Tag = "";
            // 
            // RedBrick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 435);
            this.Controls.Add(this.tbMainTable);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "RedBrick";
            this.Text = "redbrick";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RedBrick_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configurationSpecificPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem globalPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem machinePropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem operationsToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tbMainTable;
    }
}