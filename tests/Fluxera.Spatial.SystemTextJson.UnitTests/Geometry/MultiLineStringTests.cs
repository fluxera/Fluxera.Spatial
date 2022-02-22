namespace Fluxera.Spatial.SystemTextJson.UnitTests.Geometry
{
	using System.Text.Json;
	using Fluxera.Spatial.UnitTests.Geometry;
	using NUnit.Framework;

	[TestFixture]
	public class MultiLineStringTests : MultiLineStringTestsBase
	{
		private JsonSerializerOptions options;

		/// <inheritdoc />
		protected override void OnSetup()
		{
			this.options = new JsonSerializerOptions
			{
				WriteIndented = true
			};
			this.options.UseSpatial();
		}

		/// <inheritdoc />
		protected override MultiLineString Deserialize(string jsonName)
		{
			return JsonSerializer.Deserialize<MultiLineString>(this.GetJson(jsonName), this.options);
		}

		/// <inheritdoc />
		protected override string Serialize(MultiLineString obj)
		{
			return JsonSerializer.Serialize(obj, this.options);
		}
	}
}
