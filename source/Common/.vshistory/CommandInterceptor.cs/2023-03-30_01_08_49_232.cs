using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasler.RenewedPowerCommands.Common
{
	internal class CommandInterceptor : IDisposable
	{
		private DTE Dte { get; private set; }

		private string CommandGuid { get; set; }

		private int CommandId { get; set; }

		public CommandInterceptor(DTE dte, string guid, int commandId)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			this.Dte = dte;
			this.CommandGuid = guid;
			this.CommandId = commandId;
			if (this.CommandEvents != null)
			{
				this.CommandEvents.AfterExecute += this.OnAfterExecute;
				this.CommandEvents.BeforeExecute += this.OnBeforeExecute;
			}
		}

		public event EventHandler<CommandEventArgs> AfterExecute;

		public event EventHandler<CommandEventArgs> BeforeExecute;

		protected CommandEvents CommandEvents
		{
			get
			{
				ThreadHelper.ThrowIfNotOnUIThread();

				if (_commandEvents == null && this.Dte != null)
				{
					_commandEvents = this.Dte.Events.get_CommandEvents(this.CommandGuid, this.CommandId);
				}
				return _commandEvents;
			}
		}

		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		private void Dispose(bool disposing)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (!_isDisposed && disposing)
			{
				if (this.CommandEvents != null)
				{
					this.CommandEvents.AfterExecute -= this.OnAfterExecute;
					this.CommandEvents.BeforeExecute -= this.OnBeforeExecute;
				}
				_isDisposed = true;
			}
		}

		private void OnAfterExecute(string Guid, int ID, object CustomIn, object CustomOut)
		{
			this.AfterExecute?.Invoke(this, new CommandEventArgs(Guid, ID));
		}

		private void OnBeforeExecute(string Guid, int ID, object CustomIn, object CustomOut, ref bool CancelDefault)
		{
			this.BeforeExecute?.Invoke(this, new CommandEventArgs(Guid, ID));
		}

		private bool _isDisposed;

		private CommandEvents _commandEvents;
	}
}
