using System;
using System.ComponentModel.Design;
using EnvDTE;
using Tasler.RenewedPowerCommands.Extensions;
using Tasler.RenewedPowerCommands.OptionPages;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.TextManager.Interop;
using IServiceProvider = System.IServiceProvider;
using RenewedPowerCommands;

namespace Tasler.RenewedPowerCommands.Commands
{
	// Token: 0x02000030 RID: 48
	public abstract class DynamicCommand : OleMenuCommand
	{
		protected static IServiceProvider ServiceProvider => DynamicCommand.serviceProvider;

		protected static DTE Dte
		{
			get
			{
				if (DynamicCommand._dte == null)
				{
					DynamicCommand._dte = DynamicCommand.ServiceProvider.GetService<DTE>();
				}
				return DynamicCommand._dte;
			}
		}

		public static Document GetActiveEditorDocument()
		{
			Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();

			IVsTextView vsTextView;
			IVsTextLines vsTextLines;
			if (ErrorHandler.Succeeded(ServiceProvider.GetService<SVsTextManager, IVsTextManager>().GetActiveView(0, null, out vsTextView)) && ErrorHandler.Succeeded(vsTextView.GetBuffer(out vsTextLines)))
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

			return DynamicCommand.Dte.ActiveDocument;
		}

		protected static RenewedPowerCommandsPackage Package
		{
			get
			{
				if (DynamicCommand.powerCommandsPackage == null)
				{
					DynamicCommand.powerCommandsPackage = DynamicCommand.ServiceProvider.GetService<RenewedPowerCommandsPackage>();
				}
				return DynamicCommand.powerCommandsPackage;
			}
		}

		public DynamicCommand(IServiceProvider provider, EventHandler onExecute, CommandID id) : base(onExecute, id)
		{
			base.BeforeQueryStatus += this.OnBeforeQueryStatus;
			DynamicCommand.serviceProvider = provider;
		}

		protected void OnBeforeQueryStatus(object sender, EventArgs e)
		{
			OleMenuCommand oleMenuCommand = sender as OleMenuCommand;
			oleMenuCommand.Enabled = (oleMenuCommand.Visible = (oleMenuCommand.Supported = this.CanExecute(oleMenuCommand)));
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00005DFB File Offset: 0x00003FFB
		protected virtual bool CanExecute(OleMenuCommand command)
		{
			return DynamicCommand.Package.CommandsPage.IsCommandEnabled(command.CommandID.Guid, command.CommandID.ID);
		}

		private static DTE _dte;

		private static IServiceProvider serviceProvider;

		private static RenewedPowerCommandsPackage powerCommandsPackage;
	}
}
