using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class CodeElementIterator : IEnumerable<CodeElement>, IEnumerable
	{
		public CodeElementIterator(CodeElements codeElements)
		{
			_codeElements = codeElements ?? throw new ArgumentNullException(nameof(codeElements));
		}

		public IEnumerator<CodeElement> GetEnumerator()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return this.Enumerate(_codeElements).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			return this.GetEnumerator();
		}

		private IEnumerable<CodeElement> Enumerate(CodeElements codeElements)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var element in codeElements.Cast<CodeElement>())
			{
				yield return element;

				CodeElements childElements;
				try
				{
					childElements = element.Children;
				}
				catch (NotImplementedException)
				{
					childElements = null;
				}
				if (childElements != null)
				{
					foreach (CodeElement codeElement in this.Enumerate(childElements))
					{
						yield return codeElement;
					}
				}
			}
		}

		private readonly CodeElements _codeElements;
	}
}
