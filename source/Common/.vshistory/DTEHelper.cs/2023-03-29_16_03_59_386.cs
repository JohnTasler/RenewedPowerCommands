using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.PowerCommands.Linq;
using Microsoft.PowerCommands.Services;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Linq;
using Process = System.Diagnostics.Process;

namespace Tasler.RenewedPowerCommands.Common
{
	public static class DTEHelper
	{
		public static void RestartVS(DTE dte)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			Process process = new Process();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			process.StartInfo.FileName = Path.GetFullPath(commandLineArgs[0]);
			process.StartInfo.Arguments = string.Join(" ", commandLineArgs, 1, commandLineArgs.Length - 1);
			process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			process.Start();
			dte.Quit();
		}

		public static int CompileProject(Project project)
		{
			string text;
			return DTEHelper.CompileProject(project, out text);
		}

		public static int CompileProject(Project project, out string assemblyFile)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			project.DTE.Solution.SolutionBuild.BuildProject(project.DTE.Solution.SolutionBuild.ActiveConfiguration.Name, project.UniqueName, true);
			if (project.DTE.Solution.SolutionBuild.LastBuildInfo == 0)
			{
				string path = project.ConfigurationManager.ActiveConfiguration.Properties.Item("OutputPath").Value.ToString();
				string path2 = project.Properties.Item("LocalPath").Value.ToString();
				string path3 = project.Properties.Item("OutputFileName").Value.ToString();
				assemblyFile = Path.Combine(path2, Path.Combine(path, path3));
			}
			else
			{
				assemblyFile = null;
			}
			return project.DTE.Solution.SolutionBuild.LastBuildInfo;
		}

		public static IEnumerable<UIHierarchyItem> GetUIProjectAndSolutionFoldersNodes(Solution solution)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string text = solution.Properties.Item("Name").Value.ToString();
			return from item in new UIHierarchyItemIterator(((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems)
				   where item.Object is Project || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project)
				   select item;
		}

		public static bool IsUISolutionNode(UIHierarchyItem item)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return (item.Object is Project project && project.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				|| (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}");
		}

		public static bool IsProjectNode(UIHierarchyItem item)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return (item.Object is Project project && project.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				|| (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}");
		}

		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(Solution solution)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string text = solution.Properties.Item("Name").Value.ToString();
			return DTEHelper.GetUIProjectNodes(((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems);
		}

		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(UIHierarchyItems root)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return from item in new UIHierarchyItemIterator(root)
				   where (item.Object is Project project && project.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				    || (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				   select item;
		}

		public static void OpenDocument(DTE dte, IDocumentInfo docInfo)
		{
			if (File.Exists(docInfo.DocumentPath))
			{
				Window window = dte.OpenFile(docInfo.DocumentViewKind, docInfo.DocumentPath);
				if (window != null)
				{
					window.Visible = true;
					window.Activate();
					if (docInfo.CursorLine > 1 || docInfo.CursorColumn > 1)
					{
						TextSelection textSelection = window.Document.Selection as TextSelection;
						if (textSelection != null)
						{
							textSelection.MoveTo(docInfo.CursorLine, docInfo.CursorColumn, true);
							textSelection.Cancel();
						}
					}
				}
			}
		}
	}
}
