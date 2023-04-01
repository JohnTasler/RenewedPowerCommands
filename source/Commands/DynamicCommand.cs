using System;
using System.ComponentModel.Design;
using IServiceProvider = System.IServiceProvider;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using Tasler.RenewedPowerCommands.Extensions;

namespace Tasler.RenewedPowerCommands.Commands
{
	public abstract class DynamicCommand : OleMenuCommand
	{
		protected static IServiceProvider ServiceProvider => _serviceProvider;
		private static IServiceProvider _serviceProvider;

		protected static DTE2 Dte => _dte ?? (_dte = (DTE2)ServiceProvider.GetService<DTE>());
		private static DTE2 _dte;

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

		protected static RenewedPowerCommandsPackage Package => _package ?? (_package = ServiceProvider.GetService<RenewedPowerCommandsPackage>());
		private static RenewedPowerCommandsPackage _package;

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

		protected abstract bool CanExecute(OleMenuCommand command);
	}
}
