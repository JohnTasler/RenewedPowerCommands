using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Tasler.RenewedPowerCommands.OptionPages;
using Task = System.Threading.Tasks.Task;

namespace Tasler.RenewedPowerCommands
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[ProvideProfile(typeof(CommandsPage), "RenewedPowerCommands", "Commands", 0x3CF0, 0x778, true, DescriptionResourceID = 0xC5)]
	[ProvideOptionPage(typeof(CommandsPage), "RenewedPowerCommands", "Commands", 0x3CF0, 0x778, true, "ToolsOptionsKeywords_Commands")]
	[ProvideProfile(typeof(GeneralPage), "RenewedPowerCommands", "General", 0x3CF0, 0x11FE, true, DescriptionResourceID = 0xB4B)]
	[ProvideOptionPage(typeof(GeneralPage), "RenewedPowerCommands", "General", 0x3CF0, 0x11FE, true, "ToolsOptionsKeywords_General")]
	[Guid(RenewedPowerCommandsPackage.PackageGuidString)]
	public sealed class RenewedPowerCommandsPackage : AsyncPackage
	{
		public const string PackageGuidString = "BA8DECA4-149F-42B1-B371-454C7D096326";

		#region Package Members

		protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
		{
			await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);
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
