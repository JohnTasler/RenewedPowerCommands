using System;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.Extensions
{
	internal static class ProjectExtensions
	{
		public static bool IsKind(this Project project, Guid guid)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return project.IsKind(guid.ToString());
		}

		public static bool IsKind(this Project project, string guid)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return string.Compare(project.Kind, guid, StringComparison.OrdinalIgnoreCase) == 0;
		}

		public static FileCodeModel	GetFileCodeModel(this ProjectItem projectItem)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return projectItem.FileCodeModel;
		}

		public static string GetName(this ProjectItem projectItem)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return projectItem.Name;
		}
	}
}
