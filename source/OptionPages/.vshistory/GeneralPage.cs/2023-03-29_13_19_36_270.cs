using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace Microsoft.PowerCommands.OptionPages
{
	// Token: 0x02000017 RID: 23
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("DF0D89F1-C9A3-47BF-B277-42E0C178F1A0")]
	public class GeneralPage : DialogPage
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003EA2 File Offset: 0x000020A2
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003EAA File Offset: 0x000020AA
		public bool FormatOnSave
		{
			get
			{
				return this.formatOnSave;
			}
			set
			{
				this.formatOnSave = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003EB3 File Offset: 0x000020B3
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003EBB File Offset: 0x000020BB
		public bool RemoveAndSortUsingsOnSave
		{
			get
			{
				return this.removeAndSortUsingsOnSave;
			}
			set
			{
				this.removeAndSortUsingsOnSave = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003EC4 File Offset: 0x000020C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				this.control = new GeneralControl();
				this.control.Location = new Point(0, 0);
				this.control.OptionPage = this;
				return this.control;
			}
		}

		// Token: 0x04000030 RID: 48
		private GeneralControl control;

		// Token: 0x04000031 RID: 49
		private bool formatOnSave;

		// Token: 0x04000032 RID: 50
		private bool removeAndSortUsingsOnSave;
	}
}
