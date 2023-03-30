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
		public bool FormatOnSave { get; set; }

		public bool RemoveAndSortUsingsOnSave { get; set; }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected override IWin32Window Window
		{
			get
			{
				_control = new GeneralControl();
				_control.Location = new Point(0, 0);
				_control.OptionPage = this;
				return _control;
			}
		}

		private GeneralControl _control;
	}
}
