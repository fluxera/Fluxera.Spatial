namespace Fluxera.Spatial.SystemTextJson.UnitTests.Geometry
{
	using System.Text.Json;
	using Fluxera.Spatial.UnitTests.Geometry;
	using NUnit.Framework;

	[TestFixture]
	public class LineStringTests : LineStringTestsBase
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
		protected override LineString Deserialize(string jsonName)
		{
			return JsonSerializer.Deserialize<LineString>(this.GetJson(jsonName), this.options);
		}

		/// <inheritdoc />
		protected override string Serialize(LineString obj)
		{
			return JsonSerializer.Serialize(obj, this.options);
		}
	}
}
