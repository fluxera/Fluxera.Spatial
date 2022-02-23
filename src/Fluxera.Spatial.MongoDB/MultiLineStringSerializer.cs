namespace Fluxera.Spatial.MongoDB
{
	using System.Collections.Generic;
	using System.Linq;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;

	public sealed class MultiLineStringSerializer : StructSerializerBase<MultiLineString>
	{
		private readonly GeoJsonMultiLineStringSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonMultiLineStringSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonMultiLineStringSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonMultiLineStringSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, MultiLineString value)
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
		public override MultiLineString Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "MultiLineString")
			{
				BsonArray coordinatesArray = document["coordinates"].AsBsonArray;

				IList<LineString> lineStrings = new List<LineString>();
				foreach(BsonValue lineStringArrayValue in coordinatesArray)
				{
					BsonArray lineStringArray = lineStringArrayValue.AsBsonArray;

					IList<Position> positions = new List<Position>();
					foreach(BsonValue positionArrayValue in lineStringArray)
					{
						BsonArray positionArray = positionArrayValue.AsBsonArray;
						Position position = positionArray.ReadPosition();
						positions.Add(position);
					}

					lineStrings.Add(new LineString(positions));
				}

				return new MultiLineString(lineStrings);
			}

			return MultiLineString.Empty;
		}

		internal static GeoJsonMultiLineString<GeoJson3DGeographicCoordinates> Create3D(MultiLineString value)
		{
			IList<GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates>> geoLineStringCoordinates = new List<GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates>>();

			foreach(LineString lineString in value.Coordinates)
			{
				IList<GeoJson3DGeographicCoordinates> geoPositions = new List<GeoJson3DGeographicCoordinates>();

				foreach(Position position in lineString.Coordinates)
				{
					geoPositions.Add(new GeoJson3DGeographicCoordinates(position.Longitude, position.Latitude, position.Altitude.GetValueOrDefault()));
				}

				GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates> geoLineStringCoordinate = new GeoJsonLineStringCoordinates<GeoJson3DGeographicCoordinates>(geoPositions);
				geoLineStringCoordinates.Add(geoLineStringCoordinate);
			}

			GeoJsonMultiLineStringCoordinates<GeoJson3DGeographicCoordinates> geoMultiLineStringCoordinates = new GeoJsonMultiLineStringCoordinates<GeoJson3DGeographicCoordinates>(geoLineStringCoordinates);
			GeoJsonMultiLineString<GeoJson3DGeographicCoordinates> geoMultiLineString = new GeoJsonMultiLineString<GeoJson3DGeographicCoordinates>(geoMultiLineStringCoordinates);

			return geoMultiLineString;
		}

		internal static GeoJsonMultiLineString<GeoJson2DGeographicCoordinates> Create2D(MultiLineString value)
		{
			IList<GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>> geoLineStringCoordinates = new List<GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>>();

			foreach(LineString lineString in value.Coordinates)
			{
				IList<GeoJson2DGeographicCoordinates> geoPositions = new List<GeoJson2DGeographicCoordinates>();

				foreach(Position position in lineString.Coordinates)
				{
					geoPositions.Add(new GeoJson2DGeographicCoordinates(position.Longitude, position.Latitude));
				}

				GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates> geoLineStringCoordinate = new GeoJsonLineStringCoordinates<GeoJson2DGeographicCoordinates>(geoPositions);
				geoLineStringCoordinates.Add(geoLineStringCoordinate);
			}

			GeoJsonMultiLineStringCoordinates<GeoJson2DGeographicCoordinates> geoMultiLineStringCoordinates = new GeoJsonMultiLineStringCoordinates<GeoJson2DGeographicCoordinates>(geoLineStringCoordinates);
			GeoJsonMultiLineString<GeoJson2DGeographicCoordinates> geoMultiLineString = new GeoJsonMultiLineString<GeoJson2DGeographicCoordinates>(geoMultiLineStringCoordinates);

			return geoMultiLineString;
		}
	}
}
