namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class PolygonTests : PolygonTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			JsonConvert.DefaultSettings = () =>
			{
				JsonSerializerSettings settings = new JsonSerializerSettings
				{
					Formatting = Formatting.None
				};
				settings.UseSpatial();
				return settings;
			};
		}

		/// <inheritdoc />
		protected override Polygon Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<Polygon>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(Polygon obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
