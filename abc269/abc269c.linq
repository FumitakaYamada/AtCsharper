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
	const int M = 1000000007;
	static int debug = 2;
	
	static void Function(Inputter inputter)
	{
		var n = inputter.GetNext().ToLong();
		
		var k = new bool[60];
		
		foreach (var i in Ie(60))
		{
			k[i] = n % 2 == 1;
			n = n / 2;
		}
		
		//k.Dump();
		
		var results = new List<long>();
		results.Add(0);
		Wl(0);
		
		foreach (var i in Ie(60))
		{
			if (k[i])
			{
				foreach (var r in results.ToArray())
				{
					var num = r + (long)Math.Pow(2, i);
					results.Add(num);
					Wl(num);
				}
			}
		}

		//Wl(results);
	}

	static void Main()
	{
		if (debug == 1)
			foreach (var i in Ie(1, Inputter.GetCount()))
			{
				var inputter = new Inputter(){ Num = i };
				Function(inputter);
			}
		else
			Function(new Inputter());
	}

	public class Inputter
	{
		public long Num { get; set; } = 1;

		public static string _str1 =
	$@"11

";
		public static string _str2 =
	$@"0

";
		public static string _str3 =
	$@"576461302059761664

";
		public static string _str4 =
	$@"65535
";
		public static string _str5 =
	$@"
";

		public static int GetCount()
		{
			if (_str1.Length <= 2)
			{
				debug = 0;
				return 1;
			}
			if (_str2.Length <= 2) return 1;
			if (_str3.Length <= 2) return 2;
			if (_str4.Length <= 2) return 3;
			if (_str5.Length <= 2) return 4;
			return 5;
		}
		private int _index = 0;
		private string[] lines = null;
		private string[] GetLines()
		{
			var strs = new [] { _str1, _str2, _str3, _str4, _str5 };
			if (lines == null)
				lines = strs[Num - 1].Split("\n")
					.Select(x => x.Replace("\n", "").Replace("\r", ""))
					.ToArray();
			return lines;
		}
		public string GetNext()
		{
			if (debug == 1)
			{
				var str = GetLines()[_index];
				_index++;
				return str;
			}
			return Console.ReadLine();
		}
	}

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
	public static long LowerBound<T>(T[] a, T v) => LowerBound(a, v, Comparer<T>.Default);
	public static long LowerBound<T>(T[] a, T v, Comparer<T> cmp)
	{
		var ac = 0;
		var wc = a.Length - 1;
		while (ac <= wc)
		{
			var wj = ac + (wc - ac) / 2;
			var res = cmp.Compare(a[wj], v);
			if (res == -1) ac = wj + 1;
			else wc = wj - 1;
		}
		return ac;
	}
	public static long UpperBound<T>(T[] a, T v) => UpperBound(a, v, Comparer<T>.Default);
	public static long UpperBound<T>(T[] a, T v, Comparer<T> cmp)
	{
		var ac = 0;
		var wc = a.Length - 1;
		while (ac <= wc)
		{
			var wj = ac + (wc - ac) / 2;
			var res = cmp.Compare(a[wj], v);
			if (res <= 0) ac = wj + 1;
			else wc = wj - 1;
		}
		return ac;
	}
	public static Random rand = new Random();
	public static char GetRandomAlphabetChar() => ("abcdefghijklmnopqrstuvwxyz".ToCharArray()[rand.Next() % 26]);
	public static string ToSpaceString<T>(this IEnumerable<T> ie) => String.Join(' ', ie.ToArray());
	public static IEnumerable<long> ToLong(this IEnumerable<int> ie) => ie.Select(x => (long)x);
	public static long Max(long a, long b) => Math.Max(a, b);
	public static long Min(long a, long b) => Math.Min(a, b);
	public static double Max(double a, double b) => Math.Max(a, b);
	public static double Min(double a, double b) => Math.Min(a, b);
	public static void Wl(object obj = null) => Console.WriteLine(obj);
	public static long ToLong(this string str) => long.Parse(str);
	public static long ToLong(this char ch) => long.Parse(ch.ToString());
	public static double ToDouble(this string str) => double.Parse(str);
	public static long GetDigit(this long num) => (num == 0) ? 1 : ((long)Math.Log10(num) + 1);
	public static IEnumerable<long> Ie(long start, long count) => Enumerable.Range((int)start, (int)count).ToLong();
	public static IEnumerable<long> Ie(long count) => Ie(0, count);
	public static string ToCString(this char[] ca) => new String(ca);
	public static TValue TryGet<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue def = default(TValue)) { TValue val; return dic.TryGetValue(key, out val) ? val : def; }
	public static void RemoveLast<T>(this List<T> list) => list.RemoveAt(list.Count() - 1);
	public static double GetEuclidDistance(double x1, double x2, double y1, double y2) => Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
	public static long GetGcd(this IEnumerable<long> numbers) => numbers.Aggregate(GetGcd);
	public static SortedDictionary<TKey, TElement> ToSortedDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull => new SortedDictionary<TKey, TElement>(source.ToDictionary(keySelector, elementSelector, null));
	public static void AddOrCreate<TKey, TElement>(this Dictionary<TKey, List<TElement>> dic, TKey key, TElement value)
	{
		if (!dic.ContainsKey(key)) dic.Add(key, new List<TElement>());
		dic[key].Add(value);
	}
	public static List<int> AllIndexesOf(this string str, string value)
	{
		if (String.IsNullOrEmpty(value))
			throw new ArgumentException("the string to find may not be empty", "value");
		List<int> indexes = new List<int>();
		for (int index = 0; ; index += value.Length)
		{
			index = str.IndexOf(value, index);
			if (index == -1)
				return indexes;
			indexes.Add(index);
		}
	}
	
	// a ^ n mod mod
	public static long ModPow(long a, long n, long mod = M)
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

	public static long ModInv(long a, long m = M)
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

	public static bool IsPrime(long n)
	{
		if (n < 2) return false;
		for (var i = 2; i * i <= n; i++)
			if (n % i == 0) return false;
		return true;
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

	public static IEnumerable<long> GetDivisors(this long num)
	{
		if (num < 1) yield break;

		for (long i = 1; i * i <= num; i++)
		{
			if (num % i == 0)
			{
				yield return i;
				if (i * i != num) yield return (num / i);
			}
		}
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
			else i++;
		}
		if (tmp != 1L) yield return tmp;
	}

	public static long GetLcm(long a, long b)
	{
		return a * b / GetGcd(a, b);
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
