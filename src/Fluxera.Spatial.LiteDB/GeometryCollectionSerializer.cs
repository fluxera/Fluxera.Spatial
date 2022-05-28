namespace Fluxera.Spatial.LiteDB
{
	using System;
	using System.Collections.Generic;
	using global::LiteDB;

	/// <summary>
	///     A serializer that handles <see cref="GeometryCollection" /> instances.
	/// </summary>
	public static class GeometryCollectionSerializer
	{
		/// <summary>
		///     Serialize the spatial object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
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

				BsonDocument geometryDocument = BsonMapper.Global.ToDocument(geometry);
				geometriesArray.Add(geometryDocument);
			}

			document.Add("geometries", geometriesArray);

			return document;
		}

		/// <summary>
		///     Deserialize the spatial object.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
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
