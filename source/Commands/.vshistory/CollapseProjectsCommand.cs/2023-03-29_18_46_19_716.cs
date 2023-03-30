using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using Tasler.RenewedPowerCommands.Common;
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
				var project = DynamicCommand.Dte.SelectedItems.Item(1).Project;
				if (project != null)
				{
					command.Text = "Collapse Projects";

					return project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}"
						? IsAtLeastOneProjectExpanded(GetSelectedUIHierarchy().UIHierarchyItems)
						: IsSelectedProjectExpanded();
				}
				else if (new ProjectIterator(DynamicCommand.Dte.Solution).Count<Project>() > 0)
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

			if (DynamicCommand.Dte.SelectedItems.Item(1).Project != null)
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
				foreach (var item in DTEHelper.GetUIProjectNodes(DynamicCommand.Dte.Solution))
				{
					CollapseProject(item);
				}
				SolutionExplorer.GetItem(DynamicCommand.Dte.Solution.Properties.Item("Name").Value.ToString()).Select(1);
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
			return DTEHelper.GetUIProjectNodes(root).FirstOrDefault(s =>
			{
				ThreadHelper.ThrowIfNotOnUIThread();
				return s.UIHierarchyItems.Expanded;
			}) != null;
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

		public const uint c_cmdidCollapseProjectsCommand = 0x2910U;

		private static UIHierarchy s_solutionExplorer;
	}
}
