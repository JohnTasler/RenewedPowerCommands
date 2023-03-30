using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.ComponentModel.Design;
using Tasler.RenewedPowerCommands.Extensions;
using IServiceProvider = System.IServiceProvider;

namespace Tasler.RenewedPowerCommands.Commands
{
	public abstract class DynamicCommand : OleMenuCommand
	{
		protected static IServiceProvider ServiceProvider => _serviceProvider;

		protected static DTE2 Dte
		{
			get
			{
				if (_dte == null)
				{
					_dte = ServiceProvider.GetService<DTE>();
				}
				return (DTE2)_dte;
			}
		}

		public static Document GetActiveEditorDocument()
		{
			Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

			if (ErrorHandler.Succeeded(ServiceProvider.GetService<SVsTextManager, IVsTextManager>().GetActiveView(0, null, out IVsTextView vsTextView))
				&& ErrorHandler.Succeeded(vsTextView.GetBuffer(out IVsTextLines vsTextLines)))
			{
				if (vsTextLines is IExtensibleObject extensibleObject)
				{
					extensibleObject.GetAutomationObject("Document", null, out object obj);
					if (obj is Document document)
					{
						return document;
					}
				}
			}

			return Dte.ActiveDocument;
		}

		protected static RenewedPowerCommandsPackage Package
		{
			get
			{
				if (_package == null)
				{
					_package = ServiceProvider.GetService<RenewedPowerCommandsPackage>();
				}
				return _package;
			}
		}

		public DynamicCommand(IServiceProvider provider, EventHandler onExecute, CommandID id) : base(onExecute, id)
		{
			base.BeforeQueryStatus += this.OnBeforeQueryStatus;
			_serviceProvider = provider;
		}

		protected void OnBeforeQueryStatus(object sender, EventArgs e)
		{
			OleMenuCommand oleMenuCommand = sender as OleMenuCommand;
			oleMenuCommand.Enabled = (oleMenuCommand.Visible = (oleMenuCommand.Supported = this.CanExecute(oleMenuCommand)));
		}

		protected virtual bool CanExecute(OleMenuCommand command)
		{
			return Package.CommandsPage.IsCommandEnabled(command.CommandID.Guid, command.CommandID.ID);
		}

		private static DTE _dte;

		private static IServiceProvider _serviceProvider;

		private static RenewedPowerCommandsPackage _package;
	}
}
