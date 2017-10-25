using System.Threading.Tasks;
using Orleans;
using OrleansTest2.GrainInterfaces2;
using OrleansTest2.Services;
using Orleans.Providers;
using OrleansTest2.Grains2.States;

namespace OrleansTest2.Grains2
{
	[StorageProvider(ProviderName = "SqlServerStore")]
	public class Grain2 : Grain<PersonState>, IGrain2
	{
		private readonly IEmailService _emailService;

		public Grain2(IEmailService emailService)
		{
			_emailService = emailService;
		}

		public Task<string> GetPersonName()
		{
			return Task.FromResult(State.Name);
		}

		public Task SetPersonName(string name)
		{
			State.Name = name;
			return base.WriteStateAsync();
		}

		public Task<string> SayHello()
		{
			string msg = string.Empty;
			if (_emailService.Send("", "", "", "").Result)
			{
				msg = "Email sent...";
			}
			return Task.FromResult("Hello World!" + msg);
		}
	}
}
