<Query Kind="Program">
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
</Query>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

static class Program
{
	static void Main()
	{
		var inputter = new Inputter();
		var s = inputter.GetNext();
		var t = inputter.GetNext();
		
		var sca = s.ToCharArray();
		var tca = t.ToCharArray();
		
		var dp = new long[s.Length + 1, t.Length + 1];
		
		foreach (var i in Ie(1, s.Length))
		{
			foreach (var j in Ie(1, t.Length))
			{
				if (sca[i - 1].Equals(tca[j - 1])) dp[i, j] = dp[i - 1, j - 1] + 1;
				else dp[i, j] = Math.Max(dp[i - 1, j], dp[i, j - 1]);
			}
		}
		
		var ca = new char[dp[s.Length, t.Length]];
		
		var si = s.Length;
		var ti = t.Length;
		var length = ca.Length - 1;
		
		while (length >= 0)
		{
			if (sca[si - 1].Equals(tca[ti - 1]))
			{
				ca[length] = sca[si - 1];
				si--;
				ti--;
				length--;
			}
			else if (dp[si, ti] == dp[si - 1, ti])
			{
				si--;
			} else
			{
				ti--;
			}
		}

		Wl(new String(ca));
	}

	public class Inputter
	{
		//public bool IsDebug { get; } = true;
		public bool IsDebug { get; } = false;

		public static string _str =
	$@"abracadabra
avadakedavra

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

	public static string ToSpaceString<T>(this IEnumerable<T> ie) => String.Join(' ', ie.ToArray());
	public static IEnumerable<long> ToLong(this IEnumerable<int> ie) => ie.Select(x => (long)x);
	public static long LongSum(this IEnumerable<int> ie) => ie.ToLong().Sum();
	public static void Wl(object obj = null) => Console.WriteLine(obj);
	public static long ToLong(this string str) => long.Parse(str);
	public static int ToInt(this string str) => int.Parse(str);
	public static long ToLong(this char ch) => long.Parse(ch.ToString());
	public static int ToInt(this char ch) => int.Parse(ch.ToString());
	public static double ToDouble(this string str) => double.Parse(str);
	public static long GetDigit(this long num) => (num == 0) ? 1 : ((long)Math.Log10(num) + 1);
	public static IEnumerable<int> Ie(long start, long count) => Enumerable.Range((int)start, (int)count);
	public static IEnumerable<int> Ie(long count) => Ie(0, count);
	public static T[][] Aa<T>(int first, int second) => Ie(first).Select(x => new T[second]).ToArray();
	public static T[][] Aa<T>(int first, int second, T init) => Ie(first).Select(x => Ie(second).Select(x => init).ToArray()).ToArray();

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
	public static IEnumerable<T> MinBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
	{
		return SelectBy(source, selector, (a, b) => Comparer<U>.Default.Compare(a, b) < 0);
	}

	public static IEnumerable<T> MaxBy<T, U>(this IEnumerable<T> source, Func<T, U> selector)
	{
		return SelectBy(source, selector, (a, b) => Comparer<U>.Default.Compare(a, b) > 0);
	}

	private static IEnumerable<T> SelectBy<T, U>(IEnumerable<T> source, Func<T, U> selector, Func<U, U, bool> comparer)
	{
		var list = new LinkedList<T>();
		U prevKey = default(U);
		foreach (var item in source)
		{
			var key = selector(item);
			if (list.Count == 0 || comparer(key, prevKey))
			{
				list.Clear();
				list.AddLast(item);
				prevKey = key;
			}
			else if (Comparer<U>.Default.Compare(key, prevKey) == 0)
			{
				list.AddLast(item);
			}
		}
		return list;
	}
	
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
