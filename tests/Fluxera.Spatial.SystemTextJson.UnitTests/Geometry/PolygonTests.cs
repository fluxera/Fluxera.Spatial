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
		protected override string ModifyJson(string json)
		{
			return base.ModifyJson(json).Replace(".0", "");
		}

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
