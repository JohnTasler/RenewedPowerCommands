using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.PowerCommands.Common;
using Microsoft.PowerCommands.Extensions;
using Microsoft.PowerCommands.Linq;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PowerCommands.Commands
{
	// Token: 0x02000034 RID: 52
	[Guid("C4C895C3-F940-424C-B158-2923AE5B7B80")]
	[DisplayName("Collapse Projects")]
	internal class CollapseProjectsCommand : DynamicCommand
	{
		// Token: 0x06000173 RID: 371 RVA: 0x00006297 File Offset: 0x00004497
		public CollapseProjectsCommand(IServiceProvider serviceProvider) : base(serviceProvider, new EventHandler(CollapseProjectsCommand.OnExecute), new CommandID(typeof(CollapseProjectsCommand).GUID, 10512))
		{
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000062C5 File Offset: 0x000044C5
		protected static UIHierarchy SolutionExplorer
		{
			get
			{
				if (CollapseProjectsCommand.solutionExplorer == null)
				{
					CollapseProjectsCommand.solutionExplorer = ((DTE2)DynamicCommand.Dte).ToolWindows.SolutionExplorer;
				}
				return CollapseProjectsCommand.solutionExplorer;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000062EC File Offset: 0x000044EC
		protected override bool CanExecute(OleMenuCommand command)
		{
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

		// Token: 0x06000176 RID: 374 RVA: 0x00006394 File Offset: 0x00004594
		private static void OnExecute(object sender, EventArgs e)
		{
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
				DTEHelper.GetUIProjectNodes(DynamicCommand.Dte.Solution).ForEach(delegate(UIHierarchyItem uiHier)
				{
					CollapseProjectsCommand.CollapseProject(uiHier);
				});
				CollapseProjectsCommand.SolutionExplorer.GetItem(DynamicCommand.Dte.Solution.Properties.Item("Name").Value.ToString()).Select(1);
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00006434 File Offset: 0x00004634
		private static UIHierarchyItem GetSelectedUIHierarchy()
		{
			object[] array = CollapseProjectsCommand.SolutionExplorer.SelectedItems as object[];
			if (array != null && array.Length == 1)
			{
				return array[0] as UIHierarchyItem;
			}
			return null;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00006464 File Offset: 0x00004664
		private static bool IsAtLeastOneProjectExpanded(UIHierarchyItems root)
		{
			return DTEHelper.GetUIProjectNodes(root).FirstOrDefault((UIHierarchyItem hier) => hier.UIHierarchyItems.Expanded) != null;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006494 File Offset: 0x00004694
		private static bool IsSelectedProjectExpanded()
		{
			UIHierarchyItem selectedUIHierarchy = CollapseProjectsCommand.GetSelectedUIHierarchy();
			return selectedUIHierarchy != null && selectedUIHierarchy.UIHierarchyItems.Expanded;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x000064B7 File Offset: 0x000046B7
		private static void CollapseProject(UIHierarchyItem project)
		{
			new UIHierarchyItemIterator(project.UIHierarchyItems).ForEach(delegate(UIHierarchyItem subUiHier)
			{
				CollapseProjectsCommand.PerformCollapse(subUiHier);
			});
			CollapseProjectsCommand.PerformCollapse(project);
			project.Select(1);
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000064F8 File Offset: 0x000046F8
		private static void PerformCollapse(UIHierarchyItem project)
		{
			if (!DTEHelper.IsUISolutionNode(project) && project.UIHierarchyItems.Expanded)
			{
				project.UIHierarchyItems.Expanded = false;
			}
			if (DTEHelper.IsProjectNode(project) && project.Object is ProjectItem && (project.Object as ProjectItem).ContainingProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}" && project.UIHierarchyItems.Expanded)
			{
				project.Select(1);
				CollapseProjectsCommand.SolutionExplorer.DoDefaultAction();
			}
		}

		// Token: 0x04000085 RID: 133
		public const uint cmdidCollapseProjectsCommand = 10512U;

		// Token: 0x04000086 RID: 134
		private static UIHierarchy solutionExplorer;
	}
}
