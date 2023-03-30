using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.PowerCommands.Commands;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	partial public class GeneralControl : UserControl
	{
		public GeneralPage OptionPage { get; set; }

		public GeneralControl()
		{
			this.InitializeComponent();
		}

		private void chkFormatOnSave_CheckedChanged(object sender, EventArgs e)
		{
			this.OptionPage.FormatOnSave = this.chkFormatOnSave.Checked;
		}

		private void RemoveAndSortUsingsOnSave_CheckedChanged(object sender, EventArgs e)
		{
			this.OptionPage.RemoveAndSortUsingsOnSave = this.chkRemoveAndSortUsingsOnSave.Checked;
		}

		private void GeneralControl_Load(object sender, EventArgs e)
		{
			this.chkFormatOnSave.Checked = this.OptionPage.FormatOnSave;
			this.chkRemoveAndSortUsingsOnSave.Checked = this.OptionPage.RemoveAndSortUsingsOnSave;
		}
	}
}
