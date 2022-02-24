namespace Fluxera.Spatial.LiteDB
{
	using System.Linq;
	using global::LiteDB;

	internal static class PositionSerializer
	{
		public static BsonArray WritePosition(Position position)
		{
			BsonArray coordinatesArray = new BsonArray(position.Longitude, position.Latitude);

			if(position.Altitude.HasValue)
			{
				coordinatesArray.Add(position.Altitude.Value);
			}

			return coordinatesArray;
		}

		public static Position ReadPosition(BsonArray coordinatesArray)
		{
			if(coordinatesArray.Count >= 2)
			{
				BsonValue[] values = coordinatesArray.RawValue.ToArray();
				double longitude = values[0].AsDouble;
				double latitude = values[1].AsDouble;
				double? altitude = null;

				if(coordinatesArray.Count >= 3)
				{
					altitude = values[2].AsDouble;
				}

				Position position = new Position(longitude, latitude, altitude);
				return position;
			}

			return Position.Empty;
		}
	}
}
