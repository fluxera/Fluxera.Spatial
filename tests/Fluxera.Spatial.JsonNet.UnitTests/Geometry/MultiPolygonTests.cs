namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class MultiPolygonTests : MultiPolygonTestsBase
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
		protected override MultiPolygon Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<MultiPolygon>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(MultiPolygon obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
