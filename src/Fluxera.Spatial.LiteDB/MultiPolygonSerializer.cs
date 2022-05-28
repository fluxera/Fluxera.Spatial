namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles <see cref="MultiPolygon" /> instances.
	/// </summary>
	[PublicAPI]
	public static class MultiPolygonSerializer
	{
		/// <summary>
		///     Serialize the spatial object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static BsonValue Serialize(object obj)
		{
			MultiPolygon multiPolygon = (MultiPolygon)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "MultiPolygon" },
			});

			BsonArray coordinatesArray = new BsonArray();
			foreach(Polygon polygon in multiPolygon.Coordinates)
			{
				BsonArray polygonArray = new BsonArray();
				foreach(LineString lineString in polygon.Coordinates)
				{
					BsonArray lineStringArray = new BsonArray();
					foreach(Position position in lineString.Coordinates)
					{
						BsonArray positionArray = PositionSerializer.WritePosition(position);
						lineStringArray.Add(positionArray);
					}

					polygonArray.Add(lineStringArray);
				}

				coordinatesArray.Add(polygonArray);
			}

			document.Add("coordinates", coordinatesArray);

			return document;
		}

		/// <summary>
		///     Deserialize the spatial object.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static object Deserialize(BsonValue value)
		{
			string type = value["type"].AsString;
			if(type == "MultiPolygon")
			{
				BsonArray coordinatesArray = value["coordinates"].AsArray;

				IList<Polygon> polygons = new List<Polygon>();
				foreach(BsonValue polygonsArray in coordinatesArray)
				{
					IList<LineString> lineStrings = new List<LineString>();
					foreach(BsonValue lineStringArray in polygonsArray.AsArray)
					{
						IList<Position> positions = new List<Position>();
						foreach(BsonValue positionArray in lineStringArray.AsArray)
						{
							Position position = PositionSerializer.ReadPosition(positionArray.AsArray);
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
	}
}
