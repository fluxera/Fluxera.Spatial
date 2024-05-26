namespace Fluxera.Spatial.MongoDB
{
	using System.Linq;
	using global::MongoDB.Bson;

	internal static class BsonDeserializationContextExtensions
	{
		public static Position ReadPosition(this BsonArray coordinatesArray)
		{
			Guard.ThrowIfNull(coordinatesArray);

			if(coordinatesArray.Count >= 2)
			{
				BsonValue[] values = coordinatesArray.Values.ToArray();
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
