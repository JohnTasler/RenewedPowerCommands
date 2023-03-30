using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.PowerCommands.Commands;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	public class GeneralControl : UserControl
	{
		public GeneralPage OptionPage { get; set; }

		public GeneralControl()
		{
			this.InitializeComponent();
		}

		private void chkFormatOnSave_CheckedChanged(object sender, EventArgs e)
		{
			this.OptionPage.FormatOnSave = this.chkFormatOnSave.Checked;
			DynamicCommand.TelemetrySession.PostEvent("VS/PPT-PowerCommands/OpitonChanged", new object[]
			{
				"VS.PPT-PowerCommands.OpitonChanged.OptionName",
				"Check Format on Save",
				"VS.PPT-PowerCommands.OpitonChanged.OptionValue",
				this.chkFormatOnSave.Checked
			});
		}

		private void RemoveAndSortUsingsOnSave_CheckedChanged(object sender, EventArgs e)
		{
			this.OptionPage.RemoveAndSortUsingsOnSave = this.chkRemoveAndSortUsingsOnSave.Checked;
			DynamicCommand.TelemetrySession.PostEvent("VS/PPT-PowerCommands/OpitonChanged", new object[]
			{
				"VS.PPT-PowerCommands.OpitonChanged.OptionName",
				"Remove and Sort Usings on Save",
				"VS.PPT-PowerCommands.OpitonChanged.OptionValue",
				this.chkRemoveAndSortUsingsOnSave.Checked
			});
		}

		private void GeneralControl_Load(object sender, EventArgs e)
		{
			this.chkFormatOnSave.Checked = this.OptionPage.FormatOnSave;
			this.chkRemoveAndSortUsingsOnSave.Checked = this.OptionPage.RemoveAndSortUsingsOnSave;
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.grpGeneral = new GroupBox();
			this.chkRemoveAndSortUsingsOnSave = new CheckBox();
			this.chkFormatOnSave = new CheckBox();
			this.grpGeneral.SuspendLayout();
			base.SuspendLayout();
			this.grpGeneral.Controls.Add(this.chkRemoveAndSortUsingsOnSave);
			this.grpGeneral.Controls.Add(this.chkFormatOnSave);
			this.grpGeneral.Location = new Point(3, 3);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new Size(379, 89);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			this.chkRemoveAndSortUsingsOnSave.AutoSize = true;
			this.chkRemoveAndSortUsingsOnSave.Location = new Point(16, 57);
			this.chkRemoveAndSortUsingsOnSave.Name = "chkRemoveAndSortUsingsOnSave";
			this.chkRemoveAndSortUsingsOnSave.Size = new Size(185, 17);
			this.chkRemoveAndSortUsingsOnSave.TabIndex = 1;
			this.chkRemoveAndSortUsingsOnSave.Text = "Remove and Sort Usings on save";
			this.chkRemoveAndSortUsingsOnSave.UseVisualStyleBackColor = true;
			this.chkRemoveAndSortUsingsOnSave.CheckedChanged += this.RemoveAndSortUsingsOnSave_CheckedChanged;
			this.chkFormatOnSave.AutoSize = true;
			this.chkFormatOnSave.Location = new Point(16, 27);
			this.chkFormatOnSave.Name = "chkFormatOnSave";
			this.chkFormatOnSave.Size = new Size(149, 17);
			this.chkFormatOnSave.TabIndex = 0;
			this.chkFormatOnSave.Text = "Format document on save";
			this.chkFormatOnSave.UseVisualStyleBackColor = true;
			this.chkFormatOnSave.CheckedChanged += this.chkFormatOnSave_CheckedChanged;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.grpGeneral);
			base.Name = "GeneralControl";
			base.Size = new Size(385, 97);
			base.Load += this.GeneralControl_Load;
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			base.ResumeLayout(false);
		}

		private GeneralPage optionPage;

		private IContainer components;

		private GroupBox grpGeneral;

		private CheckBox chkRemoveAndSortUsingsOnSave;

		private CheckBox chkFormatOnSave;
	}
}
