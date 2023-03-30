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

			foreach (object obj in items)
			{
				UIHierarchyItem item = (UIHierarchyItem)obj;
				yield return item;
				foreach (UIHierarchyItem uihierarchyItem in this.Enumerate(item.UIHierarchyItems))
				{
					yield return uihierarchyItem;
				}
				IEnumerator<UIHierarchyItem> enumerator2 = null;
				item = null;
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		private UIHierarchyItems items;
	}
}
