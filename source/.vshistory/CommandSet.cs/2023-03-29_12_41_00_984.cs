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
			this._serviceProvider = provider;
			this._menuCommandService = this._serviceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
		}

		public void Initialize()
		{
			this.RegisterMenuCommands();
		}

		private void RegisterMenuCommands()
		{
			ICommandManagerService service = this._serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();
			OleMenuCommand command3 = new CollapseProjectsCommand(this._serviceProvider);
			this._menuCommandService.AddCommand(command3);
		}

		private IServiceProvider _serviceProvider;

		private OleMenuCommandService _menuCommandService;
	}
}
