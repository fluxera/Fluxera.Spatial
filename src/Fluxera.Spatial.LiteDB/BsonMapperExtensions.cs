namespace Fluxera.Spatial.LiteDB
{
	using System;
	using System.Collections.Generic;
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

			mapper.RegisterType(typeof(GeometryCollection),
				GeometryCollectionSerializer.Serialize,
				GeometryCollectionSerializer.Deserialize);

			return mapper;
		}
	}

	public static class GeometryCollectionSerializer
	{
		public static BsonValue Serialize(object obj)
		{
			GeometryCollection geometryCollection = (GeometryCollection)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "GeometryCollection" },
			});

			BsonArray geometriesArray = new BsonArray();
			foreach(IGeometry geometry in geometryCollection.Geometries)
			{
				if(geometry is GeometryCollection)
				{
					throw new ArgumentOutOfRangeException();
				}

				BsonDocument? geometryDocument = BsonMapper.Global.ToDocument(geometry);
				geometriesArray.Add(geometryDocument);
			}

			document.Add("geometries", geometriesArray);

			return document;
		}

		public static object Deserialize(BsonValue value)
		{
			string type = value["type"].AsString;
			if(type == "GeometryCollection")
			{
				IList<IGeometry> geometries = new List<IGeometry>();

				BsonArray geometriesArray = value["geometries"].AsArray;
				foreach(BsonValue geometryValue in geometriesArray)
				{
					string geometryTypeName = geometryValue["type"].AsString;

					Type geometryType = geometryTypeName switch
					{
						"Point" => typeof(Point),
						"MultiPoint" => typeof(MultiPoint),
						"LineString" => typeof(LineString),
						"MultiLineString" => typeof(MultiLineString),
						"Polygon" => typeof(Polygon),
						"MultiPolygon" => typeof(MultiPolygon),
						_ => throw new ArgumentOutOfRangeException()
					};

					object geometry = BsonMapper.Global.ToObject(geometryType, geometryValue.AsDocument);
					geometries.Add((IGeometry)geometry);
				}

				return new GeometryCollection(geometries);
			}

			return GeometryCollection.Empty;
		}
	}
}
