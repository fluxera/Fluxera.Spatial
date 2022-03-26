namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;
	using JetBrains.Annotations;

	[PublicAPI]
	public static class LineStringSerializer
	{
		public static BsonValue Serialize(object obj)
		{
			LineString lineString = (LineString)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "LineString" },
			});

			BsonArray coordinatesArray = new BsonArray();
			foreach(Position position in lineString.Coordinates)
			{
				BsonArray positionArray = PositionSerializer.WritePosition(position);
				coordinatesArray.Add(positionArray);
			}

			document.Add("coordinates", coordinatesArray);

			return document;
		}

		public static object Deserialize(BsonValue value)
		{
			string type = value["type"].AsString;
			if(type == "LineString")
			{
				BsonArray coordinatesArray = value["coordinates"].AsArray;

				IList<Position> positions = new List<Position>();
				foreach(BsonValue positionArray in coordinatesArray)
				{
					Position position = PositionSerializer.ReadPosition(positionArray.AsArray);
					positions.Add(position);
				}

				return new LineString(positions);
			}

			return LineString.Empty;
		}
	}
}
