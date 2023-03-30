using Microsoft.VisualStudio;

using Tasler.RenewedPowerCommands.Common;

namespace Tasler.RenewedPowerCommands.Extensions
{
	internal static class CommandInterceptorExtensions
	{
		public static CommandInterceptor SetCommandId(this CommandInterceptor @this, VSConstants.VSStd97CmdID id)
		{
			@this.CommandGuid = typeof(VSConstants.VSStd97CmdID).GUID.ToString("B");
			@this.CommandId = (int)id;
			return @this;
		}
	}
}
