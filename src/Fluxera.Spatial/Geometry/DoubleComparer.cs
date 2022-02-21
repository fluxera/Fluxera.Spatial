// ReSharper disable once CheckNamespace

namespace Fluxera.Spatial
{
	using System;
	using System.Collections.Generic;

	/// <summary>
	///     An <see cref="IEqualityComparer{T}" /> that compares double values for
	///     equality using a default accuracy of 0.0000000001.
	/// </summary>
	internal sealed class DoubleComparer : IEqualityComparer<double>
	{
		private readonly double accuracy;

		public DoubleComparer(double accuracy = 0.0000000001)
		{
			this.accuracy = accuracy;
		}

		/// <inheritdoc />
		public bool Equals(double x, double y)
		{
			return Math.Abs(x - y) < this.accuracy;
		}

		/// <inheritdoc />
		public int GetHashCode(double obj)
		{
			return obj.GetHashCode();
		}
	}
}
