using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.Extensions;
using Tasler.RenewedPowerCommands.Linq;

namespace Tasler.RenewedPowerCommands.Commands
{
	[Guid("7AED1DF9-6BAC-4FE0-8276-57369DEEDD8E")]
	[DisplayName("Paste Class")]
	internal class PasteClassCommand : DynamicCommand
	{
		public PasteClassCommand(IServiceProvider serviceProvider)
			: base(serviceProvider,
				  PasteClassCommand.OnExecute,
				  new CommandID(typeof(PasteClassCommand).GUID, c_cmdidPasteClassCommand))
		{
		}

		protected override bool CanExecute(OleMenuCommand command)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (base.CanExecute(command) && Clipboard.ContainsText())
			{
				string text = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString();
				if (text.StartsWith("class:"))
				{
					string value = text.Split(new[] { "//" }, StringSplitOptions.None)[0].Split(':')[1];
					var project = DynamicCommand.Dte.SelectedItems.Item(1).Project;
					if (project != null)
					{
						return project.IsKind(value);
					}
					var projectItem = DynamicCommand.Dte.SelectedItems.Item(1).ProjectItem;
					return projectItem == null || projectItem.ContainingProject.IsKind(value);
				}
			}
			return false;
		}

		private static void OnExecute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var project = Dte.SelectedItems.Item(1).Project;
			if (project != null)
			{
				Process(project, project.ProjectItems);
				return;
			}
			ProjectItem projectItem = Dte.SelectedItems.Item(1).ProjectItem;
			if (projectItem != null)
			{
				Process(projectItem.ContainingProject, projectItem.ProjectItems);
			}
		}

		private static void Process(Project project, ProjectItems projectItems)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string[] array = Clipboard.GetDataObject().GetData(DataFormats.Text).ToString().Split(new string[] {"&"}, StringSplitOptions.None);
			ProjectItem projectItem = AddClass(array[0].Split(new string[] { "//" }, StringSplitOptions.None)[1], project, projectItems);
			if (array.Length > 1)
			{
				for (int i = 1; i < array.Length; ++i)
				{
					AddClass(array[i].Split(new string[] { "//" }, StringSplitOptions.None)[1],
					IOHelper.GetFileWithoutExtension(projectItem.FileNames[1]), project, projectItem.ProjectItems);
				}
			}
		}

		private static ProjectItem AddClass(string fileName, Project project, ProjectItems projectItems)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return AddClass(fileName, GetIdentifierName(project, fileName), project, projectItems);
		}

		private static ProjectItem AddClass(string fileName, string identifierName, Project project, ProjectItems projectItems)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			var projectItem = projectItems.AddFromTemplate(fileName, $"{identifierName}{IOHelper.GetFileExtension(fileName)}");
			var codeClass = new CodeElementIterator(projectItem.FileCodeModel.CodeElements).OfType<CodeClass>().FirstOrDefault();
			if (codeClass != null)
			{
				codeClass.Name = identifierName;
			}
			projectItem.Open(Constants.vsViewKindTextView).Activate();
			return projectItem;
		}

		private static string GetIdentifierName(Project project, string fileName)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string fileNameWithoutExtension = IOHelper.GetFileWithoutExtension(fileName);
			var source = new ProjectItemIterator(project.ProjectItems)
				.Where(item => item.GetName().StartsWith(fileNameWithoutExtension)).Select(item => item.GetName());

			return fileNameWithoutExtension + source.Count();
		}

		public const int c_cmdidPasteClassCommand = 0x201A;
	}
}
