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
	
	public class Inputter
	{
		public bool IsDebug { get; } = false;

		public static string _str =
	$@"3 3 0
100 -100 0
0 100 100
100 100 100
-100 100 100";

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

	static void Main()
	{
		var inputter = new Inputter();
		var inputs = inputter.GetNext().Split().Select(ToInt).ToArray();
		var n = inputs[0];
		var c = inputs[2];
		var b = inputter.GetNext().Split().Select(ToInt).ToArray();
		
		var ac = 0;
		
		foreach (var i in Ie(0, n))
		{
			if (inputter.GetNext().Split().Select(ToInt).Select((x, i) => x * b[i]).Sum() > -c) ac++;
		}
		
		Console.WriteLine(ac);
	}
}
















