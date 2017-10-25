using System.Threading.Tasks;

namespace OrleansTest2.Services
{
	public class EmailService : IEmailService
	{
		public Task<bool> Send(string from, string to, string subject, string body)
		{
			return Task.FromResult(true);
		}
	}
}
