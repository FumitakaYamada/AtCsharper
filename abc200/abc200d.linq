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
		var n = inputter.GetNext().ToInt();
		var inputs = inputter.GetNext().Split().Select(ToLong).ToArray();
		
		n = 200;
		inputs = Ie(0, 200).Select(x => (long)x).ToArray();
		
		var l = new List<int>();
		
		var done = false;
		
		void dfs()
		{
			if (done)
			{
				return;
			}
			
			if (l.Count() == n)
			{
				var a = inputs.Where((x, i) => l[i] == 0).ToArray();
				var b = inputs.Where((x, i) => l[i] == 1).ToArray();
				
				if (a.Sum() == b.Sum())
				{
					"yah".Dump();
					done = true;
				}
				return;
			}
			
			foreach (var i in Ie(0, 3))
			{
				l.Add(i);
				dfs();
				l.RemoveAt(l.Count() - 1);
			}
		}
		
		dfs();

		Wl();
	}

	public class Inputter
	{
		public bool IsDebug { get; } = true;
		//public bool IsDebug { get; } = false;

		public static string _str =
	$@"5
180 186 189 191 218";

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
















