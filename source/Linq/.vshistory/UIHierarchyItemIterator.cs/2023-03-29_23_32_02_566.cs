using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class UIHierarchyItemIterator : IEnumerable<UIHierarchyItem>, IEnumerable
	{
		public UIHierarchyItemIterator(UIHierarchyItems items)
		{
			_items = items ?? throw new ArgumentNullException(nameof(items));
		}

		public IEnumerator<UIHierarchyItem> GetEnumerator()
		{
			foreach (var item in this.Enumerate(_items))
			{
				yield  return item;
			}

			//return (from item in this.Enumerate(_items)
			//		select item).GetEnumerator();
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

		private UIHierarchyItems _items;
	}
}
