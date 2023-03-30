using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using System.Threading;
using Tasler.RenewedPowerCommands.OptionPages;
using Tasler.RenewedPowerCommands.Services;
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

		public CommandsPage CommandsPage
		{
			get
			{
				if (_commandsPage == null)
				{
					_commandsPage = base.GetDialogPage(typeof(CommandsPage)) as CommandsPage;
				}
				return _commandsPage;
			}
		}

		private CommandsPage _commandsPage;
	}
}
