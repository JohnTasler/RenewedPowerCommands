using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasler.RenewedPowerCommands.Extensions
{
	internal static class UIHierarchyItemsExtensions
	{
		public static bool IsExpanded(this UIHierarchyItems @this)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return @this.Expanded;
		}

		public static bool ItemsAreExpanded(this UIHierarchyItem @this)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return @this.Expanded;
		}
	}
}
