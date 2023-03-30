using Microsoft.VisualStudio.Shell;
using System;
using System.ComponentModel.Design;
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
			service.AddCommand(new CollapseProjectsCommand(_serviceProvider));
			service.AddCommand(new RemoveSortUsingsCommand(_serviceProvider));
		}

		private readonly IServiceProvider _serviceProvider;

		private readonly OleMenuCommandService _menuCommandService;
	}
}
