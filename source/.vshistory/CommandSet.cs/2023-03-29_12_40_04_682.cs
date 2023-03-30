using System;
using System.ComponentModel.Design;
//using Microsoft.PowerCommands.Commands;
//using Microsoft.PowerCommands.Extensions;
//using Microsoft.PowerCommands.Services;
using Microsoft.VisualStudio.Shell;

namespace Tasler.RenewedPowerCommands
{
	internal class CommandSet
	{
		public CommandSet(IServiceProvider provider)
		{
			this.serviceProvider = provider;
			this.menuCommandService = this.serviceProvider.GetService<IMenuCommandService, OleMenuCommandService>();
		}

		public void Initialize()
		{
			this.RegisterMenuCommands();
		}

		private void RegisterMenuCommands()
		{
			ICommandManagerService service = this.serviceProvider.GetService<SCommandManagerService, ICommandManagerService>();
			OleMenuCommand command = new OpenVSCommandPromptCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command);
			service.RegisterCommand(command);
			OleMenuCommand command2 = new TransformTemplatesCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command2);
			service.RegisterCommand(command2);
			OleMenuCommand command3 = new CollapseProjectsCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command3);
			service.RegisterCommand(command3);
			OleMenuCommand command4 = new OpenContainingFolderCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command4);
			service.RegisterCommand(command4);
			OleMenuCommand command5 = new ExtractToConstantCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command5);
			service.RegisterCommand(command5);
			OleMenuCommand command6 = new EditProjectFileCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command6);
			service.RegisterCommand(command6);
			OleMenuCommand command7 = new CopyAsProjectReferenceCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command7);
			service.RegisterCommand(command7);
			OleMenuCommand command8 = new CopyReferenceCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command8);
			service.RegisterCommand(command8);
			OleMenuCommand command9 = new CopyReferencesCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command9);
			service.RegisterCommand(command9);
			OleMenuCommand command10 = new PasteReferenceCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command10);
			service.RegisterCommand(command10);
			OleMenuCommand command11 = new ClearRecentFileListCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command11);
			service.RegisterCommand(command11);
			OleMenuCommand command12 = new ClearRecentProjectListCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command12);
			service.RegisterCommand(command12);
			OleMenuCommand command13 = new ReloadProjectsCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command13);
			service.RegisterCommand(command13);
			OleMenuCommand command14 = new UnloadProjectsCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command14);
			service.RegisterCommand(command14);
			OleMenuCommand command15 = new RemoveSortUsingsCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command15);
			service.RegisterCommand(command15);
			OleMenuCommand command16 = new CopyClassCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command16);
			service.RegisterCommand(command16);
			OleMenuCommand command17 = new PasteClassCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command17);
			service.RegisterCommand(command17);
			OleMenuCommand command18 = new CopyPathCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command18);
			service.RegisterCommand(command18);
			OleMenuCommand command19 = new EmailCodeSnippetCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command19);
			service.RegisterCommand(command19);
			OleMenuCommand command20 = new InsertGuidAttributeCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command20);
			service.RegisterCommand(command20);
			OleMenuCommand command21 = new UndoCloseCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command21);
			service.RegisterCommand(command21);
			OleMenuCommand command22 = new ClearAllPanesCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command22);
			service.RegisterCommand(command22);
			OleMenuCommand command23 = new RecentlyClosedDocumentsListCommand(this.serviceProvider);
			this.menuCommandService.AddCommand(command23);
			service.RegisterCommand(command23);
		}

		// Token: 0x04000001 RID: 1
		private IServiceProvider serviceProvider;

		// Token: 0x04000002 RID: 2
		private OleMenuCommandService menuCommandService;
	}
}
