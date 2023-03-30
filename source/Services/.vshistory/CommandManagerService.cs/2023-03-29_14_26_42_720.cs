﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.Services
{
	[Guid("FA4AA647-22F8-4974-8520-F1CD552A3C8B")]
	[ComVisible(true)]
	public interface ICommandManagerService
	{
		void RegisterCommand(OleMenuCommand command);

		void UnRegisterCommand(OleMenuCommand command);

		IEnumerable<OleMenuCommand> GetRegisteredCommands();
	}

	[Guid("395C5E1E-4074-4C39-A814-16E94158E51C")]
	public interface SCommandManagerService
	{
	}

	internal class CommandManagerService : ICommandManagerService, SCommandManagerService
	{
		public CommandManagerService()
		{
		}

		public void RegisterCommand(OleMenuCommand command)
		{
			if (this.registeredCommands.SingleOrDefault((OleMenuCommand cmd) => cmd.CommandID.Guid.Equals(command.CommandID.Guid) && cmd.CommandID.ID.Equals(command.CommandID.ID)) == null)
			{
				this.registeredCommands.Add(command);
			}
		}

		public void UnRegisterCommand(OleMenuCommand command)
		{
			if (this.registeredCommands.SingleOrDefault((OleMenuCommand cmd) => cmd.CommandID.Guid.Equals(command.CommandID.Guid) && cmd.CommandID.ID.Equals(command.CommandID.ID)) != null)
			{
				this.registeredCommands.Remove(command);
			}
		}

		public IEnumerable<OleMenuCommand> GetRegisteredCommands()
		{
			return new ReadOnlyCollection<OleMenuCommand>(this.registeredCommands);
		}

		private IList<OleMenuCommand> registeredCommands = new List<OleMenuCommand>();
	}
}