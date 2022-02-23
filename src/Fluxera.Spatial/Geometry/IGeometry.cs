// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using JetBrains.Annotations;

	/// <summary>
	///     Common interface for all geometry types.
	/// </summary>
	[PublicAPI]
	public interface IGeometry
	{
		/// <summary>
		///     Gets if the geometry object uses altitude in it's coordinates.
		/// </summary>
		bool HasAltitude { get; }
	}
}
