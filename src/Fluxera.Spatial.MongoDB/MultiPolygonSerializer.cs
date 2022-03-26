namespace Fluxera.Spatial.MongoDB
{
	using System.Collections.Generic;
	using System.Linq;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;
	using JetBrains.Annotations;

	[PublicAPI]
	public sealed class MultiPolygonSerializer : StructSerializerBase<MultiPolygon>
	{
		private readonly GeoJsonMultiPolygonSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonMultiPolygonSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonMultiPolygonSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonMultiPolygonSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, MultiPolygon value)
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
		public override MultiPolygon Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "MultiPolygon")
			{
				BsonArray coordinatesArray = document["coordinates"].AsBsonArray;

				IList<Polygon> polygons = new List<Polygon>();
				foreach(BsonValue polygonArrayValue in coordinatesArray)
				{
					BsonArray polygonArray = polygonArrayValue.AsBsonArray;

					IList<LineString> lineStrings = new List<LineString>();
					foreach(BsonValue lineStringArrayValue in polygonArray)
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

					polygons.Add(new Polygon(lineStrings));
				}

				return new MultiPolygon(polygons);
			}

			return MultiPolygon.Empty;
		}

		internal static GeoJsonMultiPolygon<GeoJson3DGeographicCoordinates> Create3D(MultiPolygon value)
		{
			IList<GeoJsonPolygonCoordinates<GeoJson3DGeographicCoordinates>> geoPolygons = new List<GeoJsonPolygonCoordinates<GeoJson3DGeographicCoordinates>>();

			foreach(Polygon polygon in value.Coordinates)
			{
				IList<GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>> geoLinearRingCoordinates = new List<GeoJsonLinearRingCoordinates<GeoJson3DGeographicCoordinates>>();

				foreach(LineString lineString in polygon.Coordinates)
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
				geoPolygons.Add(geoPolygonCoordinates);
			}

			GeoJsonMultiPolygonCoordinates<GeoJson3DGeographicCoordinates> geoMultiPolygonCoordinates = new GeoJsonMultiPolygonCoordinates<GeoJson3DGeographicCoordinates>(geoPolygons);
			GeoJsonMultiPolygon<GeoJson3DGeographicCoordinates> geoMultiPolygon = new GeoJsonMultiPolygon<GeoJson3DGeographicCoordinates>(geoMultiPolygonCoordinates);

			return geoMultiPolygon;
		}

		internal static GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> Create2D(MultiPolygon value)
		{
			IList<GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>> geoPolygons = new List<GeoJsonPolygonCoordinates<GeoJson2DGeographicCoordinates>>();

			foreach(Polygon polygon in value.Coordinates)
			{
				IList<GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>> geoLinearRingCoordinates = new List<GeoJsonLinearRingCoordinates<GeoJson2DGeographicCoordinates>>();

				foreach(LineString lineString in polygon.Coordinates)
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
				geoPolygons.Add(geoPolygonCoordinates);
			}

			GeoJsonMultiPolygonCoordinates<GeoJson2DGeographicCoordinates> geoMultiPolygonCoordinates = new GeoJsonMultiPolygonCoordinates<GeoJson2DGeographicCoordinates>(geoPolygons);
			GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates> geoMultiPolygon = new GeoJsonMultiPolygon<GeoJson2DGeographicCoordinates>(geoMultiPolygonCoordinates);

			return geoMultiPolygon;
		}
	}
}
