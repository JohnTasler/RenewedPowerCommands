using System;

namespace Tasler.RenewedPowerCommands.Common
{
	public class CommandEventArgs : EventArgs
	{
		public string CommandGuid { get; private set; }

		public int CommandId { get; private set; }

		public CommandEventArgs(string commandGuid, int commandId)
		{
			this.CommandGuid = commandGuid;
			this.CommandId = commandId;
		}
	}
}
