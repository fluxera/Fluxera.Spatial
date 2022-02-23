namespace Fluxera.Spatial.MongoDB
{
	using System.Collections.Generic;
	using System.Linq;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;

	public sealed class LineStringSerializer : StructSerializerBase<LineString>
	{
		private readonly GeoJsonLineStringSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonLineStringSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonLineStringSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonLineStringSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, LineString value)
		{
			bool hasAltitude = value.Coordinates.Any(x => x.HasAltitude);
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
		public override LineString Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "LineString")
			{
				BsonArray coordinatesArray = document["coordinates"].AsBsonArray;

				IList<Position> positions = new List<Position>();
				foreach(BsonValue positionArrayValue in coordinatesArray)
				{
					BsonArray positionArray = positionArrayValue.AsBsonArray;
					Position position = positionArray.ReadPosition();
					positions.Add(position);
				}

				return new LineString(positions);
			}

			return LineString.Empty;
		}

		internal static GeoJsonLineString<GeoJson3DGeographicCoordinates> Create3D(LineString value)
		{
			IList<GeoJson3DGeographicCoordinates> geoPositions = new List<GeoJson3DGeographicCoordinates>();

			foreach(Position position in value.Coordinates)
			{
				geoPositions.Add(new GeoJson3DGeographicCoordinates(position.Longitude, position.Latitude, position.Altitude.GetValueOrDefault()));
			}

			GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates> geoLineStringCoordinates = new GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates>(geoPositions);
			GeoJsonLineString<GeoJson3DGeographicCoordinates> geoLineString = new GeoJsonLineString<GeoJson3DGeographicCoordinates>(geoLineStringCoordinates);

			return geoLineString;
		}

		internal static GeoJsonLineString<GeoJson2DGeographicCoordinates> Create2D(LineString value)
		{
			IList<GeoJson2DGeographicCoordinates> geoPositions = new List<GeoJson2DGeographicCoordinates>();

			foreach(Position position in value.Coordinates)
			{
				geoPositions.Add(new GeoJson2DGeographicCoordinates(position.Longitude, position.Latitude));
			}

			GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates> geoLineStringCoordinates = new GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>(geoPositions);
			GeoJsonLineString<GeoJson2DGeographicCoordinates> geoLineString = new GeoJsonLineString<GeoJson2DGeographicCoordinates>(geoLineStringCoordinates);

			return geoLineString;
		}
	}
}
