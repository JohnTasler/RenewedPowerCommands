using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Microsoft.PowerCommands.OptionPages
{
	// Token: 0x02000018 RID: 24
	public class RowItem
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003EFD File Offset: 0x000020FD
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003F05 File Offset: 0x00002105
		[DisplayName("Command")]
		public string CommandText { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003F0E File Offset: 0x0000210E
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003F16 File Offset: 0x00002116
		public bool Enabled { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003F1F File Offset: 0x0000211F
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003F27 File Offset: 0x00002127
		[Browsable(false)]
		public CommandID Command { get; set; }
	}
}
