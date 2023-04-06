using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Tasler.RenewedPowerCommands.Commands;

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
			_menuCommandService.AddCommand(new CollapseProjectsCommand(_serviceProvider));
			_menuCommandService.AddCommand(new RemoveSortUsingsCommand(_serviceProvider));
			_menuCommandService.AddCommand(new CopyClassCommand(_serviceProvider));
			_menuCommandService.AddCommand(new PasteClassCommand(_serviceProvider));
		}

		private readonly IServiceProvider _serviceProvider;
		private readonly OleMenuCommandService _menuCommandService;
	}
}
