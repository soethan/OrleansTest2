using System;

using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using OrleansTest2.GrainInterfaces2;
using Orleans.Storage;
using System.Collections.Generic;

namespace OrleansTest2.SiloHost2
{
	/// <summary>
	/// Orleans test silo host
	/// </summary>
	public class Program
	{
		static void Main(string[] args)
		{
			// First, configure and start a local silo
			var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
			// *** For Dependency Injection in Silo ***
			siloConfig.UseStartupType<StartUp>();

			var properties = new Dictionary<string, string>()
			{
				["AdoInvariant"] = "System.Data.SqlClient",
				["DataConnectionString"] = @"Server=.\SQLExpress;Database=OrleansTestDb1;Persist Security Info=True;integrated security=SSPI;",
				["UseJsonFormat"] = "true"
			};
			siloConfig.Globals.RegisterStorageProvider<AdoNetStorageProvider>("SqlServerStore", properties);

			var silo = new SiloHost("TestSilo", siloConfig);
			silo.InitializeOrleansSilo();
			silo.StartOrleansSilo();

			Console.WriteLine("Silo started.");
			Console.WriteLine("Waiting for Orleans Silo to start. Press Enter to proceed...");
			Console.ReadLine();

			// Then configure and connect a client.
			var clientConfig = ClientConfiguration.LocalhostSilo();
			var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
			client.Connect().Wait();

			Console.WriteLine("Client connected.");

			var helloGrain = client.GetGrain<IGrain2>(1);
			//var helloGrain = GrainClient.GrainFactory.GetGrain<IGrain2>(0);
			Console.WriteLine("\n\n{0}\n\n", helloGrain.SayHello().Result);
			Console.WriteLine($"Before setting PersonName = {helloGrain.GetPersonName().Result}");
			helloGrain.SetPersonName("Michael");
			Console.WriteLine($"After setting PersonName = {helloGrain.GetPersonName().Result}");

			Console.WriteLine("\nPress Enter to terminate...");
			Console.ReadLine();

			// Shut down
			client.Close();
			silo.ShutdownOrleansSilo();
		}
	}
}
