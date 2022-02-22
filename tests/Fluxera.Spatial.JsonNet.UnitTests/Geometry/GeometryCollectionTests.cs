namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class GeometryCollectionTests : GeometryCollectionTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
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

		/// <inheritdoc />
		protected override GeometryCollection Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<GeometryCollection>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(GeometryCollection obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
