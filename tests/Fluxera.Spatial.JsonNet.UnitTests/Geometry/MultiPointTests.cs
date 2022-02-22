namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class MultiPointTests : MultiPointTestsBase
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
		protected override MultiPoint Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<MultiPoint>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(MultiPoint obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
