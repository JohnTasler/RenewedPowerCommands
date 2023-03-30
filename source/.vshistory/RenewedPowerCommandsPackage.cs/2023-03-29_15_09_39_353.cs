using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using Tasler.RenewedPowerCommands.OptionPages;
using Task = System.Threading.Tasks.Task;

namespace RenewedPowerCommands
{
	[PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
	[Guid(RenewedPowerCommandsPackage.PackageGuidString)]
	public sealed class RenewedPowerCommandsPackage : AsyncPackage
	{
		public const string PackageGuidString = "ba8deca4-149f-42b1-b371-454c7d096326";

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
