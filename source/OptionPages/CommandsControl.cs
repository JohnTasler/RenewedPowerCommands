using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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
			_commandManagerService = this.OptionPage.Site.GetService<SCommandManagerService, ICommandManagerService>();
			_items = (from command in _commandManagerService.GetRegisteredCommands()
				orderby command.GetType().Name
				select new RowItem
				{
					Command = command.CommandID,
					CommandText = CommandsControl.GetDisplayName(command.GetType()),
					Enabled = this.OptionPage.IsCommandEnabled(command.CommandID.Guid, command.CommandID.ID)
				}).ToList<RowItem>();

			_gridVisibility.DataSource = _items;
			_gridVisibility.Columns[0].Width = 200;
			_gridVisibility.Columns[0].ReadOnly = true;
		}

		private void gridVisibility_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 1)
			{
				RowItem rowItem = _gridVisibility.CurrentRow.DataBoundItem as RowItem;
				this.OptionPage.RemoveDisabledCommand(rowItem.Command.ID);
				if (!rowItem.Enabled)
				{
					this.OptionPage.AddDisabledCommand(rowItem.Command.ID);
				}
			}
		}

		private void gridVisibility_MouseLeave(object sender, EventArgs e)
		{
			_gridVisibility.EndEdit();
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

		private ICommandManagerService _commandManagerService;
		private IList<RowItem> _items;
	}
}