namespace Fluxera.Spatial.MongoDB
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using global::MongoDB.Bson;
	using global::MongoDB.Bson.IO;
	using global::MongoDB.Bson.Serialization;
	using global::MongoDB.Bson.Serialization.Serializers;
	using global::MongoDB.Driver.GeoJsonObjectModel;
	using global::MongoDB.Driver.GeoJsonObjectModel.Serializers;
	using JetBrains.Annotations;

	/// <inheritdoc />
	[PublicAPI]
	public sealed class GeometryCollectionSerializer : StructSerializerBase<GeometryCollection>
	{
		private readonly LineStringSerializer lineStringSerializer = new LineStringSerializer();
		private readonly MultiLineStringSerializer multiLineStringSerializer = new MultiLineStringSerializer();
		private readonly MultiPointSerializer multiPointSerializer = new MultiPointSerializer();
		private readonly MultiPolygonSerializer multiPolygonSerializer = new MultiPolygonSerializer();
		private readonly PointSerializer pointSerializer = new PointSerializer();
		private readonly PolygonSerializer polygonSerializer = new PolygonSerializer();

		private readonly GeoJsonGeometryCollectionSerializer<GeoJson2DGeographicCoordinates> serializer = new GeoJsonGeometryCollectionSerializer<GeoJson2DGeographicCoordinates>();
		private readonly GeoJsonGeometryCollectionSerializer<GeoJson3DGeographicCoordinates> serializerWithAltitude = new GeoJsonGeometryCollectionSerializer<GeoJson3DGeographicCoordinates>();

		/// <inheritdoc />
		public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, GeometryCollection value)
		{
			bool hasAltitude = value.Geometries.Any(x => x.HasAltitude);
			if(hasAltitude)
			{
				IList<GeoJsonGeometry<GeoJson3DGeographicCoordinates>> geoGeometries = new List<GeoJsonGeometry<GeoJson3DGeographicCoordinates>>();

				foreach(IGeometry geometry in value.Geometries)
				{
					if(geometry is GeometryCollection)
					{
						throw new ArgumentOutOfRangeException();
					}

					switch(geometry)
					{
						case Point point:
							geoGeometries.Add(PointSerializer.Create3D(point));
							break;
						case MultiPoint multiPoint:
							geoGeometries.Add(MultiPointSerializer.Create3D(multiPoint));
							break;
						case LineString lineString:
							geoGeometries.Add(LineStringSerializer.Create3D(lineString));
							break;
						case MultiLineString multiLineString:
							geoGeometries.Add(MultiLineStringSerializer.Create3D(multiLineString));
							break;
						case Polygon polygon:
							geoGeometries.Add(PolygonSerializer.Create3D(polygon));
							break;
						case MultiPolygon multiPolygon:
							geoGeometries.Add(MultiPolygonSerializer.Create3D(multiPolygon));
							break;
					}
				}

				GeoJsonGeometryCollection<GeoJson3DGeographicCoordinates> geoGeometryCollection = new GeoJsonGeometryCollection<GeoJson3DGeographicCoordinates>(geoGeometries);
				this.serializerWithAltitude.Serialize(context, args, geoGeometryCollection);
			}
			else
			{
				IList<GeoJsonGeometry<GeoJson2DGeographicCoordinates>> geoGeometries = new List<GeoJsonGeometry<GeoJson2DGeographicCoordinates>>();

				foreach(IGeometry geometry in value.Geometries)
				{
					if(geometry is GeometryCollection)
					{
						throw new ArgumentOutOfRangeException();
					}

					switch(geometry)
					{
						case Point point:
							geoGeometries.Add(PointSerializer.Create2D(point));
							break;
						case MultiPoint multiPoint:
							geoGeometries.Add(MultiPointSerializer.Create2D(multiPoint));
							break;
						case LineString lineString:
							geoGeometries.Add(LineStringSerializer.Create2D(lineString));
							break;
						case MultiLineString multiLineString:
							geoGeometries.Add(MultiLineStringSerializer.Create2D(multiLineString));
							break;
						case Polygon polygon:
							geoGeometries.Add(PolygonSerializer.Create2D(polygon));
							break;
						case MultiPolygon multiPolygon:
							geoGeometries.Add(MultiPolygonSerializer.Create2D(multiPolygon));
							break;
					}
				}

				GeoJsonGeometryCollection<GeoJson2DGeographicCoordinates> geoGeometryCollection = new GeoJsonGeometryCollection<GeoJson2DGeographicCoordinates>(geoGeometries);
				this.serializer.Serialize(context, args, geoGeometryCollection);
			}
		}

		/// <inheritdoc />
		public override GeometryCollection Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
		{
			BsonDocument document = BsonDocumentSerializer.Instance.Deserialize(context);
			string type = document["type"].AsString;

			if(type == "GeometryCollection")
			{
				BsonArray geometriesArray = document["geometries"].AsBsonArray;

				IList<IGeometry> geometries = new List<IGeometry>();

				foreach(BsonValue geometryValue in geometriesArray)
				{
					BsonDocument geometryDocument = geometryValue.AsBsonDocument;
					BsonDeserializationContext newContext = BsonDeserializationContext.CreateRoot(new BsonDocumentReader(geometryDocument));

					string geometryTypeName = geometryDocument["type"].AsString;
					switch(geometryTypeName)
					{
						case "Point":
							Point point = this.pointSerializer.Deserialize(newContext, args);
							geometries.Add(point);
							break;
						case "MultiPoint":
							MultiPoint multiPoint = this.multiPointSerializer.Deserialize(newContext, args);
							geometries.Add(multiPoint);
							break;
						case "LineString":
							LineString lineString = this.lineStringSerializer.Deserialize(newContext, args);
							geometries.Add(lineString);
							break;
						case "MultiLineString":
							MultiLineString multiLineString = this.multiLineStringSerializer.Deserialize(newContext, args);
							geometries.Add(multiLineString);
							break;
						case "Polygon":
							Polygon polygon = this.polygonSerializer.Deserialize(newContext, args);
							geometries.Add(polygon);
							break;
						case "MultiPolygon":
							MultiPolygon multiPolygon = this.multiPolygonSerializer.Deserialize(newContext, args);
							geometries.Add(multiPolygon);
							break;
						default:
							throw new ArgumentOutOfRangeException();
					}
				}

				return new GeometryCollection(geometries);
			}

			return GeometryCollection.Empty;
		}
	}
}
