namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles <see cref="Point" /> instances.
	/// </summary>
	[PublicAPI]
	public static class PointSerializer
	{
		/// <summary>
		///     Serialize the spatial object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
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

		/// <summary>
		///     Deserialize the spatial object.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
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
