using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands.OptionPages
{
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[Guid("DF0D89F1-C9A3-47BF-B277-42E0C178F1A0")]
	public class GeneralPage : DialogPage
	{
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

		private GeneralControl control;

		private bool formatOnSave;

		private bool removeAndSortUsingsOnSave;
	}
}
