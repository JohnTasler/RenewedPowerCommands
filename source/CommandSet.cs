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
			_service = _serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();
		}

		public void Initialize()
		{
			this.RegisterMenuCommands();
		}

		private void RegisterMenuCommands()
		{
			this.AddAndRegisterCommand(new CollapseProjectsCommand(_serviceProvider));
			this.AddAndRegisterCommand(new RemoveSortUsingsCommand(_serviceProvider));
		}

		private void AddAndRegisterCommand(OleMenuCommand command)
		{
			_menuCommandService.AddCommand(command);
			_service.RegisterCommand(command);
		}

		private readonly IServiceProvider _serviceProvider;
		private readonly OleMenuCommandService _menuCommandService;
		private readonly ICommandManagerService _service;
	}
}
