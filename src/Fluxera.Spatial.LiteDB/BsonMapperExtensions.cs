namespace Fluxera.Spatial.LiteDB
{
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class BsonMapperExtensions
	{
		public static BsonMapper UseSpatial(this BsonMapper mapper)
		{
			mapper.RegisterType(typeof(Point),
				PointSerializer.Serialize,
				PointSerializer.Deserialize);

			mapper.RegisterType(typeof(MultiPoint),
				MultiPointSerializer.Serialize,
				MultiPointSerializer.Deserialize);

			mapper.RegisterType(typeof(LineString),
				LineStringSerializer.Serialize,
				LineStringSerializer.Deserialize);

			mapper.RegisterType(typeof(MultiLineString),
				MultiLineStringSerializer.Serialize,
				MultiLineStringSerializer.Deserialize);

			mapper.RegisterType(typeof(Polygon),
				PolygonSerializer.Serialize,
				PolygonSerializer.Deserialize);

			mapper.RegisterType(typeof(MultiPolygon),
				MultiPolygonSerializer.Serialize,
				MultiPolygonSerializer.Deserialize);

			return mapper;
		}
	}
}
