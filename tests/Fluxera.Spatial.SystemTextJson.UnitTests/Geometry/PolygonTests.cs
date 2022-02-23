namespace Fluxera.Spatial.SystemTextJson.UnitTests.Geometry
{
	using System.Text.Json;
	using Fluxera.Spatial.UnitTests.Geometry;
	using NUnit.Framework;

	[TestFixture]
	public class PolygonTests : PolygonTestsBase
	{
		private JsonSerializerOptions options;

		/// <inheritdoc />
		protected override void OnSetup()
		{
			this.options = new JsonSerializerOptions
			{
				WriteIndented = false
			};
			this.options.UseSpatial();
		}

		/// <inheritdoc />
		protected override string ModifyJson(string json)
		{
			json = base.ModifyJson(json);
			json = json.Replace(".0", "");
			return json;
		}

		/// <inheritdoc />
		protected override Polygon Deserialize(string jsonName)
		{
			return JsonSerializer.Deserialize<Polygon>(this.GetJson(jsonName), this.options);
		}

		/// <inheritdoc />
		protected override string Serialize(Polygon obj)
		{
			return JsonSerializer.Serialize(obj, this.options);
		}
	}
}
