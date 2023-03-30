using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.PowerCommands.Extensions;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	public class DisabledCommandsDictionaryConverter : StringConverter
	{
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return true;
		}

		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return true;
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			this.disabledCommands = (value as IList<CommandID>);
			StringBuilder builder = new StringBuilder();
			foreach (var commandId in disabledCommands)
			{
				builder.Append($"{commandId.Guid},{commandId.ID}");
			}
			return builder.ToString();
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			this.disabledCommands = new List<CommandID>();
			try
			{
				if (!string.IsNullOrEmpty(value.ToString()))
				{
					Guid cmdGuid;
					int cmdId;
					foreach (var commandItem in value.ToString().Split(';'))
					{
						string[] array = item.Split(new char[]
						{
							','
						});
						if (array.Count<string>() == 2)
						{
							cmdGuid = new Guid(array[0]);
							int.TryParse(array[1], out cmdId);
							this.disabledCommands.Add(new CommandID(cmdGuid, cmdId));
						}
					});
				}
			}
			catch
			{
			}
			return this.disabledCommands;
		}

		private IList<CommandID> disabledCommands;
	}
}
