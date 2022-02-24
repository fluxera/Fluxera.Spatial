//namespace Fluxera.Spatial.LiteDB.UnitTests.Geometry
//{
//	using Fluxera.Spatial.UnitTests.Geometry;
//	using NUnit.Framework;

//	[TestFixture]
//	public class GeometryCollectionTests : GeometryCollectionTestsBase
//	{
//		/// <inheritdoc />
//		protected override void OnSetup()
//		{
//			ConventionPack pack = new ConventionPack();
//			pack.UseSpatial();
//			ConventionRegistry.Register("ConventionPack", pack, t => true);
//		}

//		/// <inheritdoc />
//		protected override GeometryCollection Deserialize(string jsonName)
//		{
//			string json = this.GetJson(jsonName);
//			json = "{\"Property\":" + json + "}";

//			TestEntity<GeometryCollection> testEntity = BsonSerializer.Deserialize<TestEntity<GeometryCollection>>(json);
//			return testEntity.Property;
//		}

//		/// <inheritdoc />
//		protected override string Serialize(GeometryCollection obj)
//		{
//			string json = new TestEntity<GeometryCollection>(obj).ToJson(new JsonWriterSettings
//			{
//				Indent = false
//			});
//			json = this.Minify(json);
//			json = json.Replace("{\"Property\":", "");
//			json = json.Remove(json.Length - 1);
//			return json;
//		}
//	}
//}


