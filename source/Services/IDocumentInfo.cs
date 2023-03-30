
namespace Tasler.RenewedPowerCommands.Services
{
	public interface IDocumentInfo
	{
		string DocumentPath { get; set; }

		int CursorLine { get; set; }

		int CursorColumn { get; set; }

		string DocumentViewKind { get; set; }
	}
}
