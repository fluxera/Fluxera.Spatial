namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles <see cref="MultiLineString" /> instances.
	/// </summary>
	[PublicAPI]
	public static class MultiLineStringSerializer
	{
		/// <summary>
		///     Serialize the spatial object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static BsonValue Serialize(object obj)
		{
			MultiLineString multiLineString = (MultiLineString)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "MultiLineString" },
			});

			BsonArray coordinatesArray = new BsonArray();
			foreach(LineString lineString in multiLineString.Coordinates)
			{
				BsonArray lineStringArray = new BsonArray();
				foreach(Position position in lineString.Coordinates)
				{
					BsonArray positionArray = PositionSerializer.WritePosition(position);
					lineStringArray.Add(positionArray);
				}

				coordinatesArray.Add(lineStringArray);
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
			if(type == "MultiLineString")
			{
				BsonArray coordinatesArray = value["coordinates"].AsArray;

				IList<LineString> lineStrings = new List<LineString>();
				foreach(BsonValue lineStringArray in coordinatesArray)
				{
					IList<Position> positions = new List<Position>();
					foreach(BsonValue positionArray in lineStringArray.AsArray)
					{
						Position position = PositionSerializer.ReadPosition(positionArray.AsArray);
						positions.Add(position);
					}

					lineStrings.Add(new LineString(positions));
				}

				return new MultiLineString(lineStrings);
			}

			return MultiLineString.Empty;
		}
	}
}
