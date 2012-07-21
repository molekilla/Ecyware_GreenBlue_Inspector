using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.ReportEngine;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for ReportForm.
	/// </summary>
	public class ReportForm : BasePluginForm
	{
//		private CrystalDecisions.Windows.Forms.CrystalReportViewer crViewer;
		private HtmlUnitTestReport reportDataSet;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReportForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		public ReportForm(HtmlUnitTestReport dataSet):this()
		{
			reportDataSet = dataSet;
			ShowReport();
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.crViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.SuspendLayout();
			// 
			// crViewer
			// 
			this.crViewer.ActiveViewIndex = -1;
			this.crViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.crViewer.Location = new System.Drawing.Point(0, 0);
			this.crViewer.Name = "crViewer";
			this.crViewer.ReportSource = null;
			this.crViewer.Size = new System.Drawing.Size(534, 348);
			this.crViewer.TabIndex = 0;
			// 
			// ReportForm
			// 
			this.Controls.Add(this.crViewer);
			this.Name = "ReportForm";
			this.Size = new System.Drawing.Size(534, 348);
			this.ResumeLayout(false);

		}
		#endregion

//		public override void Close()
//		{
//			rpt.Close();
//		}

		/// <summary>
		/// ShowReport displays reports in push mode.
		/// </summary>
		public void ShowReport()
		{
			try
			{
				MainReport report = new MainReport();
				report.SetDataSource(reportDataSet);
				crViewer.DisplayGroupTree=true;
				crViewer.Zoom(3);
				crViewer.ReportSource=report;				
			}
			catch (LoadSaveReportException lse)
			{
				System.Diagnostics.Trace.Write(lse.Message);
			}
			catch (Exception e)
			{
				System.Diagnostics.Trace.Write(e.Message);
			}
		}

	}
}
