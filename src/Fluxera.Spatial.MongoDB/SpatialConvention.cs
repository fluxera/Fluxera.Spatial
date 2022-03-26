namespace Fluxera.Spatial.MongoDB
{
	using System;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Conventions;

	internal sealed class SpatialConvention : ConventionBase, IMemberMapConvention
	{
		/// <inheritdoc />
		public void Apply(BsonMemberMap memberMap)
		{
			Type originalMemberType = memberMap.MemberType;
			Type memberType = Nullable.GetUnderlyingType(originalMemberType) ?? originalMemberType;

			IBsonSerializer? serializer = null;

			if(memberType == typeof(Point))
			{
				serializer = new PointSerializer();
			}
			else if(memberType == typeof(MultiPoint))
			{
				serializer = new MultiPointSerializer();
			}
			else if(memberType == typeof(LineString))
			{
				serializer = new LineStringSerializer();
			}
			else if(memberType == typeof(MultiLineString))
			{
				serializer = new MultiLineStringSerializer();
			}
			else if(memberType == typeof(Polygon))
			{
				serializer = new PolygonSerializer();
			}
			else if(memberType == typeof(MultiPolygon))
			{
				serializer = new MultiPolygonSerializer();
			}
			else if(memberType == typeof(GeometryCollection))
			{
				serializer = new GeometryCollectionSerializer();
			}

			if(serializer != null)
			{
				memberMap.SetSerializer(serializer);
			}
		}
	}
}
