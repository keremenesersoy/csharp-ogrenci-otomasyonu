
namespace SQL_Proje
{
    partial class raporgoster
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
            this.denemeDataSet1 = new SQL_Proje.denemeDataSet();
            this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.raporSinavNot1 = new SQL_Proje.RaporSinavNot();
            ((System.ComponentModel.ISupportInitialize)(this.denemeDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // denemeDataSet1
            // 
            this.denemeDataSet1.DataSetName = "denemeDataSet";
            this.denemeDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // crystalReportViewer1
            // 
            this.crystalReportViewer1.ActiveViewIndex = -1;
            this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalReportViewer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.crystalReportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
            this.crystalReportViewer1.Name = "crystalReportViewer1";
            this.crystalReportViewer1.Size = new System.Drawing.Size(849, 533);
            this.crystalReportViewer1.TabIndex = 0;
            // 
            // raporgoster
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 533);
            this.Controls.Add(this.crystalReportViewer1);
            this.Name = "raporgoster";
            this.Text = "raporgoster";
            this.Load += new System.EventHandler(this.raporgoster_Load);
            ((System.ComponentModel.ISupportInitialize)(this.denemeDataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private denemeDataSet denemeDataSet1;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
        private RaporSinavNot raporSinavNot1;
    }
}