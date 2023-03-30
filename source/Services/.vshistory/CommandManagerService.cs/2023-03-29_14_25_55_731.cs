using System;
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
		// Token: 0x0600002D RID: 45 RVA: 0x00002D0B File Offset: 0x00000F0B
		public CommandManagerService()
		{
			this.registeredCommands = new List<OleMenuCommand>();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D20 File Offset: 0x00000F20
		public void RegisterCommand(OleMenuCommand command)
		{
			if (this.registeredCommands.SingleOrDefault((OleMenuCommand cmd) => cmd.CommandID.Guid.Equals(command.CommandID.Guid) && cmd.CommandID.ID.Equals(command.CommandID.ID)) == null)
			{
				this.registeredCommands.Add(command);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002D64 File Offset: 0x00000F64
		public void UnRegisterCommand(OleMenuCommand command)
		{
			if (this.registeredCommands.SingleOrDefault((OleMenuCommand cmd) => cmd.CommandID.Guid.Equals(command.CommandID.Guid) && cmd.CommandID.ID.Equals(command.CommandID.ID)) != null)
			{
				this.registeredCommands.Remove(command);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002DA9 File Offset: 0x00000FA9
		public IEnumerable<OleMenuCommand> GetRegisteredCommands()
		{
			return new ReadOnlyCollection<OleMenuCommand>(this.registeredCommands);
		}

		// Token: 0x04000015 RID: 21
		private IList<OleMenuCommand> registeredCommands;
	}
}
