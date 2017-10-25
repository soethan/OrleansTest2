using System.Threading.Tasks;

namespace OrleansTest2.Services
{
	public interface IEmailService
	{
		Task<bool> Send(string from, string to, string subject, string body);
	}
}
