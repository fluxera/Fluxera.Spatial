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
			return BsonSerializer.Deserialize<Point>(this.GetJson(jsonName))!;
		}

		/// <inheritdoc />
		protected override string Serialize(Point obj)
		{
			return new PointEntity(obj).ToJson(new JsonWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				OutputMode = JsonOutputMode.RelaxedExtendedJson
			});
		}
	}

	public class PointEntity
	{
		public PointEntity(Point point)
		{
			this.Point = point;
		}

		public Point Point { get; set; }
	}
}
