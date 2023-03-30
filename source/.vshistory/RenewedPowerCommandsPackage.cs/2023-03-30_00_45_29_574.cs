using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Tasler.RenewedPowerCommands.Common;
using Tasler.RenewedPowerCommands.OptionPages;
using Tasler.RenewedPowerCommands.Services;
using static Microsoft.VisualStudio.VSConstants;
using Task = System.Threading.Tasks.Task;

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
			this.AddService(typeof(SCommandManagerService), new ServiceCreatorCallback(this.CreateCommandManagerService), true);
		}

		#endregion Package Members

		// Microsoft.PowerCommands.PowerCommandsPackage
		private void saveCommandInterceptor_BeforeExecute(object sender, CommandEventArgs args)
		{
			if (this.GeneralPage.RemoveAndSortUsingsOnSave)
			{
				Document activeDocument = this.Dte.ActiveDocument;
				if (args.CommandGuid == typeof(VSStd97CmdID).GUID.ToString())
				{
					IEnumerable<Document> enumerable;
					if (args.CommandId == 0xE0)
					{
						enumerable = this.GetDocumentsToBeSaved();
					}
					else
					{
						IEnumerable<Document> enumerable2 = new Document[]
						{
					activeDocument
						};
						enumerable = enumerable2;
					}
					foreach (Document document in enumerable)
					{
						string kind = document.ProjectItem.ContainingProject.Kind;
						if (document.ProjectItem.FileCodeModel != null && (document.ProjectItem.ContainingProject.Kind == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" || document.ProjectItem.ContainingProject.Kind == "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}" || document.ProjectItem.ContainingProject.Kind == "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}" || document.ProjectItem.ContainingProject.Kind == "{778DAE3C-4631-46EA-AA77-85C1314464D9}"))
						{
							if (!activeDocument.Equals(document))
							{
								document.Activate();
							}
							if (this.GeneralPage.FormatOnSave)
							{
								this.Dte.ExecuteCommand("Edit.FormatDocument", string.Empty);
							}
							if ((document.ProjectItem.ContainingProject.Kind == "{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}" || document.ProjectItem.ContainingProject.Kind == "{9A19103F-16F7-4668-BE54-9A1E7A4F7556}" || document.ProjectItem.ContainingProject.Kind == "{F184B08F-C81C-45F6-A57F-5ABD9991F28F}" || document.ProjectItem.ContainingProject.Kind == "{778DAE3C-4631-46EA-AA77-85C1314464D9}") && this.GeneralPage.RemoveAndSortUsingsOnSave)
							{
								this.Dte.ExecuteCommand("Edit.RemoveAndSort", string.Empty);
							}
							activeDocument.Activate();
						}
					}
				}
			}
		}


		public CommandsPage CommandsPage
		{
			get
			{
				return _commandsPage ?? (_commandsPage = base.GetDialogPage(typeof(CommandsPage)) as CommandsPage);
			}
		}

		public GeneralPage GeneralPage
		{
			get
			{
				return _generalPage ?? (_generalPage = (base.GetDialogPage(typeof(GeneralPage)) as GeneralPage));
			}
		}

		private CommandsPage _commandsPage;
		private GeneralPage _generalPage;
	}
}
