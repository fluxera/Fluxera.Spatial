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

	/// <inheritdoc />
	[PublicAPI]
	public sealed class MultiPointSerializer : StructSerializerBase<MultiPoint>
	{
		private readonly GeoJsonMultiPointSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonMultiPointSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonMultiPointSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonMultiPointSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, MultiPoint value)
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
		public override MultiPoint Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "MultiPoint")
			{
				BsonArray coordinatesArray = document["coordinates"].AsBsonArray;

				IList<Position> positions = new List<Position>();
				foreach(BsonValue positionArrayValue in coordinatesArray)
				{
					BsonArray positionArray = positionArrayValue.AsBsonArray;
					Position position = positionArray.ReadPosition();
					positions.Add(position);
				}

				return new MultiPoint(positions);
			}

			return MultiPoint.Empty;
		}

		internal static GeoJsonMultiPoint<GeoJson3DGeographicCoordinates> Create3D(MultiPoint value)
		{
			IList<GeoJson3DGeographicCoordinates> geoPositions = new List<GeoJson3DGeographicCoordinates>();

			foreach(Position position in value.Coordinates)
			{
				geoPositions.Add(new GeoJson3DGeographicCoordinates(position.Longitude, position.Latitude, position.Altitude.GetValueOrDefault()));
			}

			GeoJsonMultiPointCoordinates<GeoJson3DGeographicCoordinates> geoMultiPointCoordinates = new GeoJsonMultiPointCoordinates<GeoJson3DGeographicCoordinates>(geoPositions);
			GeoJsonMultiPoint<GeoJson3DGeographicCoordinates> geoMultiPoint = new GeoJsonMultiPoint<GeoJson3DGeographicCoordinates>(geoMultiPointCoordinates);

			return geoMultiPoint;
		}

		internal static GeoJsonMultiPoint<GeoJson2DGeographicCoordinates> Create2D(MultiPoint value)
		{
			IList<GeoJson2DGeographicCoordinates> geoPositions = new List<GeoJson2DGeographicCoordinates>();

			foreach(Position position in value.Coordinates)
			{
				geoPositions.Add(new GeoJson2DGeographicCoordinates(position.Longitude, position.Latitude));
			}

			GeoJsonMultiPointCoordinates<GeoJson2DGeographicCoordinates> geoMultiPointCoordinates = new GeoJsonMultiPointCoordinates<GeoJson2DGeographicCoordinates>(geoPositions);
			GeoJsonMultiPoint<GeoJson2DGeographicCoordinates> geoMultiPoint = new GeoJsonMultiPoint<GeoJson2DGeographicCoordinates>(geoMultiPointCoordinates);

			return geoMultiPoint;
		}
	}
}
