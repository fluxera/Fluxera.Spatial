namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class MultiLineStringTests : MultiLineStringTestsBase
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
		protected override MultiLineString Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<MultiLineString>(this.GetJson(jsonName));
		}

		/// <inheritdoc />
		protected override string Serialize(MultiLineString obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
