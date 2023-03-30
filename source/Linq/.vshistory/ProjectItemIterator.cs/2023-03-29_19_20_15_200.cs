using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class ProjectItemIterator : IEnumerable<ProjectItem>, IEnumerable
	{
		public ProjectItemIterator(ProjectItems items)
		{
			this._items = items ?? throw new ArgumentNullException(nameof(items));
		}

		public IEnumerator<ProjectItem> GetEnumerator()
		{
			return (from item in this.Enumerate(this._items)
					select item).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private IEnumerable<ProjectItem> Enumerate(ProjectItems items)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var item in items.Cast<ProjectItem>())
			{
				yield return item;

				foreach (ProjectItem projectItem in this.Enumerate(item.ProjectItems))
				{
					yield return projectItem;
				}
			}
		}

		private readonly ProjectItems _items;
	}
}
