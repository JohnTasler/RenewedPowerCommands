using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

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
				return string.Join<int>(";", this._disabledCommands);
			}
			set
			{
				this._disabledCommands = new List<int>();
				string[] array = value.Split(';');
				for (int i = 0; i < array.Length; i++)
				{
					if (int.TryParse(array[i], out int item))
					{
						this._disabledCommands.Add(item);
					}
				}
			}
		}

		public void RemoveDisabledCommand(int cmdId)
		{
			this._disabledCommands.Remove(cmdId);
		}

		public void AddDisabledCommand(int cmdId)
		{
			this._disabledCommands.Add(cmdId);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				this._control = new CommandsControl();
				this._control.Location = new Point(0, 0);
				this._control.OptionPage = this;
				return this._control;
			}
		}

		internal bool IsCommandEnabled(Guid commandGuid, int commandId)
		{
			for (int i = 0; i < this._disabledCommands.Count; i++)
			{
				if (this._disabledCommands[i] == commandId)
				{
					return false;
				}
			}
			return true;
		}

		private CommandsControl _control;

		private List<int> _disabledCommands = new List<int>();

		internal Dictionary<CommandID, string> guidCommandMapper = new Dictionary<CommandID, string>
		{
			{
				new CommandID(new Guid("C4C895C3-F940-424C-B158-2923AE5B7B80"), 10512),
				"Collapse Projects"
			},
			{
				new CommandID(new Guid("453783B0-8DB7-4F1C-B7CE-5319D3915E8E"), 3518),
				"Remove and Sort Usings"
			},
		};
	}
}
