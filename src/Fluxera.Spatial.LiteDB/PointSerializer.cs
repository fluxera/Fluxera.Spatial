namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;

	public static class PointSerializer
	{
		public static BsonValue Serialize(object obj)
		{
			Point point = (Point)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "Point" },
				{ "coordinates", PositionSerializer.WritePosition(point.Coordinates) }
			});

			return document;
		}

		public static object Deserialize(BsonValue value)
		{
			string type = value["type"].AsString;
			if(type == "Point")
			{
				BsonArray coordinatesArray = value["coordinates"].AsArray;
				Position position = PositionSerializer.ReadPosition(coordinatesArray);

				return new Point(position);
			}

			return Point.Empty;
		}
	}
}
