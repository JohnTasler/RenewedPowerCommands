using System;

namespace Tasler.RenewedPowerCommands.Extensions
{
	public static class IServiceProviderExtensions
	{
		public static TService GetService<TService>(this IServiceProvider serviceProvider)
		{
			return (TService)serviceProvider.GetService(typeof(TService));
		}

		public static TInterface GetService<SInterface, TInterface>(this IServiceProvider serviceProvider)
		{
			return (TInterface)serviceProvider.GetService(typeof(SInterface));
		}

		public static TService TryGetService<TService>(this IServiceProvider serviceProvider)
		{
			object service = serviceProvider.GetService(typeof(TService));
			if (service == null)
			{
				return default(TService);
			}
			return (TService)((object)service);
		}

		public static TInterface TryGetService<SInterface, TInterface>(this IServiceProvider serviceProvider)
		{
			object service = serviceProvider.GetService(typeof(SInterface));
			if (service == null)
			{
				return default(TInterface);
			}
			return (TInterface)((object)service);
		}
	}
}
