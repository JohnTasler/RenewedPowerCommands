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
			components = new System.ComponentModel.Container();
			this.gridVisibility = new DataGridView();
			((ISupportInitialize)this.gridVisibility).BeginInit();
			base.SuspendLayout();
			this.gridVisibility.AllowUserToAddRows = false;
			this.gridVisibility.AllowUserToDeleteRows = false;
			this.gridVisibility.AllowUserToResizeRows = false;
			this.gridVisibility.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.gridVisibility.Location = new Point(14, 15);
			this.gridVisibility.MultiSelect = false;
			this.gridVisibility.Name = "gridVisibility";
			this.gridVisibility.SelectionMode = DataGridViewSelectionMode.CellSelect;
			this.gridVisibility.ShowEditingIcon = false;
			this.gridVisibility.Size = new Size(359, 254);
			this.gridVisibility.TabIndex = 0;
			this.gridVisibility.CellValueChanged += this.gridVisibility_CellValueChanged;
			this.gridVisibility.MouseLeave += this.gridVisibility_MouseLeave;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.Controls.Add(this.gridVisibility);
			base.Name = "CommandsControl";
			base.Size = new Size(388, 285);
			base.Load += this.CommandsControl_Load;
			((ISupportInitialize)this.gridVisibility).EndInit();
			base.ResumeLayout(false);
		}

		#endregion

		private DataGridView gridVisibility;
	}
}
