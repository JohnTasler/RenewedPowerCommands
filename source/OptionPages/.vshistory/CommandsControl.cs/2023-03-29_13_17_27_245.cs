using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.PowerCommands.Commands;
using Microsoft.PowerCommands.Extensions;
using Microsoft.PowerCommands.Services;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PowerCommands.OptionPages
{
	// Token: 0x02000013 RID: 19
	public class CommandsControl : UserControl
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000032D4 File Offset: 0x000014D4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x000032DC File Offset: 0x000014DC
		public CommandsPage OptionPage
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

		// Token: 0x06000072 RID: 114 RVA: 0x000032E5 File Offset: 0x000014E5
		public CommandsControl()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000032F4 File Offset: 0x000014F4
		private void CommandsControl_Load(object sender, EventArgs e)
		{
			this.commandManagerService = this.optionPage.Site.GetService<SCommandManagerService, ICommandManagerService>();
			this.items = (from command in this.commandManagerService.GetRegisteredCommands()
			orderby command.GetType().Name
			select new RowItem
			{
				Command = command.CommandID,
				CommandText = CommandsControl.GetDisplayName(command.GetType()),
				Enabled = this.OptionPage.IsCommandEnabled(command.CommandID.Guid, command.CommandID.ID)
			}).ToList<RowItem>();
			this.gridVisibility.DataSource = this.items;
			this.gridVisibility.Columns[0].Width = 200;
			this.gridVisibility.Columns[0].ReadOnly = true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000033A8 File Offset: 0x000015A8
		private void gridVisibility_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				RowItem rowItem = this.gridVisibility.CurrentRow.DataBoundItem as RowItem;
				this.optionPage.RemoveDisabledCommand(rowItem.Command.ID);
				if (!rowItem.Enabled)
				{
					this.optionPage.AddDisabledCommand(rowItem.Command.ID);
				}
				DynamicCommand.TelemetrySession.PostEvent("VS/PPT-PowerCommands/OpitonChanged", new object[]
				{
					"VS.PPT-PowerCommands.OpitonChanged.OptionName",
					rowItem.CommandText,
					"VS.PPT-PowerCommands.OpitonChanged.OptionValue",
					rowItem.Enabled
				});
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003447 File Offset: 0x00001647
		private void gridVisibility_MouseLeave(object sender, EventArgs e)
		{
			this.gridVisibility.EndEdit();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003458 File Offset: 0x00001658
		internal static string GetDisplayName(Type command)
		{
			string result = string.Empty;
			DisplayNameAttribute displayNameAttribute = TypeDescriptor.GetAttributes(command).OfType<DisplayNameAttribute>().FirstOrDefault<DisplayNameAttribute>();
			if (displayNameAttribute != null)
			{
				result = displayNameAttribute.DisplayName;
			}
			return result;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003487 File Offset: 0x00001687
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034A8 File Offset: 0x000016A8
		private void InitializeComponent()
		{
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

		// Token: 0x04000022 RID: 34
		private ICommandManagerService commandManagerService;

		// Token: 0x04000023 RID: 35
		private IList<RowItem> items;

		// Token: 0x04000024 RID: 36
		private CommandsPage optionPage;

		// Token: 0x04000025 RID: 37
		private IContainer components;

		// Token: 0x04000026 RID: 38
		private DataGridView gridVisibility;
	}
}
