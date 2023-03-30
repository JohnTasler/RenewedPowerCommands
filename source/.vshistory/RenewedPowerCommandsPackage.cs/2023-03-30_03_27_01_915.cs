using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using Task = System.Threading.Tasks.Task;

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;

using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.OptionPages;
using Tasler.RenewedPowerCommands.Services;
using Tasler.RenewedPowerCommands.Extensions;

namespace Tasler.RenewedPowerCommands
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideMenuResource(1000, 1)]
	[ProvideAutoLoad(EnvDTE.Constants.vsContextNoSolution, PackageAutoLoadFlags.BackgroundLoad)]
	[ProvideService   (typeof(SCommandManagerService), ServiceName = "CommandManagerService")]
	[ProvideProfile   (typeof(CommandsPage          ), "RenewedPowerCommands", "Commands", 15600, 1912, true, DescriptionResourceID = 197    )]
	[ProvideOptionPage(typeof(CommandsPage          ), "RenewedPowerCommands", "Commands", 15600, 1912, true, "ToolsOptionsKeywords_Commands")]
	[ProvideProfile   (typeof(GeneralPage           ), "RenewedPowerCommands", "General" , 15600, 4606, true, DescriptionResourceID = 2891   )]
	[ProvideOptionPage(typeof(GeneralPage           ), "RenewedPowerCommands", "General" , 15600, 4606, true, "ToolsOptionsKeywords_General" )]
	[Guid(RenewedPowerCommandsPackage.PackageGuidString)]
	public sealed class RenewedPowerCommandsPackage : AsyncPackage
	{
		public const string PackageGuidString = "BA8DECA4-149F-42B1-B371-454C7D096326";

		#region Package Members

		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
			((IServiceContainer)this).AddService(typeof(SCommandManagerService), new ServiceCreatorCallback(this.CreateCommandManagerService), true);
			new CommandSet(this).Initialize();
			var commandIdGuid = typeof(VSConstants.VSStd97CmdID).GUID;
			_saveCommandInterceptors = new List<CommandInterceptor>
			{
				new CommandInterceptor(this.Dte).SetCommandId(VSConstants.VSStd97CmdID.Save),
				new CommandInterceptor(this.Dte).SetCommandId(VSConstants.VSStd97CmdID.SaveProjectItem),
				new CommandInterceptor(this.Dte).SetCommandId(VSConstants.VSStd97CmdID.SaveAs),
				new CommandInterceptor(this.Dte).SetCommandId(VSConstants.VSStd97CmdID.SaveProjectItemAs),
				new CommandInterceptor(this.Dte).SetCommandId(VSConstants.VSStd97CmdID.SaveSolution),
			};

			try
			{
				foreach (CommandInterceptor commandInterceptor in _saveCommandInterceptors)
				{
					commandInterceptor.BeforeExecute += this.SaveCommandInterceptor_BeforeExecute;
				}
			}
			catch
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			ThreadHelper.ThrowIfNotOnUIThread();

			if (_saveCommandInterceptors != null)
			{
				foreach (CommandInterceptor commandInterceptor in _saveCommandInterceptors)
				{
					commandInterceptor.BeforeExecute -= this.SaveCommandInterceptor_BeforeExecute;
					commandInterceptor.Dispose();
				}
			}
		}
		#endregion Package Members

		private void SaveCommandInterceptor_BeforeExecute(object sender, CommandEventArgs args)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			if (new Guid(args.CommandGuid) == typeof(VSConstants.VSStd97CmdID).GUID)
			{
				if (this.GeneralPage.RemoveAndSortUsingsOnSave || this.GeneralPage.FormatOnSave)
				{
					Document activeDocument = this.Dte.ActiveDocument;
					IEnumerable<Document> enumerable;
					if (args.CommandId == 0xE0)
					{
						enumerable = this.GetDocumentsToBeSaved();
					}
					else
					{
						enumerable = new Document[] { activeDocument };
					}

					foreach (Document document in enumerable)
					{
						if (document.ProjectItem.FileCodeModel != null
							&& document.ProjectItem.ContainingProject.IsKind(VSLangProj.PrjKind.prjKindCSharpProject)
							|| document.ProjectItem.ContainingProject.IsKind(VSLangProj.PrjKind.prjKindVBProject)
							|| document.ProjectItem.ContainingProject.IsKind(CpsCsProjectGuid)
							|| document.ProjectItem.ContainingProject.IsKind(CpsVbProjectGuid))
						{
							if (!activeDocument.Equals(document))
							{
								document.Activate();
							}
							if (this.GeneralPage.FormatOnSave)
							{
								this.Dte.ExecuteCommand("Edit.FormatDocument", string.Empty);
							}
							if (this.GeneralPage.RemoveAndSortUsingsOnSave)
							{
								this.Dte.ExecuteCommand("Edit.RemoveAndSort", string.Empty);
							}
							activeDocument.Activate();
						}
					}
				}
			}
		}

		public DTE2 Dte => _dte ?? (_dte = this.GetService<DTE>() as DTE2);
		private DTE2 _dte;

		public CommandsPage CommandsPage => _commandsPage ?? (_commandsPage = base.GetDialogPage(typeof(CommandsPage)) as CommandsPage);
		private CommandsPage _commandsPage;

		public GeneralPage GeneralPage => _generalPage ?? (_generalPage = (base.GetDialogPage(typeof(GeneralPage)) as GeneralPage));
		private GeneralPage _generalPage;

		private List<CommandInterceptor> _saveCommandInterceptors;

		private object CreateCommandManagerService(IServiceContainer container, Type serviceType)
		{
			if (container != this)
			{
				return null;
			}
			if (typeof(SCommandManagerService) == serviceType)
			{
				return new CommandManagerService();
			}
			return null;
		}

		private IEnumerable<Document> GetDocumentsToBeSaved()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			new RunningDocumentTable(this);
			return this.Dte.Documents.OfType<Document>().Where((Document document) => !document.IsSaved());
		}

		private const string CpsCsProjectGuid = "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}";

		private const string CpsVbProjectGuid = "{778DAE3C-4631-46EA-AA77-85C1314464D9}";
	}
}
