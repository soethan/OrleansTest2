using System;
using Microsoft.Extensions.DependencyInjection;
using OrleansTest2.Services;

namespace OrleansTest2.SiloHost2
{
	public class StartUp
	{
		public IServiceProvider ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IEmailService, EmailService>();

			return services.BuildServiceProvider();
		}
	}
}
