namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class LineStringTests : LineStringTestsBase
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
		protected override LineString Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<LineString>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(LineString obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
