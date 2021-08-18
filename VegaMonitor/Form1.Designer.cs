namespace VegaMonitor
{
    partial class Form1
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
            this.monitorGridView = new System.Windows.Forms.DataGridView();
            this.monitor_label_status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // monitorGridView
            // 
            this.monitorGridView.AllowUserToAddRows = false;
            this.monitorGridView.AllowUserToDeleteRows = false;
            this.monitorGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.monitorGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.monitorGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.monitorGridView.Location = new System.Drawing.Point(12, 28);
            this.monitorGridView.Name = "monitorGridView";
            this.monitorGridView.ReadOnly = true;
            this.monitorGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.monitorGridView.Size = new System.Drawing.Size(623, 640);
            this.monitorGridView.TabIndex = 0;
//            this.monitorGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.monitorGridView_CellContentClick);
            // 
            // monitor_label_status
            // 
            this.monitor_label_status.AutoSize = true;
            this.monitor_label_status.Location = new System.Drawing.Point(12, 9);
            this.monitor_label_status.Name = "monitor_label_status";
            this.monitor_label_status.Size = new System.Drawing.Size(15, 13);
            this.monitor_label_status.TabIndex = 1;
            this.monitor_label_status.Text = "**";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 680);
            this.Controls.Add(this.monitor_label_status);
            this.Controls.Add(this.monitorGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Vega Site Monitor v1.00";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.monitorGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView monitorGridView;
        private System.Windows.Forms.Label monitor_label_status;
    }
}

