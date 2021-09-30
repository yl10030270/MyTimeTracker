using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTimeTracker.Core.Utility
{
	public static class EnumerableExtensions
	{
		public static string JoinToString<T>(this IEnumerable<T> inputs, Func<T, string>? toStringFunc = null, string separator = ",")
		{
			return string.Join(separator, inputs.Select(x => toStringFunc?.Invoke(x) ?? x?.ToString()));
		}
	}
}