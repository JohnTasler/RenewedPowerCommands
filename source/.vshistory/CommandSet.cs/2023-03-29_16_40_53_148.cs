using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Commands;
using Tasler.RenewedPowerCommands.Services;

namespace Tasler.RenewedPowerCommands
{
	internal class CommandSet
	{
		public CommandSet(IServiceProvider provider)
		{
			_serviceProvider = provider;
			_menuCommandService = _serviceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
		}

		public void Initialize()
		{
			this.RegisterMenuCommands();
		}

		private void RegisterMenuCommands()
		{
			ICommandManagerService service = _serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();
			OleMenuCommand command3 = new CollapseProjectsCommand(_serviceProvider);
			_menuCommandService.AddCommand(command3);
		}

		private IServiceProvider _serviceProvider;

		private OleMenuCommandService _menuCommandService;
	}
}
