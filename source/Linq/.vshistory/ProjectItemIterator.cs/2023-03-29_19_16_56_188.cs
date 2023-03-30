using EnvDTE;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class ProjectItemIterator : IEnumerable<ProjectItem>, IEnumerable
	{
		public ProjectItemIterator(ProjectItems items)
		{
			this.items = items ?? throw new ArgumentNullException(nameof(items));
		}

		public IEnumerator<ProjectItem> GetEnumerator()
		{
			return (from item in this.Enumerate(this.items)
					select item).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private IEnumerable<ProjectItem> Enumerate(ProjectItems items)
		{
			foreach (object obj in items)
			{
				ProjectItem item = (ProjectItem)obj;
				yield return item;
				foreach (ProjectItem projectItem in this.Enumerate(item.ProjectItems))
				{
					yield return projectItem;
				}
				IEnumerator<ProjectItem> enumerator2 = null;
				item = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		private ProjectItems items;
	}
}
