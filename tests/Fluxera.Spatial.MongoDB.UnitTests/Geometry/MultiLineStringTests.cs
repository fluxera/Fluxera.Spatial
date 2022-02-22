namespace Fluxera.Spatial.MongoDB.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class MultiLineStringTests : MultiLineStringTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseSpatial();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		/// <inheritdoc />
		protected override MultiLineString Deserialize(string jsonName)
		{
			return BsonSerializer.Deserialize<MultiLineString>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(MultiLineString obj)
		{
			return obj.ToJson();
		}
	}
}
