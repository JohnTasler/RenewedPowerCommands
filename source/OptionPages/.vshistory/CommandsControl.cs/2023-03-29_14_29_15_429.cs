using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//using Microsoft.PowerCommands.Commands;
//using Microsoft.PowerCommands.Extensions;
using Tasler.RenewedPowerCommands.Services;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	public partial class CommandsControl : UserControl
	{
		public CommandsPage OptionPage { get; set; }

		public CommandsControl()
		{
			this.InitializeComponent();
		}

		private void CommandsControl_Load(object sender, EventArgs e)
		{
			this.commandManagerService = this.OptionPage.Site.GetService<SCommandManagerService, ICommandManagerService>();
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

		private void gridVisibility_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				RowItem rowItem = this.gridVisibility.CurrentRow.DataBoundItem as RowItem;
				this.OptionPage.RemoveDisabledCommand(rowItem.Command.ID);
				if (!rowItem.Enabled)
				{
					this.OptionPage.AddDisabledCommand(rowItem.Command.ID);
				}
			}
		}

		private void gridVisibility_MouseLeave(object sender, EventArgs e)
		{
			this.gridVisibility.EndEdit();
		}

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

		private ICommandManagerService commandManagerService;
		private IList<RowItem> items;

	}
}
