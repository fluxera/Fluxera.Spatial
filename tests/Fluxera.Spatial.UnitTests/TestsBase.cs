namespace Fluxera.Spatial.UnitTests
{
	using System;
	using System.IO;
	using System.Reflection;
	using NUnit.Framework;

	public abstract class TestsBase<T>
		where T : struct, IGeometry
	{
		private static readonly Assembly Assembly = typeof(TestsBase<>).GetTypeInfo().Assembly;
		private static readonly string AssemblyName = Assembly.GetName().Name;

		[SetUp]
		public void Setup()
		{
			this.OnSetup();
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

			return this.ModifyJson(json);
		}

		protected virtual string ModifyJson(string json)
		{
			return json.Replace("\t", "  ");
		}

		protected abstract void OnSetup();

		protected abstract T Deserialize(string jsonName);

		protected abstract string Serialize(T obj);
	}
}
