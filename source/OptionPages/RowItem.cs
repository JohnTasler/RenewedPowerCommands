using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	public class RowItem
	{
		[DisplayName("Command")]
		public string CommandText { get; set; }

		public bool Enabled { get; set; }

		[Browsable(false)]
		public CommandID Command { get; set; }
	}
}
