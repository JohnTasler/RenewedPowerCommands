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
			this.grpGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpGeneral
			// 
			this.grpGeneral.Controls.Add(this.chkRemoveAndSortUsingsOnSave);
			this.grpGeneral.Location = new System.Drawing.Point(3, 3);
			this.grpGeneral.Name = "grpGeneral";
			this.grpGeneral.Size = new System.Drawing.Size(379, 59);
			this.grpGeneral.TabIndex = 0;
			this.grpGeneral.TabStop = false;
			this.grpGeneral.Text = "General";
			// 
			// chkRemoveAndSortUsingsOnSave
			// 
			this.chkRemoveAndSortUsingsOnSave.AutoSize = true;
			this.chkRemoveAndSortUsingsOnSave.Location = new System.Drawing.Point(16, 27);
			this.chkRemoveAndSortUsingsOnSave.Name = "chkRemoveAndSortUsingsOnSave";
			this.chkRemoveAndSortUsingsOnSave.Size = new System.Drawing.Size(185, 17);
			this.chkRemoveAndSortUsingsOnSave.TabIndex = 1;
			this.chkRemoveAndSortUsingsOnSave.Text = "&Remove and Sort Usings on save";
			this.chkRemoveAndSortUsingsOnSave.UseVisualStyleBackColor = true;
			// 
			// GeneralControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.grpGeneral);
			this.Name = "GeneralControl";
			this.Size = new System.Drawing.Size(385, 67);
			this.grpGeneral.ResumeLayout(false);
			this.grpGeneral.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private GroupBox grpGeneral;
		private CheckBox chkRemoveAndSortUsingsOnSave;
	}
}
