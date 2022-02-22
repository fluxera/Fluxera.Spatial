namespace Fluxera.Spatial.MongoDB.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class PolygonTests : PolygonTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseSpatial();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		/// <inheritdoc />
		protected override Polygon Deserialize(string jsonName)
		{
			return BsonSerializer.Deserialize<Polygon>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(Polygon obj)
		{
			return obj.ToJson();
		}
	}
}
