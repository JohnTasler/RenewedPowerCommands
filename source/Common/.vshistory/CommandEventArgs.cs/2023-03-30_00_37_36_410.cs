using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasler.RenewedPowerCommands.Common
{
	internal class CommandEventArgs : EventArgs
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
