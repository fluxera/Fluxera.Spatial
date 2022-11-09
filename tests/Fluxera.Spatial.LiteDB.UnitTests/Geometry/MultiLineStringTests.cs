namespace Fluxera.Spatial.LiteDB.UnitTests.Geometry
{
	using Fluxera.Spatial.UnitTests.Geometry;
	using global::LiteDB;
	using NUnit.Framework;

	[TestFixture]
	public class MultiLineStringTests : MultiLineStringTestsBase
	{
		/// <inheritdoc />
		protected override void OnSetup()
		{
			BsonMapper.Global.UseSpatial();
		}

		/// <inheritdoc />
		protected override MultiLineString Deserialize(string jsonName)
		{
			BsonDocument doc = (BsonDocument)JsonSerializer.Deserialize(this.GetJson(jsonName));
			return BsonMapper.Global.ToObject<MultiLineString>(doc);
		}

		/// <inheritdoc />
		protected override string Serialize(MultiLineString obj)
		{
			BsonDocument doc = BsonMapper.Global.ToDocument(obj);
			return JsonSerializer.Serialize(doc);
		}
	}
}
