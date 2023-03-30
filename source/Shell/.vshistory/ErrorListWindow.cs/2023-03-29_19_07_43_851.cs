using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasler.RenewedPowerCommands.Shell
{
	public class ErrorListWindow
	{
		public ErrorListWindow(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		protected ErrorListProvider ErrorListProvider
		{
			get
			{
				if (_errorListProvider == null)
				{
					_errorListProvider = new ErrorListProvider(_serviceProvider);
				}
				return _errorListProvider;
			}
		}

		public void Show()
		{
			this.ErrorListProvider.Show();
		}

		public void WriteError(string message)
		{
			var errorTask = new ErrorTask
			{
				CanDelete = false,
				ErrorCategory = TaskErrorCategory.Error,
				Text = message
			};
			this.ErrorListProvider.Tasks.Add(errorTask);
			this.Show();
		}

		public void ClearErrors()
		{
			this.ErrorListProvider.Tasks.Clear();
		}

		private readonly IServiceProvider _serviceProvider;

		private ErrorListProvider _errorListProvider;
	}
}
