using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
//using Microsoft.PowerCommands.Common;
//using Microsoft.PowerCommands.Extensions;
//using Microsoft.PowerCommands.Linq;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.Linq;

namespace Tasler.RenewedPowerCommands.Commands
{
	[Guid("C4C895C3-F940-424C-B158-2923AE5B7B80")]
	[DisplayName("Collapse Projects")]
	internal class CollapseProjectsCommand : DynamicCommand
	{
		public CollapseProjectsCommand(IServiceProvider serviceProvider) : base(serviceProvider, new EventHandler(CollapseProjectsCommand.OnExecute), new CommandID(typeof(CollapseProjectsCommand).GUID, 10512))
		{
		}

		protected static UIHierarchy SolutionExplorer
		{
			get
			{
				if (s_solutionExplorer == null)
				{
					s_solutionExplorer = ((DTE2)DynamicCommand.Dte).ToolWindows.SolutionExplorer;
				}
				return s_solutionExplorer;
			}
		}

		protected override bool CanExecute(OleMenuCommand command)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (base.CanExecute(command))
			{
				Project project = DynamicCommand.Dte.SelectedItems.Item(1).Project;
				if (project != null)
				{
					if (project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
					{
						command.Text = "Collapse Projects";
						return CollapseProjectsCommand.IsAtLeastOneProjectExpanded(CollapseProjectsCommand.GetSelectedUIHierarchy().UIHierarchyItems);
					}
					command.Text = "Collapse Project";
					return CollapseProjectsCommand.IsSelectedProjectExpanded();
				}
				else if (new ProjectIterator(DynamicCommand.Dte.Solution).Count<Project>() > 0)
				{
					command.Text = "Collapse Projects";
					return CollapseProjectsCommand.IsAtLeastOneProjectExpanded(CollapseProjectsCommand.SolutionExplorer.UIHierarchyItems);
				}
			}
			return false;
		}

		private static void OnExecute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (DynamicCommand.Dte.SelectedItems.Item(1).Project != null)
			{
				UIHierarchyItem selectedUIHierarchy = CollapseProjectsCommand.GetSelectedUIHierarchy();
				if (selectedUIHierarchy != null)
				{
					CollapseProjectsCommand.CollapseProject(selectedUIHierarchy);
					return;
				}
			}
			else
			{
				foreach (var item in DTEHelper.GetUIProjectNodes(DynamicCommand.Dte.Solution))
				{
					CollapseProjectsCommand.CollapseProject(item);
				}
				CollapseProjectsCommand.SolutionExplorer.GetItem(DynamicCommand.Dte.Solution.Properties.Item("Name").Value.ToString()).Select(1);
			}
		}

		private static UIHierarchyItem GetSelectedUIHierarchy()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			object[] array = CollapseProjectsCommand.SolutionExplorer.SelectedItems as object[];
			if (array != null && array.Length == 1)
			{
				return array[0] as UIHierarchyItem;
			}
			return null;
		}

		private static bool IsAtLeastOneProjectExpanded(UIHierarchyItems root)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return DTEHelper.GetUIProjectNodes(root).FirstOrDefault((UIHierarchyItem hier) => hier.UIHierarchyItems.Expanded) != null;
		}

		private static bool IsSelectedProjectExpanded()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			UIHierarchyItem selectedUIHierarchy = GetSelectedUIHierarchy();
			return selectedUIHierarchy != null && selectedUIHierarchy.UIHierarchyItems.Expanded;
		}

		private static void CollapseProject(UIHierarchyItem project)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var item in new UIHierarchyItemIterator(project.UIHierarchyItems))
			{
				PerformCollapse(item);
			}
			PerformCollapse(project);
			project.Select(vsUISelectionType.vsUISelectionTypeSelect);
		}

		private static void PerformCollapse(UIHierarchyItem project)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (!DTEHelper.IsUISolutionNode(project) && project.UIHierarchyItems.Expanded)
			{
				project.UIHierarchyItems.Expanded = false;
			}
			if (DTEHelper.IsProjectNode(project) && project.Object is ProjectItem projectItem && projectItem.ContainingProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}" && project.UIHierarchyItems.Expanded)
			{
				project.Select(vsUISelectionType.vsUISelectionTypeSelect);
				SolutionExplorer.DoDefaultAction();
			}
		}

		public const uint cmdidCollapseProjectsCommand = 10512U;

		private static UIHierarchy s_solutionExplorer;
	}
}
