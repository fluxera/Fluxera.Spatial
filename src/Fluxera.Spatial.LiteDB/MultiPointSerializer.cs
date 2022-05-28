namespace Fluxera.Spatial.LiteDB
{
	using System.Collections.Generic;
	using global::LiteDB;
	using JetBrains.Annotations;

	/// <summary>
	///     A serializer that handles <see cref="MultiPoint" /> instances.
	/// </summary>
	[PublicAPI]
	public static class MultiPointSerializer
	{
		/// <summary>
		///     Serialize the spatial object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static BsonValue Serialize(object obj)
		{
			MultiPoint multiPoint = (MultiPoint)obj;

			BsonDocument document = new BsonDocument(new Dictionary<string, BsonValue>
			{
				{ "type", "MultiPoint" },
			});

			BsonArray coordinatesArray = new BsonArray();
			foreach(Position position in multiPoint.Coordinates)
			{
				BsonArray positionArray = PositionSerializer.WritePosition(position);
				coordinatesArray.Add(positionArray);
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
			if(type == "MultiPoint")
			{
				BsonArray coordinatesArray = value["coordinates"].AsArray;

				IList<Position> positions = new List<Position>();
				foreach(BsonValue positionArray in coordinatesArray)
				{
					Position position = PositionSerializer.ReadPosition(positionArray.AsArray);
					positions.Add(position);
				}

				return new MultiPoint(positions);
			}

			return MultiPoint.Empty;
		}
	}
}
