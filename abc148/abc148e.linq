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
		var n = inputter.GetNext().ToLong();

		if (n % 2 == 1)
		{
			Wl(0);
			return;
		}

		var sum = n / 10;
		
		n /= 10;
		
		long p5 = 5;
		
		while (p5 <= n)
		{
			sum += n / p5;
			p5 *= 5;
		}
		
		Wl(sum);
	}

	public class Inputter
	{
		//public bool IsDebug { get; } = true;
		public bool IsDebug { get; } = false;

		public static string _str =
	$@"1000000000000000000
	

";

		private int _index = 0;
		private string[] lines = null;

		private string[] GetLines()
		{
			if (lines == null)
			{
				lines = _str.Split("\n")
					.Select(x => x.Replace("\n", "").Replace($@"
", "").Replace("\r", ""))
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

	// a must be bigger than b
	public static int GetGcd(int a, int b)
	{
		var r = a % b;
		if (r == 0)
		{
			return b;
		}
		return GetGcd(b, r);
	}
	
	public static IEnumerable<int> Ie(int start, int count)
	{
		return Enumerable.Range(start, count);
	}

	public class P
	{
		public int X;
		public int Y;
		public int Score;

		public P(int x, int y, int score)
		{
			X = x;
			Y = y;
			Score = score;
		}

		public override string ToString()
		{
			return $"X : {X}, Y : {Y}, Score : {Score}";
		}
	}
	
	public class Point
	{
		public double X { get; }
		public double Y { get; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}

		public Point(double x, double y)
		{
			X = x;
			Y = y;
		}

		public Point(int[] nums)
		{
			X = nums[0];
			Y = nums[1];
		}

		public Point(double[] nums)
		{
			X = nums[0];
			Y = nums[1];
		}
	}

	public static double GetEuclidDistance(Point a, Point b)
	{
		return Math.Sqrt(Math.Pow(a.X - (float)b.X, 2) + Math.Pow(a.Y - (float)b.Y, 2));
	}

	public static Point RotateCounterClock(Point center, Point rotatePoint, double radius)
	{
		double cosTheta = Math.Cos(radius);
		double sinTheta = Math.Sin(radius);
		return new Point(
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
