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
	public class Inputter
	{
		public bool IsDebug { get; } = false;

		public static string _str =
	$@"6 5
1 2
2 3
3 4
4 5
5 6";

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

	static void Main()
	{
		var inputter = new Inputter();
		var inputs = inputter.GetNext().Split().Select(ToInt).ToArray();
		var n = inputs[0];
		var m = inputs[1];

		var list = new List<KeyValuePair<int, int>>();
		
		foreach (var i in Enumerable.Range(0, m))
		{
			var nums = inputter.GetNext().Split().Select(ToInt).ToArray();
			list.Add(new KeyValuePair<int, int>(nums.Min(), nums.Max()));
		}
		
		var groups = new List<int[]>();
		
		int[] groupDfs(int num)
		{
			var nums = list.Where(x => x.Key == num).Select(x => x.Value).ToArray();
			
			var childs = nums.SelectMany(x => groupDfs(x)).ToArray();
			
			var all = childs.ToList();
			all.AddRange(nums);
			all.Add(num);
			
			return all.OrderBy(x => x).ToArray();
		}
		
		foreach (var i in Enumerable.Range(1, n))
		{
			if (groups.Any(x => x.Contains(i)))
			{
				continue;
			}
			
			groups.Add(groupDfs(i).Distinct().ToArray());
		}
		
		var multipliers = new List<int>();
		
		const int R = 1;
		const int G = 2;
		const int B = 3;

		foreach (var group in groups)
		{
			if (group.Count() == 1)
			{
				multipliers.Add(3);
				continue;
			}
			if (group.Count() == 2)
			{
				multipliers.Add(6);
				continue;
			}
			
			var min = group.Min();
			
			var patterns = 0;
			
			var dic = new Dictionary<int, int>();

			void dfs(int num)
			{
				if (dic.Count() == group.Count()) {
					patterns++;
					return;
				}

				var ten = group[num];

				var otonarisans = list.Where(x => x.Key == ten).Select(x => x.Value).ToList();
				otonarisans.AddRange(list.Where(x => x.Value == ten).Select(x => x.Key));
				
				var used = otonarisans.Where(x => dic.ContainsKey(x)).Select(x => dic[x]).ToArray();
				
				foreach (var color in new[] { R, G, B })
				{
					if (used.Contains(color))
					{
						continue;
					}
					
					dic.Add(ten, color);
					dfs(num + 1);
					dic.Remove(ten);
				}
			}

			dfs(0);
			multipliers.Add(patterns);
		}

		long result = 1;
		
		foreach (var i in multipliers)
		{
			result *= i;
		}

		Console.WriteLine(result);
	}
}
























