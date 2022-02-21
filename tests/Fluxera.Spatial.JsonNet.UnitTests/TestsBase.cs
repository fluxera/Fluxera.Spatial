namespace Fluxera.Spatial.JsonNet.UnitTests
{
	using System;
	using System.IO;
	using System.Reflection;
	using Newtonsoft.Json;
	using NUnit.Framework;

	public abstract class TestsBase<T>
		where T : struct, IGeometry
	{
		private static readonly Assembly Assembly = typeof(TestsBase<>).GetTypeInfo().Assembly;
		private static readonly string AssemblyName = Assembly.GetName().Name;

		[SetUp]
		public void Setup()
		{
			JsonConvert.DefaultSettings = () =>
			{
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					Formatting = Formatting.Indented,
				};
				settings.UseSpatial();
				return settings;
			};
		}

		protected string GetJson(string name)
		{
			Type type = typeof(T);
			string path = $"{AssemblyName}.json.{type.Name}.{name}.json";

			string json = string.Empty;

			using(Stream resourceStream = Assembly.GetManifestResourceStream(path)!)
			{
				using(StreamReader reader = new StreamReader(resourceStream))
				{
					json = reader.ReadToEnd();
				}
			}

			return json.Replace("\t", "  ");
		}
	}
}
