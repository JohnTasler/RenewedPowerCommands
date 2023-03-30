using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.PowerCommands.Commands;

namespace Microsoft.PowerCommands.OptionPages
{
	// Token: 0x02000016 RID: 22
	public class GeneralControl : UserControl
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003B1C File Offset: 0x00001D1C
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00003B24 File Offset: 0x00001D24
		public GeneralPage OptionPage
		{
			get
			{
				return this.optionPage;
			}
			set
			{
				this.optionPage = value;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003B2D File Offset: 0x00001D2D
		public GeneralControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003B3C File Offset: 0x00001D3C
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

		// Token: 0x0600008A RID: 138 RVA: 0x00003BA0 File Offset: 0x00001DA0
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

		// Token: 0x0600008B RID: 139 RVA: 0x00003C03 File Offset: 0x00001E03
		private void GeneralControl_Load(object sender, EventArgs e)
		{
			this.chkFormatOnSave.Checked = this.OptionPage.FormatOnSave;
			this.chkRemoveAndSortUsingsOnSave.Checked = this.OptionPage.RemoveAndSortUsingsOnSave;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C31 File Offset: 0x00001E31
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C50 File Offset: 0x00001E50
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

		// Token: 0x0400002B RID: 43
		private GeneralPage optionPage;

		// Token: 0x0400002C RID: 44
		private IContainer components;

		// Token: 0x0400002D RID: 45
		private GroupBox grpGeneral;

		// Token: 0x0400002E RID: 46
		private CheckBox chkRemoveAndSortUsingsOnSave;

		// Token: 0x0400002F RID: 47
		private CheckBox chkFormatOnSave;
	}
}
