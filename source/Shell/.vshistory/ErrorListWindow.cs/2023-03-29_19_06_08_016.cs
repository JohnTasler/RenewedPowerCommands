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
			this.serviceProvider = serviceProvider;
		}

		protected ErrorListProvider ErrorListProvider
		{
			get
			{
				if (this.errorListProvider == null)
				{
					this.errorListProvider = new ErrorListProvider(this.serviceProvider);
				}
				return this.errorListProvider;
			}
		}

		public void Show()
		{
			this.ErrorListProvider.Show();
		}

		public void WriteError(string message)
		{
			ErrorTask errorTask = new ErrorTask();
			errorTask.CanDelete = false;
			errorTask.ErrorCategory = TaskErrorCategory.Error;
			errorTask.Text = message;
			this.ErrorListProvider.Tasks.Add(errorTask);
			this.Show();
		}

		public void ClearErrors()
		{
			this.ErrorListProvider.Tasks.Clear();
		}

		private IServiceProvider serviceProvider;

		private ErrorListProvider errorListProvider;
	}
}
