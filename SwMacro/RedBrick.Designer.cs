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
            this.tbMainTable = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // tbMainTable
            // 
            this.tbMainTable.AutoScroll = true;
            this.tbMainTable.AutoSize = true;
            this.tbMainTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tbMainTable.ColumnCount = 3;
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.73684F));
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.42105F));
            this.tbMainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.84211F));
            this.tbMainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbMainTable.Location = new System.Drawing.Point(0, 0);
            this.tbMainTable.Name = "tbMainTable";
            this.tbMainTable.RowCount = 3;
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tbMainTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tbMainTable.Size = new System.Drawing.Size(175, 228);
            this.tbMainTable.TabIndex = 2;
            this.tbMainTable.Tag = "";
            // 
            // RedBrick
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(175, 228);
            this.Controls.Add(this.tbMainTable);
            this.Name = "RedBrick";
            this.Text = "redbrick";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tbMainTable;
    }
}