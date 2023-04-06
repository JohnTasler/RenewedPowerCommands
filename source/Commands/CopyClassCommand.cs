using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Extensions;
using Tasler.RenewedPowerCommands.Linq;
using VSLangProj;

namespace Tasler.RenewedPowerCommands.Commands
{
	[Guid("41E946DC-C6A9-4604-A673-7013E213B947")]
	[DisplayName("Copy Class")]
	internal class CopyClassCommand : DynamicCommand
	{
		public CopyClassCommand(IServiceProvider serviceProvider)
			: base(serviceProvider,
				  CopyClassCommand.OnExecute,
				  new CommandID(typeof(CopyClassCommand).GUID, c_cmdidCopyClassCommand))
		{
		}

		protected override bool CanExecute(OleMenuCommand command)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (base.CanExecute(command))
			{
				ProjectItem projectItem = DynamicCommand.Dte.SelectedItems.Item(1).ProjectItem;
				if (projectItem != null &&
					(projectItem.ContainingProject.IsKind(PrjKind.prjKindCSharpProject)
					|| projectItem.ContainingProject.IsKind(PrjKind.prjKindVBProject))
					&& projectItem.FileCodeModel != null)
				{
					return new CodeElementIterator(projectItem.FileCodeModel.CodeElements).OfType<CodeClass>().Count() == 1;
				}
			}
			return false;
		}

		private static void OnExecute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			ProjectItem projectItem = DynamicCommand.Dte.SelectedItems.Item(1).ProjectItem;
			if (projectItem?.FileCodeModel != null)
			{
				var list = new List<string>
				{
					$"class:{projectItem.ContainingProject.Kind}//{projectItem.FileNames[1]}",
				};
				foreach (var item in projectItem.ProjectItems.Cast<ProjectItem>())
				{
					list.Add($"class:{item.ContainingProject.Kind}//{item.FileNames[1]}");
				}
				Clipboard.SetDataObject(string.Join("&", list), true);
			}
		}

		public const int c_cmdidCopyClassCommand = 0x0EA8;
	}
}
