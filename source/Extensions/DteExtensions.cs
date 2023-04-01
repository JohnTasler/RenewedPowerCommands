using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.Linq;
using Tasler.RenewedPowerCommands.Services;
using Process = System.Diagnostics.Process;

namespace Tasler.RenewedPowerCommands.Extensions
{
	public static class DteExtensions
	{
		public static void RestartVS(this DTE dte)
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

		public static int Compile(this Project project)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return project.Compile(out _);
		}

		public static int Compile(this Project project, out string assemblyFile)
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

		public static IEnumerable<UIHierarchyItem> GetUIProjectAndSolutionFoldersNodes(this Solution solution)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string text = solution.Properties.Item("Name").Value.ToString();
			return from item in new UIHierarchyItemIterator(((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems)
				   where item.Object is Project || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project)
				   select item;
		}

		public static bool IsUISolutionNode(this UIHierarchyItem item)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return (item.Object is Project project && project.Kind == EnvDTE.Constants.vsProjectKindSolutionItems)
				|| (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind == EnvDTE.Constants.vsProjectKindSolutionItems);
		}

		public static bool IsProjectNode(this UIHierarchyItem item)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return (item.Object is Project project && project.Kind != EnvDTE.Constants.vsProjectKindSolutionItems)
				|| (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind != EnvDTE.Constants.vsProjectKindSolutionItems);
		}

		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(this Solution solution)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			string text = solution.Properties.Item("Name").Value.ToString();
			return ((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems.GetUIProjectNodes();
		}

		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(this UIHierarchyItems root)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return from item in new UIHierarchyItemIterator(root)
				   where (item.Object is Project project && project.Kind != EnvDTE.Constants.vsProjectKindSolutionItems)
				    || (item.Object is ProjectItem projectItem && projectItem.Object is Project itemProject && itemProject.Kind != EnvDTE.Constants.vsProjectKindSolutionItems)
				   select item;
		}

		public static void OpenDocument(this DTE dte, IDocumentInfo docInfo)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

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

		public static CommandInterceptor CreateCommandInterceptor<T>(this DTE dte, T commandId)
			where T : Enum, IConvertible
		{
			return new CommandInterceptor(dte, typeof(T).GUID, commandId.ToInt32(CultureInfo.CurrentCulture));
		}
	}
}
