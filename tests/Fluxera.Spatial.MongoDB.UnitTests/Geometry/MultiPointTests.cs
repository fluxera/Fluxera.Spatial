namespace Fluxera.Spatial.MongoDB.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;
	using NUnit.Framework;

	[TestFixture]
	public class MultiPointTests : MultiPointTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			ConventionPack pack = new ConventionPack();
			pack.UseSpatial();
			ConventionRegistry.Register("ConventionPack", pack, t => true);
		}

		/// <inheritdoc />
		protected override MultiPoint Deserialize(string jsonName)
		{
			return BsonSerializer.Deserialize<MultiPoint>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(MultiPoint obj)
		{
			return obj.ToJson();
		}
	}
}
