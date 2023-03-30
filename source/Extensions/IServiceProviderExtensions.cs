using System;

namespace Tasler.RenewedPowerCommands.Extensions
{
	public static class IServiceProviderExtensions
	{
		public static TService GetService<TService>(this IServiceProvider serviceProvider)
		{
			return (TService)serviceProvider.GetService(typeof(TService));
		}

		public static TService TryGetService<TService>(this IServiceProvider serviceProvider)
		{
			var service = serviceProvider.GetService(typeof(TService));
			if (service == null)
			{
				return default(TService);
			}
			return (TService)service;
		}

		public static TInterface TryGetService<SInterface, TInterface>(this IServiceProvider serviceProvider)
		{
			var service = serviceProvider.GetService(typeof(SInterface));
			if (service == null)
			{
				return default(TInterface);
			}
			return (TInterface)service;
		}
	}
}
