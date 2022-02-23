namespace Fluxera.Spatial.UnitTests
{
	using System;
	using System.IO;
	using System.Reflection;
	using System.Text.RegularExpressions;
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

			string json;

			using(Stream resourceStream = Assembly.GetManifestResourceStream(path)!)
			{
				using(StreamReader reader = new StreamReader(resourceStream))
				{
					json = reader.ReadToEnd();
				}
			}

			json = this.Minify(json);
			json = this.ModifyJson(json);

			return json;
		}

		protected virtual string ModifyJson(string json)
		{
			return json;
		}

		protected string Minify(string json)
		{
			return Regex.Replace(json, @"(""(?:[^""\\]|\\.)*"")|\s+", "$1", RegexOptions.Compiled);
		}

		protected abstract void OnSetup();

		protected abstract T Deserialize(string jsonName);

		protected abstract string Serialize(T obj);
	}
}
