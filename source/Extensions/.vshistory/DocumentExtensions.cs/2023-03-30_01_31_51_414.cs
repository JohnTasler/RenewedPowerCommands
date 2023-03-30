using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EnvDTE;

using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.Extensions
{
	internal static class DocumentExtensions
	{
		public static bool IsSaved(this Document document)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			return document.IsSaved;
		}
	}
}
