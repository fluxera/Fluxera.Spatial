namespace Fluxera.Spatial.JsonNet.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using Newtonsoft.Json;
	using NUnit.Framework;

	[TestFixture]
	public class PointTests : PointTestsBase
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
		protected override Point Deserialize(string jsonName)
		{
			return JsonConvert.DeserializeObject<Point>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(Point obj)
		{
			return JsonConvert.SerializeObject(obj);
		}
	}
}
