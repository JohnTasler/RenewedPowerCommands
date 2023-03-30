using System;
using System.ComponentModel.Design;
//using Microsoft.PowerCommands.Commands;
//using Microsoft.PowerCommands.Extensions;
//using Microsoft.PowerCommands.Services;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands
{
	internal class CommandSet
	{
		public CommandSet(IServiceProvider provider)
		{
			this.serviceProvider = provider;
			this.menuCommandService = this.serviceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
		}

		public void Initialize()
		{
			this.RegisterMenuCommands();
		}

		private void RegisterMenuCommands()
		{
			ICommandManagerService service = this.serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();
			OleMenuCommand command3 = new CollapseProjectsCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command3);
		}

		// Token: 0x04000001 RID: 1
		private IServiceProvider serviceProvider;

		// Token: 0x04000002 RID: 2
		private OleMenuCommandService menuCommandService;
	}
}
