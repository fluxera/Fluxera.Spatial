namespace Fluxera.Spatial.MongoDB
{
	using System.Collections.Generic;
	using System.Linq;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;

	public sealed class PolygonSerializer : StructSerializerBase<Polygon>
	{
		private readonly GeoJsonPolygonSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonPolygonSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonPolygonSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonPolygonSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Polygon value)
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
		public override Polygon Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "Polygon")
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

				return new Polygon(lineStrings);
			}

			return Polygon.Empty;
		}

		internal static GeoJsonPolygon<GeoJson3DGeographicCoordinates> Create3D(Polygon value)
		{
			IList<GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>> geoLinearRingCoordinates = new List<GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>>();

			foreach(LineString lineString in value.Coordinates)
			{
				IList<GeoJson3DGeographicCoordinates> geoPositions = new List<GeoJson3DGeographicCoordinates>();

				foreach(Position position in lineString.Coordinates)
				{
					geoPositions.Add(new GeoJson3DGeographicCoordinates(position.Longitude, position.Latitude, position.Altitude.GetValueOrDefault()));
				}

				GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates> geoLineStringCoordinate = new GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>(geoPositions);
				geoLinearRingCoordinates.Add(geoLineStringCoordinate);
			}

			GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates> exterieur = geoLinearRingCoordinates.First();
			IEnumerable<GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>> holes = geoLinearRingCoordinates.Skip(1);

			GeoJsonPolygonCoordinates<GeoJson3DGeographicCoordinates> geoPolygonCoordinates = new GeoJsonPolygonCoordinates<GeoJson3DGeographicCoordinates>(exterieur, holes);
			GeoJsonPolygon<GeoJson3DGeographicCoordinates> geoPolygon = new GeoJsonPolygon<GeoJson3DGeographicCoordinates>(geoPolygonCoordinates);

			return geoPolygon;
		}

		internal static GeoJsonPolygon<GeoJson2DGeographicCoordinates> Create2D(Polygon value)
		{
			IList<GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>> geoLinearRingCoordinates = new List<GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>>();

			foreach(LineString lineString in value.Coordinates)
			{
				IList<GeoJson2DGeographicCoordinates> geoPositions = new List<GeoJson2DGeographicCoordinates>();

				foreach(Position position in lineString.Coordinates)
				{
					geoPositions.Add(new GeoJson2DGeographicCoordinates(position.Longitude, position.Latitude));
				}

				GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates> geoLineStringCoordinate = new GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>(geoPositions);
				geoLinearRingCoordinates.Add(geoLineStringCoordinate);
			}

			GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates> exterieur = geoLinearRingCoordinates.First();
			IEnumerable<GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>> holes = geoLinearRingCoordinates.Skip(1);

			GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates> geoPolygonCoordinates = new GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>(exterieur, holes);
			GeoJsonPolygon<GeoJson2DGeographicCoordinates> geoPolygon = new GeoJsonPolygon<GeoJson2DGeographicCoordinates>(geoPolygonCoordinates);

			return geoPolygon;
		}
	}
}
