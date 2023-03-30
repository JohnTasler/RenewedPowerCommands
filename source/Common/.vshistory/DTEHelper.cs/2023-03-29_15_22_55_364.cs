using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using EnvDTE;
using EnvDTE80;
using Microsoft.PowerCommands.Linq;
using Microsoft.PowerCommands.Services;

namespace Tasler.RenewedPowerCommands.Common
{
	// Token: 0x0200002C RID: 44
	public class DTEHelper
	{
		// Token: 0x06000145 RID: 325 RVA: 0x00005638 File Offset: 0x00003838
		public static void RestartVS(DTE dte)
		{
			Process process = new Process();
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			process.StartInfo.FileName = Path.GetFullPath(commandLineArgs[0]);
			process.StartInfo.Arguments = string.Join(" ", commandLineArgs, 1, commandLineArgs.Length - 1);
			process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			process.Start();
			dte.Quit();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005698 File Offset: 0x00003898
		public static int CompileProject(Project project)
		{
			string text;
			return DTEHelper.CompileProject(project, out text);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x000056B0 File Offset: 0x000038B0
		public static int CompileProject(Project project, out string assemblyFile)
		{
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

		// Token: 0x06000148 RID: 328 RVA: 0x00005790 File Offset: 0x00003990
		public static IEnumerable<UIHierarchyItem> GetUIProjectAndSolutionFoldersNodes(Solution solution)
		{
			string text = solution.Properties.Item("Name").Value.ToString();
			return from item in new UIHierarchyItemIterator(((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems)
				   where item.Object is Project || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project)
				   select item;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005828 File Offset: 0x00003A28
		public static bool IsUISolutionNode(UIHierarchyItem item)
		{
			return (item.Object is Project && ((Project)item.Object).Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}") || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project && ((Project)((ProjectItem)item.Object).Object).Kind == "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}");
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000058AC File Offset: 0x00003AAC
		public static bool IsProjectNode(UIHierarchyItem item)
		{
			return (item.Object is Project && ((Project)item.Object).Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}") || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project && ((Project)((ProjectItem)item.Object).Object).Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00005930 File Offset: 0x00003B30
		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(Solution solution)
		{
			string text = solution.Properties.Item("Name").Value.ToString();
			return DTEHelper.GetUIProjectNodes(((DTE2)solution.DTE).ToolWindows.SolutionExplorer.GetItem(text).UIHierarchyItems);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005980 File Offset: 0x00003B80
		public static IEnumerable<UIHierarchyItem> GetUIProjectNodes(UIHierarchyItems root)
		{
			return from item in new UIHierarchyItemIterator(root)
				   where (item.Object is Project && ((Project)item.Object).Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}") || (item.Object is ProjectItem && ((ProjectItem)item.Object).Object is Project && ((Project)((ProjectItem)item.Object).Object).Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				   select item;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000059DC File Offset: 0x00003BDC
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
