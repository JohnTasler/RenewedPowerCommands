using System;
using System.Collections;
using System.Collections.Generic;
using EnvDTE;
using Microsoft.VisualStudio.Shell;

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

			foreach (object obj in _solution.Projects)
			{
				Project project = (Project)obj;
				if (project.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
				{
					yield return project;
				}
				else
				{
					foreach (Project subProject in this.Enumerate(project))
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

			foreach (object obj in project.ProjectItems)
			{
				ProjectItem projectItem = (ProjectItem)obj;
				if (projectItem.Object is Project itemProject)
				{
					if (itemProject.Kind != "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}")
					{
						yield return itemProject;
					}
					else
					{
						foreach (Project project2 in this.Enumerate(itemProject))
						{
							yield return project2;
						}
					}
				}
			}
		}

		private readonly Solution _solution;
	}
}
