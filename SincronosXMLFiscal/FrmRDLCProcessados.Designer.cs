namespace SincronosXMLFiscal
{
    partial class FrmRDLCProcessados
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataSetRelatorio = new SincronosXMLFiscal.DataSetRelatorio();
            this.DT_ProcessadosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetRelatorio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT_ProcessadosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSetRelatorio";
            reportDataSource2.Value = this.DT_ProcessadosBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SincronosXMLFiscal.RV_Processados.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(649, 290);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSetRelatorio
            // 
            this.DataSetRelatorio.DataSetName = "DataSetRelatorio";
            this.DataSetRelatorio.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DT_ProcessadosBindingSource
            // 
            this.DT_ProcessadosBindingSource.DataMember = "DT_Processados";
            this.DT_ProcessadosBindingSource.DataSource = this.DataSetRelatorio;
            // 
            // FrmRDLCProcessados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(649, 290);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmRDLCProcessados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmRDLCProcessados";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmRDLCProcessados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetRelatorio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT_ProcessadosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource DT_ProcessadosBindingSource;
        private DataSetRelatorio DataSetRelatorio;
    }
}