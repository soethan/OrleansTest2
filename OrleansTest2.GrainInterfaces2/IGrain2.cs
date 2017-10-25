using System.Threading.Tasks;
using Orleans;

namespace OrleansTest2.GrainInterfaces2
{
	/// <summary>
	/// Grain interface IGrain2
	/// </summary>
	public interface IGrain2 : IGrainWithIntegerKey
	{
		Task<string> SayHello();
		Task<string> GetPersonName();
		Task SetPersonName(string name);
	}
}
