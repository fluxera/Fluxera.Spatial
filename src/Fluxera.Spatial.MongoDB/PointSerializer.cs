namespace Fluxera.Spatial.MongoDB
{
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;

	public sealed class PointSerializer : StructSerializerBase<Point>
	{
		private readonly GeoJsonPointSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonPointSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonPointSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonPointSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Point value)
		{
			bool hasAltitude = value.HasAltitude;
			if(hasAltitude)
			{
				this.serializerWithAltitude.Serialize(context, args, Create3D(value));
			}
			else
			{
				this.serializer.Serialize(context, args, Create2D(value));
			}
		}

		/// <inheritdoc />
		public override Point Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "Point")
			{
				BsonArray coordinatesArray = document["coordinates"].AsBsonArray;

				Position position = coordinatesArray.ReadPosition();
				if(position != Position.Empty)
				{
					return new Point(position);
				}
			}

			return Point.Empty;
		}

		internal static GeoJsonPoint<GeoJson3DGeographicCoordinates> Create3D(Point value)
		{
			GeoJson3DGeographicCoordinates coordinates = new GeoJson3DGeographicCoordinates(value.Longitude, value.Latitude, value.Altitude.GetValueOrDefault());
			GeoJsonPoint<GeoJson3DGeographicCoordinates> geoPoint = new GeoJsonPoint<GeoJson3DGeographicCoordinates>(coordinates);
			return geoPoint;
		}

		internal static GeoJsonPoint<GeoJson2DGeographicCoordinates> Create2D(Point value)
		{
			GeoJson2DGeographicCoordinates coordinates = new GeoJson2DGeographicCoordinates(value.Longitude, value.Latitude);
			GeoJsonPoint<GeoJson2DGeographicCoordinates> geoPoint = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(coordinates);
			return geoPoint;
		}
	}
}
