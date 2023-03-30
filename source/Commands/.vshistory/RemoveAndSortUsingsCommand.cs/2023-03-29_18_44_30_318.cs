using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.Extensions;
using Tasler.RenewedPowerCommands.Linq;

namespace Tasler.RenewedPowerCommands.Commands
{
	[Guid("DAF452A2-2D1F-4D11-B477-97C2F71809D1")]
	[DisplayName("Remove and Sort Usings")]
	internal class RemoveSortUsingsCommand : DynamicCommand
	{
		public RemoveSortUsingsCommand(IServiceProvider serviceProvider)
			: base(serviceProvider,
				  RemoveSortUsingsCommand.OnExecute,
				  new CommandID(typeof(RemoveSortUsingsCommand).GUID, (int)c_cmdidRemoveSortUsingsCommand))
		{
		}

		protected override bool CanExecute(OleMenuCommand command)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (base.CanExecute(command))
			{
				Project project = DynamicCommand.Dte.SelectedItems.Item(1).Project;
				if (project == null)
				{
					return RemoveSortUsingsCommand.IsAtLeastOneCSharpProject();
				}
				if (project.IsKind("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}"))
				{
					return true;
				}
			}
			return false;
		}

		private static void OnExecute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			Project project = DynamicCommand.Dte.SelectedItems.Item(1).Project;
			if (project?.Kind == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")
			{
				RemoveSortUsingsCommand.ProcessProject(project);
				return;
			}
			foreach (var proj in new ProjectIterator(Dte.Solution).Where(p => p.IsKind("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")))
			{
				RemoveSortUsingsCommand.ProcessProject(proj);
			}
		}

		private static bool IsAtLeastOneCSharpProject()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return new ProjectIterator(DynamicCommand.Dte.Solution).FirstOrDefault(p => p.GetKind("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}")) != null;
		}

		private static void ProcessProject(Project project)
		{
			if (project != null)
			{
				if (DTEHelper.CompileProject(project) != 0)
				{
					new ErrorListWindow(DynamicCommand.ServiceProvider).Show();
					return;
				}
				RunningDocumentTable source = new RunningDocumentTable(DynamicCommand.ServiceProvider);
				List<string> alreadyOpenFiles = source.Select((RunningDocumentInfo info) => info.Moniker).ToList<string>();
				string fileName;
				Func<string, bool> <> 9__3;
				new ProjectItemIterator(project.ProjectItems).Where((ProjectItem item) => item.FileCodeModel != null).ForEach(delegate (ProjectItem item)
				{
					fileName = item.get_FileNames(1);
					Window window = DynamicCommand.Dte.OpenFile("{7651A703-06E5-11D1-8EBD-00A0C90F26EA}", fileName);
					window.Activate();
					try
					{
						DynamicCommand.Dte.ExecuteCommand("Edit.RemoveAndSort", string.Empty);
					}
					catch (COMException)
					{
					}
					IEnumerable<string> alreadyOpenFiles = alreadyOpenFiles;
					Func<string, bool> predicate;
					if ((predicate = <> 9__3) == null)
					{
						predicate = (<> 9__3 = ((string file) => file.Equals(fileName, StringComparison.OrdinalIgnoreCase)));
					}
					if (alreadyOpenFiles.SingleOrDefault(predicate) != null)
					{
						DynamicCommand.Dte.ActiveDocument.Save(fileName);
						return;
					}
					window.Close(1);
				});
			}
		}

		public const uint c_cmdidRemoveSortUsingsCommand = 0xDBEU;
	}
}
