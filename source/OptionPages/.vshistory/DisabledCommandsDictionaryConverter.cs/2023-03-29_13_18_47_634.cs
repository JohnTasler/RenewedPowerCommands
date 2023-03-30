using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.PowerCommands.Extensions;

namespace Microsoft.PowerCommands.OptionPages
{
	// Token: 0x02000015 RID: 21
	public class DisabledCommandsDictionaryConverter : StringConverter
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003A4E File Offset: 0x00001C4E
		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003A4E File Offset: 0x00001C4E
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{
			return true;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003A54 File Offset: 0x00001C54
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			this.disabledCommands = (value as IList<CommandID>);
			StringBuilder builder = new StringBuilder();
			this.disabledCommands.ForEach(delegate(CommandID cmdId)
			{
				builder.Append(string.Format("{0},{1};", cmdId.Guid, cmdId.ID));
			});
			return builder.ToString();
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003AA0 File Offset: 0x00001CA0
		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			this.disabledCommands = new List<CommandID>();
			try
			{
				if (!string.IsNullOrEmpty(value.ToString()))
				{
					Guid cmdGuid;
					int cmdId;
					value.ToString().Split(new char[]
					{
						';'
					}).ForEach(delegate(string item)
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

		// Token: 0x0400002A RID: 42
		private IList<CommandID> disabledCommands;
	}
}
