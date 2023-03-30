using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.Extensions;
using Tasler.RenewedPowerCommands.Linq;

namespace Tasler.RenewedPowerCommands.Commands
{
	[Guid("24F66925-42C1-4C83-AD7F-C3842D9C8D9D")]
	[DisplayName("Collapse Projects")]
	internal class CollapseProjectsCommand : DynamicCommand
	{
		public CollapseProjectsCommand(IServiceProvider serviceProvider)
			: base(serviceProvider,
				  CollapseProjectsCommand.OnExecute,
				  new CommandID(typeof(CollapseProjectsCommand).GUID, (int)c_cmdidCollapseProjectsCommand))
		{
		}

		protected static UIHierarchy SolutionExplorer
		{
			get
			{
				if (s_solutionExplorer == null)
				{
					s_solutionExplorer = Dte.ToolWindows.SolutionExplorer;
				}
				return s_solutionExplorer;
			}
		}

		protected override bool CanExecute(OleMenuCommand command)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (base.CanExecute(command))
			{
				var project = Dte.SelectedItems.Item(1).Project;
				if (project != null)
				{
					command.Text = "Collapse Projects";

					return project.IsKind(EnvDTE.Constants.vsProjectKindSolutionItems)
						? IsAtLeastOneProjectExpanded(GetSelectedUIHierarchy().UIHierarchyItems)
						: IsSelectedProjectExpanded();
				}
				else if (new ProjectIterator(Dte.Solution).Count<Project>() > 0)
				{
					command.Text = "Collapse Projects";
					return IsAtLeastOneProjectExpanded(SolutionExplorer.UIHierarchyItems);
				}
			}
			return false;
		}

		private static void OnExecute(object sender, EventArgs e)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (Dte.SelectedItems.Item(1).Project != null)
			{
				UIHierarchyItem selectedUIHierarchy = GetSelectedUIHierarchy();
				if (selectedUIHierarchy != null)
				{
					CollapseProject(selectedUIHierarchy);
					return;
				}
			}
			else
			{
				foreach (var item in DTEHelper.GetUIProjectNodes(Dte.Solution))
				{
					CollapseProject(item);
				}
				SolutionExplorer.GetItem(Dte.Solution.Properties.Item("Name").Value.ToString()).Select(vsUISelectionType.vsUISelectionTypeSelect);
			}
		}

		private static UIHierarchyItem GetSelectedUIHierarchy()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (SolutionExplorer.SelectedItems is object[] array && array.Length == 1)
			{
				return array[0] as UIHierarchyItem;
			}
			return null;
		}

		private static bool IsAtLeastOneProjectExpanded(UIHierarchyItems root)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return DTEHelper.GetUIProjectNodes(root).FirstOrDefault(s => s.UIHierarchyItems.IsExpanded()) != null;
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
			if (DTEHelper.IsProjectNode(project) && project.Object is ProjectItem projectItem && projectItem.ContainingProject.IsKind(EnvDTE.Constants.vsProjectKindSolutionItems) && project.UIHierarchyItems.Expanded)
			{
				project.Select(vsUISelectionType.vsUISelectionTypeSelect);
				SolutionExplorer.DoDefaultAction();
			}
		}

		public const uint c_cmdidCollapseProjectsCommand = 0x2910U;

		private static UIHierarchy s_solutionExplorer;
	}
}
