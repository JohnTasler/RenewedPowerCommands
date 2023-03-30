using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Commands;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("CBB451AE-1E6C-4B4E-9B1C-AEAAF5A63B89")]
	public class CommandsPage : DialogPage
	{
		public string DisabledCommandsStorage
		{
			get
			{
				return string.Join<int>(";", _disabledCommands);
			}
			set
			{
				_disabledCommands = new List<int>();
				string[] array = value.Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					if (int.TryParse(array[i], out int item))
					{
						_disabledCommands.Add(item);
					}
				}
			}
		}

		public void RemoveDisabledCommand(int cmdId)
		{
			_disabledCommands.Remove(cmdId);
		}

		public void AddDisabledCommand(int cmdId)
		{
			_disabledCommands.Add(cmdId);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				_control = new CommandsControl();
				_control.Location = new Point(0, 0);
				_control.OptionPage = this;
				return _control;
			}
		}

		internal bool IsCommandEnabled(Guid commandGuid, int commandId)
		{
			for (int i = 0; i < _disabledCommands.Count; i++)
			{
				if (_disabledCommands[i] == commandId)
				{
					return false;
				}
			}
			return true;
		}

		private CommandsControl _control;

		private List<int> _disabledCommands = new List<int>();

		internal Dictionary<CommandID, string> _guidCommandMapper = new Dictionary<CommandID, string>
		{
			{
				new CommandID(typeof(CollapseProjectsCommand).GUID, CollapseProjectsCommand.c_cmdidCollapseProjectsCommand),
					"Collapse Projects"
			},
			{
				new CommandID(typeof(RemoveSortUsingsCommand).GUID, RemoveSortUsingsCommand.c_cmdidRemoveSortUsingsCommand),
					"Remove and Sort Usings"
			},
		};
	}
}
