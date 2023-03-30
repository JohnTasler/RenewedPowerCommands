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
			this.grpGeneral = new System.Windows.Forms.GroupBox();
			this.chkRemoveAndSortUsingsOnSave = new System.Windows.Forms.CheckBox();
			this.chkFormatOnSave = new System.Windows.Forms.CheckBox();
			this.grpGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpGeneral
			// 
			this.grpGeneral.Controls.Add(this.chkFormatOnSave);
			this.grpGeneral.Controls.Add(this.chkRemoveAndSortUsingsOnSave);
			this.grpGeneral.Location = new System.Drawing.Point(3, 3);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(379, 89);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			this.grpGeneral.Enter += new System.EventHandler(this.grpGeneral_Enter);
			// 
			// chkRemoveAndSortUsingsOnSave
			// 
			this.chkRemoveAndSortUsingsOnSave.AutoSize = true;
			this.chkRemoveAndSortUsingsOnSave.Location = new System.Drawing.Point(16, 57);
			this.chkRemoveAndSortUsingsOnSave.Name = "chkRemoveAndSortUsingsOnSave";
			this.chkRemoveAndSortUsingsOnSave.Size = new System.Drawing.Size(185, 17);
			this.chkRemoveAndSortUsingsOnSave.TabIndex = 1;
			this.chkRemoveAndSortUsingsOnSave.Text = "&Remove and Sort Usings on save";
			this.chkRemoveAndSortUsingsOnSave.UseVisualStyleBackColor = true;
			// 
			// chkFormatOnSave
			// 
			this.chkFormatOnSave.AutoSize = true;
			this.chkFormatOnSave.Location = new System.Drawing.Point(16, 27);
			this.chkFormatOnSave.Name = "chkFormatOnSave";
			this.chkFormatOnSave.Size = new System.Drawing.Size(149, 17);
			this.chkFormatOnSave.TabIndex = 0;
			this.chkFormatOnSave.Text = "&Format document on save";
			this.chkFormatOnSave.UseVisualStyleBackColor = true;
			// 
			// GeneralControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpGeneral);
			this.Name = "GeneralControl";
			this.Size = new System.Drawing.Size(385, 97);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox grpGeneral;
		private CheckBox chkFormatOnSave;
		private CheckBox chkRemoveAndSortUsingsOnSave;
	}
}
