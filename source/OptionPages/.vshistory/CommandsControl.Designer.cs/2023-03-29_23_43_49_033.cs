using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	partial class CommandsControl
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
			this._gridVisibility = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this._gridVisibility)).BeginInit();
			this.SuspendLayout();
			// 
			// _gridVisibility
			// 
			this._gridVisibility.AllowUserToAddRows = false;
			this._gridVisibility.AllowUserToDeleteRows = false;
			this._gridVisibility.AllowUserToResizeRows = false;
			this._gridVisibility.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._gridVisibility.Location = new System.Drawing.Point(14, 15);
			this._gridVisibility.MultiSelect = false;
			this._gridVisibility.Name = "_gridVisibility";
			this._gridVisibility.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this._gridVisibility.ShowEditingIcon = false;
			this._gridVisibility.Size = new System.Drawing.Size(359, 254);
			this._gridVisibility.TabIndex = 0;
			// 
			// CommandsControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this._gridVisibility);
			this.Name = "CommandsControl";
			this.Size = new System.Drawing.Size(388, 285);
			this.Load += new System.EventHandler(this.CommandsControl_Load);
			((System.ComponentModel.ISupportInitialize)(this._gridVisibility)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DataGridView _gridVisibility;
	}
}
