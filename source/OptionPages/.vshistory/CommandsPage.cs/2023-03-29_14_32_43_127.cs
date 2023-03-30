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
				return string.Join<int>(";", this.disabledCommands);
			}
			set
			{
				this.disabledCommands = new List<int>();
				string[] array = value.Split(new char[]
				{
					';'
				});
				for (int i = 0; i < array.Length; i++)
				{
					int item;
					if (int.TryParse(array[i], out item))
					{
						this.disabledCommands.Add(item);
					}
				}
			}
		}

		public void RemoveDisabledCommand(int cmdId)
		{
			this.disabledCommands.Remove(cmdId);
		}

		public void AddDisabledCommand(int cmdId)
		{
			this.disabledCommands.Add(cmdId);
		}

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				this.control = new CommandsControl();
				this.control.Location = new Point(0, 0);
				this.control.OptionPage = this;
				return this.control;
			}
		}

		internal bool IsCommandEnabled(Guid commandGuid, int commandId)
		{
			for (int i = 0; i < this.disabledCommands.Count; i++)
			{
				if (this.disabledCommands[i] == commandId)
				{
					return false;
				}
			}
			return true;
		}

		private CommandsControl control;

		private List<int> disabledCommands = new List<int>();

		internal Dictionary<CommandID, string> guidCommandMapper = new Dictionary<CommandID, string>
		{
			{
				new CommandID(new Guid("8093C326-9C55-4ACC-96F4-B21525333D10"), 11858),
				"Clear All Panes"
			},
			{
				new CommandID(new Guid("5DC1F44A-F045-4E82-9A6A-D576BD672DB3"), 3965),
				"Clear Recent File List"
			},
			{
				new CommandID(new Guid("63D8DB72-0E23-4950-8E30-680BEFC80BAD"), 3952),
				"Clear Recent Project List"
			},
			{
				new CommandID(new Guid("C4C895C3-F940-424C-B158-2923AE5B7B80"), 10512),
				"Collapse Projects"
			},
			{
				new CommandID(new Guid("88822172-82D1-48ff-A566-72400006A992"), 2064),
				"Copy As Project Reference"
			},
			{
				new CommandID(new Guid("899EB090-8728-46DF-8CEB-FCA2E326FE63"), 3752),
				"Copy Class"
			},
			{
				new CommandID(new Guid("7F95D8FB-4996-4763-AF41-A2154A831F77"), 23049),
				"Copy Path"
			},
			{
				new CommandID(new Guid("D88EF4B1-587E-4A9F-AE08-F3CEDDBF028A"), 2274),
				"Copy Reference"
			},
			{
				new CommandID(new Guid("C91EA546-A349-47B1-AA69-7A1529B58C57"), 9843),
				"Copy References"
			},
			{
				new CommandID(new Guid("888DA324-B21F-4658-B663-F22884A3AF1D"), 289),
				"Edit Project File"
			},
			{
				new CommandID(new Guid("F359CFC9-D628-46B4-AA78-99893E4E056C"), 15788),
				"Email CodeSnippet"
			},
			{
				new CommandID(new Guid("DD2ADE52-CB4E-415A-B9C1-C3183BC8DDB5"), 30074),
				"Extract Constant..."
			},
			{
				new CommandID(new Guid("D99DE366-5426-4F39-A444-23698B9B5D89"), 9818),
				"Insert Guid Attribute"
			},
			{
				new CommandID(new Guid("5C199E63-E4F4-4B27-8955-75844A35066A"), 14777),
				"Open Containing Folder"
			},
			{
				new CommandID(new Guid("A9902F9B-09E2-418D-B3D0-EA771B908B65"), 29468),
				"Open Command Prompt"
			},
			{
				new CommandID(new Guid("C328650B-8F49-4883-8D83-3A9103458095"), 8218),
				"Paste Class"
			},
			{
				new CommandID(new Guid("14C14C76-3555-4D81-AFA6-2ADE1EE0D896"), 31753),
				"Paste Reference"
			},
			{
				new CommandID(new Guid("E8F31AE2-1186-4936-9A54-B5D10E6AB0F8"), 4096),
				"Recently Closed Documents"
			},
			{
				new CommandID(new Guid("9759B1F3-64EF-41AF-B383-170CE3FC7277"), 21285),
				"Reload Projects"
			},
			{
				new CommandID(new Guid("453783B0-8DB7-4F1C-B7CE-5319D3915E8E"), 3518),
				"Remove and Sort Usings"
			},
			{
				new CommandID(new Guid("06743131-62C0-406A-8D14-0D487A579D5F"), 6639),
				"Transform Templates"
			},
			{
				new CommandID(new Guid("184DD6C2-6301-49E8-A6C9-D8D026444172"), 26237),
				"Undo Close"
			},
			{
				new CommandID(new Guid("86155E5B-99D3-48A8-B3D7-99860F8FDCA9"), 8169),
				"Unload Projects"
			}
		};
	}
}
