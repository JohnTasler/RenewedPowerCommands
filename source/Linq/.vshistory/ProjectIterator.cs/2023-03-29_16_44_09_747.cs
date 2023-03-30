using EnvDTE;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tasler.RenewedPowerCommands.Linq
{
	public sealed class ProjectIterator : IEnumerable<Project>, IEnumerable
	{
		public ProjectIterator(Solution solution)
		{
			_solution = solution ?? throw new ArgumentNullException(nameof(solution));
		}

		public IEnumerator<Project> GetEnumerator()
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var project in _solution.Projects.OfType<Project>())
			{
				if (project.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				{
					yield return project;
				}
				else
				{
					foreach (var subProject in this.Enumerate(project))
					{
						yield return subProject;
					}
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		private IEnumerable<Project> Enumerate(Project project)
		{
			ThreadHelper.ThrowIfNotOnUIThread();

			foreach (var projectItem in project.ProjectItems.OfType<ProjectItem>())
			{
				if (projectItem.Object is Project itemProject)
				{
					if (itemProject.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
					{
						yield return itemProject;
					}
					else
					{
						foreach (var subProject in this.Enumerate(itemProject))
						{
							yield return subProject;
						}
					}
				}
			}
		}

		private readonly Solution _solution;
	}
}
