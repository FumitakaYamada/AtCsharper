<Query Kind="Program">
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
</Query>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

static class Program
{
	static void Main()
	{
		var inputter = new Inputter();
		var s = inputter.GetNext();
		var n = inputter.GetNext().ToInt();
		var inp = inputter.GetNext().Split().Select(ToInt).ToArray();
		var a = inp[0];
		var b = inp[1];

		Wl();
	}

	public class Inputter
	{
		public bool IsDebug { get; } = true;
		//public bool IsDebug { get; } = false;

		public static string _str =
	$@"
";

		private int _index = 0;
		private string[] lines = null;

		private string[] GetLines()
		{
			if (lines == null)
			{
				lines = _str.Split("\n")
					.Select(x => x.Replace("\n", "").Replace("\r", ""))
					.ToArray();
			}
			return lines;
		}

		public string GetNext()
		{
			if (IsDebug)
			{
				var str = GetLines()[_index];
				_index++;
				return str;
			}
			else
			{
				return Console.ReadLine();
			}
		}
	}

	const int M = 1000000007;

	static int[] sosuu = new int[] {
	2,3,5,7,11,13,17,19,23,29,31,37,41,43,47,53,59,61,67,71,
73,79,83,89,97,101,103,107,109,113,127,131,137,139,149,151,157,163,167,173,
179,181,191,193,197,199,211,223,227,229,233,239,241,251,257,263,269,271,277,281,
283,293,307,311,313,317,331,337,347,349,353,359,367,373,379,383,389,397,401,409,
419,421,431,433,439,443,449,457,461,463,467,479,487,491,499,503,509,521,523,541,
547,557,563,569,571,577,587,593,599,601,607,613,617,619,631,641,643,647,653,659,
661,673,677,683,691,701,709,719,727,733,739,743,751,757,761,769,773,787,797,809,
811,821,823,827,829,839,853,857,859,863,877,881,883,887,907,911,919,929,937,941,
947,953,967,971,977,983,991,997,1009,1013,1019,1021,1031,1033,1039,1049,1051,1061,1063,1069,
1087,1091,1093,1097,1103,1109,1117,1123,1129,1151,1153,1163,1171,1181,1187,1193,1201,1213,1217,1223,
1229,1231,1237,1249,1259,1277,1279,1283,1289,1291,1297,1301,1303,1307,1319,1321,1327,1361,1367,1373,
1381,1399,1409,1423,1427,1429,1433,1439,1447,1451,1453,1459,1471,1481,1483,1487,1489,1493,1499,1511,
1523,1531,1543,1549,1553,1559,1567,1571,1579,1583,1597,1601,1607,1609,1613,1619,1621,1627,1637,1657,
1663,1667,1669,1693,1697,1699,1709,1721,1723,1733,1741,1747,1753,1759,1777,1783,1787,1789,1801,1811,
1823,1831,1847,1861,1867,1871,1873,1877,1879,1889,1901,1907,1913,1931,1933,1949,1951,1973,1979,1987,
1993,1997,1999,2003,2011,2017,2027,2029,2039,2053,2063,2069,2081,2083,2087,2089,2099,2111,2113,2129,
2131,2137,2141,2143,2153,2161,2179,2203,2207,2213,2221,2237,2239,2243,2251,2267,2269,2273,2281,2287,
2293,2297,2309,2311,2333,2339,2341,2347,2351,2357,2371,2377,2381,2383,2389,2393,2399,2411,2417,2423,
2437,2441,2447,2459,2467,2473,2477,2503,2521,2531,2539,2543,2549,2551,2557,2579,2591,2593,2609,2617,
2621,2633,2647,2657,2659,2663,2671,2677,2683,2687,2689,2693,2699,2707,2711,2713,2719,2729,2731,2741,
2749,2753,2767,2777,2789,2791,2797,2801,2803,2819,2833,2837,2843,2851,2857,2861,2879,2887,2897,2903,
2909,2917,2927,2939,2953,2957,2963,2969,2971,2999,3001,3011,3019,3023,3037,3041,3049,3061,3067,3079,
3083,3089,3109,3119,3121,3137,3163,3167,3169,3181,3187,3191,3203,3209,3217,3221,3229,3251,3253,3257,
3259,3271,3299,3301,3307,3313,3319,3323,3329,3331,3343,3347,3359,3361,3371,3373,3389,3391,3407,3413,
3433,3449,3457,3461,3463,3467,3469,3491,3499,3511,3517,3527,3529,3533,3539,3541,3547,3557,3559,3571
	};

	// 順列
	static long nPk(long n, long k)
	{
		if (n < k) return 0;
		if (n == k) return 1;

		long x = 1;
		for (long i = 0; i < k; i++)
		{
			x = x * (n - i);
		}
		return x;
	}

	// 組合せ
	static long nCk(long n, long k)
	{
		if (n < k) return 0;
		if (n == k) return 1;

		long x = 1;
		for (long i = 0; i < k; i++)
		{
			x = x * (n - i) / (i + 1);
		}
		return x;
	}

	public static string ToSpaceString<T>(this IEnumerable<T> ie)
	{
		return String.Join(' ', ie.ToArray());
	}

	public static IEnumerable<long> ToLong(this IEnumerable<int> ie)
	{
		return ie.Select(x => (long)x);
	}
	
	public static long LongSum(this IEnumerable<int> ie)
	{
		return ie.ToLong().Sum();
	}

	public static void Wl(object obj = null)
	{
		Console.WriteLine(obj);
	}

	public static long ToLong(this string str)
	{
		return long.Parse(str);
	}

	public static int ToInt(this string str)
	{
		return int.Parse(str);
	}

	public static int ToInt(this char ch)
	{
		return int.Parse(ch.ToString());
	}

	public static double ToDouble(this string str)
	{
		return double.Parse(str);
	}

	public static long GetDigit(this long num)
	{
		return (num == 0) ? 1 : ((long)Math.Log10(num) + 1);
	}

	// a ^ n mod mod
	public static long ModPow(long a, long n, long mod)
	{
		long res = 1;
		while (n > 0)
		{
			if ((n & 1) == 1)
			{
				res = res * a % mod;
			}
			a = a * a % mod;
			n = n >> 1;
		}
		return res;
	}

	public static long ModInv(long a, long m)
	{
		long b = m, u = 1, v = 0;
		while (b > 0)
		{
			long t = a / b;
			a -= t * b; Swap(ref a, ref b);
			u -= t * v; Swap(ref u, ref v);
		}
		u %= m;
		if (u < 0) u += m;
		return u;
	}

	public static int CountChar(this string s, char c)
	{
		return s.Length - s.Replace(c.ToString(), "").Length;
	}

	public static int CountString(string s, string s2)
	{
		return (s.Length - s.Replace(s2, "").Length) / s2.Length;
	}


	public static void Swap(ref object a, ref object b)
	{
		var c = a;
		a = b;
		b = c;
	}

	public static void Swap(ref long a, ref long b)
	{
		var c = a;
		a = b;
		b = c;
	}

	public static void rep(int count, Action<int> action)
	{
		for (var i = 0; i < count; i++)
		{
			action(i);
		}
	}

	public static void rep(int count, Func<int, bool> action)
	{
		for (var i = 0; i < count; i++)
		{
			if (!action(i)) break;
		}
	}

	public static string ToBitString(this int num)
	{
		var ca = new List<char>();

		var i = 0;
		while (num > 0)
		{
			ca.Insert(0, (num % (1 << (i + 1))).ToString().ToCharArray().First());
			num = num >> 1;
		}

		return new String(ca.ToArray());
	}

	public static int BitStringToInt(this string str)
	{
		var result = 0;
		foreach (var c in str.ToCharArray())
		{
			
			if (c.Equals('1'))
			{
				result++;
			}
			
			result = result << 1;
		}
		return result;
	}

	public static IEnumerable<long> GetPrimeFactors(this long n)
	{
		var i = 2L;
		var tmp = n;

		while (i * i <= n)
		{
			if (tmp % i == 0)
			{
				tmp /= i;
				yield return i;
			}
			else
			{
				i++;
			}
		}
		if (tmp != 1L) yield return tmp;
	}

	public static long GetGcd(this long a, long b)
	{
		var r = a % b;
		if (r == 0)
		{
			return b;
		}
		return GetGcd(b, r);
	}

	static long GetGcd(this IEnumerable<long> numbers)
	{
		return numbers.Aggregate(GetGcd);
	}

	public static IEnumerable<int> Ie(int start, int count)
	{
		return Enumerable.Range(start, count);
	}

	public static IEnumerable<int> Ie(int count)
	{
		return Enumerable.Range(0, count);
	}

	public class LP
	{
		public long X;
		public long Y;

		public LP(long x, long y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"X : {X}, Y : {Y}";
		}
	}
	
	public class P
	{
		public int X;
		public int Y;

		public P(int x, int y)
		{
			X = x;
			Y = y;
		}

		public override string ToString()
		{
			return $"X : {X}, Y : {Y}";
		}
	}

	public class Range
	{
		public int min;
		public int max;

		public Range(int xx, int yx)
		{
			min = xx;
			max = yx;
		}
	}

	public class DP
	{
		public double X { get; }
		public double Y { get; }

		public DP(int x, int y)
		{
			X = x;
			Y = y;
		}

		public DP(double x, double y)
		{
			X = x;
			Y = y;
		}

		public DP(int[] nums)
		{
			X = nums[0];
			Y = nums[1];
		}

		public DP(double[] nums)
		{
			X = nums[0];
			Y = nums[1];
		}
	}

	public static double GetEuclidDistance(P a, P b)
	{
		return Math.Sqrt(Math.Pow(a.X - (double)b.X, 2) + Math.Pow(a.Y - (double)b.Y, 2));
	}

	public static long GetManhattanDistance(P a, P b)
	{
		return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
	}

	public static DP RotateCounterClock(DP center, DP rotatePoint, double radius)
	{
		double cosTheta = Math.Cos(radius);
		double sinTheta = Math.Sin(radius);
		return new DP(
				(cosTheta * (rotatePoint.X - (double)center.X) -
				sinTheta * (rotatePoint.Y - (double)center.Y) + center.X),
				(sinTheta * (rotatePoint.X - (double)center.X) +
				cosTheta * (rotatePoint.Y - (double)center.Y) + center.Y)
			);
	}
}

public static class Extension
{
	public static PriorityQueue<T> ToPriorityQueue<T>(this IEnumerable<T> source, bool isDescending = true)
	{
		var queue = new PriorityQueue<T>(isDescending);
		foreach (var item in source)
		{
			queue.Enqueue(item);
		}

		return queue;
	}

	public static PriorityQueue<TKey, TSource> ToPriorityQueue<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, bool isDescending = true)
	{
		var queue = new PriorityQueue<TKey, TSource>(keySelector, isDescending);
		foreach (var item in source)
		{
			queue.Enqueue(item);
		}

		return queue;
	}
}

public class PriorityQueue<T> : IEnumerable<T>
{
	private readonly List<T> _data = new List<T>();
	private readonly IComparer<T> _comparer;
	private readonly bool _isDescending;

	public PriorityQueue(IComparer<T> comparer, bool isDescending = true)
	{
		_comparer = comparer;
		_isDescending = isDescending;
	}

	public PriorityQueue(Comparison<T> comparison, bool isDescending = true)
		: this(Comparer<T>.Create(comparison), isDescending)
	{
	}

	public PriorityQueue(bool isDescending = true)
		: this(Comparer<T>.Default, isDescending)
	{
	}

	public void Enqueue(T item)
	{
		_data.Add(item);
		var childIndex = _data.Count - 1;
		while (childIndex > 0)
		{
			var parentIndex = (childIndex - 1) / 2;
			if (Compare(_data[childIndex], _data[parentIndex]) >= 0)
				break;
			Swap(childIndex, parentIndex);
			childIndex = parentIndex;
		}
	}

	public T Dequeue()
	{
		var lastIndex = _data.Count - 1;
		var firstItem = _data[0];
		_data[0] = _data[lastIndex];
		_data.RemoveAt(lastIndex--);
		var parentIndex = 0;
		while (true)
		{
			var childIndex = parentIndex * 2 + 1;
			if (childIndex > lastIndex)
				break;
			var rightChild = childIndex + 1;
			if (rightChild <= lastIndex && Compare(_data[rightChild], _data[childIndex]) < 0)
				childIndex = rightChild;
			if (Compare(_data[parentIndex], _data[childIndex]) <= 0)
				break;
			Swap(parentIndex, childIndex);
			parentIndex = childIndex;
		}
		return firstItem;
	}

	public T Peek()
	{
		return _data[0];
	}

	private void Swap(int a, int b)
	{
		var tmp = _data[a];
		_data[a] = _data[b];
		_data[b] = tmp;
	}

	private int Compare(T a, T b)
	{
		return _isDescending ? _comparer.Compare(b, a) : _comparer.Compare(a, b);
	}

	public int Count => _data.Count;

	public IEnumerator<T> GetEnumerator()
	{
		return _data.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PriorityQueue<TKey, TValue> : IEnumerable<TValue>
{
	private readonly List<KeyValuePair<TKey, TValue>> _data = new List<KeyValuePair<TKey, TValue>>();
	private readonly bool _isDescending;
	private readonly Func<TValue, TKey> _keySelector;
	private readonly IComparer<TKey> _keyComparer;

	public PriorityQueue(Func<TValue, TKey> keySelector, bool isDescending = true)
		: this(keySelector, Comparer<TKey>.Default, isDescending)
	{
	}

	public PriorityQueue(Func<TValue, TKey> keySelector, IComparer<TKey> keyComparer, bool isDescending = true)
	{
		_keySelector = keySelector;
		_keyComparer = keyComparer;
		_isDescending = isDescending;
	}

	public void Enqueue(TValue item)
	{
		_data.Add(new KeyValuePair<TKey, TValue>(_keySelector(item), item));
		var childIndex = _data.Count - 1;
		while (childIndex > 0)
		{
			var parentIndex = (childIndex - 1) / 2;
			if (Compare(_data[childIndex].Key, _data[parentIndex].Key) >= 0)
				break;
			Swap(childIndex, parentIndex);
			childIndex = parentIndex;
		}
	}

	public TValue Dequeue()
	{
		var lastIndex = _data.Count - 1;
		var firstItem = _data[0];
		_data[0] = _data[lastIndex];
		_data.RemoveAt(lastIndex--);
		var parentIndex = 0;
		while (true)
		{
			var childIndex = parentIndex * 2 + 1;
			if (childIndex > lastIndex)
				break;
			var rightChild = childIndex + 1;
			if (rightChild <= lastIndex && Compare(_data[rightChild].Key, _data[childIndex].Key) < 0)
				childIndex = rightChild;
			if (Compare(_data[parentIndex].Key, _data[childIndex].Key) <= 0)
				break;
			Swap(parentIndex, childIndex);
			parentIndex = childIndex;
		}
		return firstItem.Value;
	}

	public TValue Peek()
	{
		return _data[0].Value;
	}

	private void Swap(int a, int b)
	{
		var tmp = _data[a];
		_data[a] = _data[b];
		_data[b] = tmp;
	}

	private int Compare(TKey a, TKey b)
	{
		return _isDescending ? _keyComparer.Compare(b, a) : _keyComparer.Compare(a, b);
	}

	public int Count => _data.Count;

	public IEnumerator<TValue> GetEnumerator()
	{
		return _data.Select(r => r.Value).GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
