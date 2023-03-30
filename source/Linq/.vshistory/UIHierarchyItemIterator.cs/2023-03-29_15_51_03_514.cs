using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class UIHierarchyItemIterator : IEnumerable<UIHierarchyItem>, IEnumerable
	{
		public UIHierarchyItemIterator(UIHierarchyItems items)
		{
			if (items == null)
			{
				throw new ArgumentNullException(nameof(items));
			}
			this.items = items;
		}

		public IEnumerator<UIHierarchyItem> GetEnumerator()
		{
			return (from item in this.Enumerate(this.items)
					select item).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private IEnumerable<UIHierarchyItem> Enumerate(UIHierarchyItems items)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var item in items.OfType<UIHierarchyItem>())
			{
				yield return item;
				foreach (UIHierarchyItem uihierarchyItem in this.Enumerate(item.UIHierarchyItems))
				{
					yield return uihierarchyItem;
				}
			}
		}

		private UIHierarchyItems items;
	}
}
