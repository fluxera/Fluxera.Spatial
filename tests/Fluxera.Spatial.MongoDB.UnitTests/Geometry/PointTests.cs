namespace Fluxera.Spatial.MongoDB.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.IO;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class PointTests : PointTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseSpatial();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		/// <inheritdoc />
		protected override Point Deserialize(string jsonName)
		{
			string json = this.GetJson(jsonName);
			json = "{\"Property\":" + json + "}";

			TestEntity<Point> testEntity = BsonSerializer.Deserialize<TestEntity<Point>>(json);
			return testEntity.Property;
		}

		/// <inheritdoc />
		protected override string Serialize(Point obj)
		{
			string json = new TestEntity<Point>(obj).ToJson(new JsonWriterSettings
			{
				Indent = false
			});
			json = this.Minify(json);
			json = json.Replace("{\"Property\":", "");
			json = json.Remove(json.Length - 1);
			return json;
		}
	}
}
