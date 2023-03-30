using System.Drawing;
using System.Windows.Forms;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	partial class GeneralControl
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
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

		#endregion

		private GroupBox grpGeneral;
		private CheckBox chkRemoveAndSortUsingsOnSave;
		private CheckBox chkFormatOnSave;
	}
}
