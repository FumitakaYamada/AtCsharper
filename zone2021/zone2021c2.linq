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
	$@"5
6 13 6 19 11
4 4 12 11 18
20 7 19 2 5
15 5 12 20 7
8 7 6 18 5";

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

	public class Man
	{
		public int Id {get;set;}
		public int A { get; set; }
		public int B { get; set; }
		public int C { get; set; }
		public int D { get; set; }
		public int E { get; set; }
		
		public Dictionary<int, int> _powers = null;

		public Dictionary<int, int> GetPowers()
		{
			if (_powers == null)
			{
				_powers = new Dictionary<int, int>()
				{
					{1, A},
					{2, B},
					{3, C},
					{4, D},
					{5, E},
				};
			}
			return _powers;
		}
		
		public int GetKiiteruMin(int[] kiiteru)
		{
			return kiiteru.Select(x => _powers[x]).Min();
		}
	}

	public static int GetPower(IEnumerable<Man> mans)
	{
		var powers = new int[] {
			mans.Max(x => x.A),
			mans.Max(x => x.B),
			mans.Max(x => x.C),
			mans.Max(x => x.D),
			mans.Max(x => x.E),
		};
		return powers.Min();
	}

	public static int[] GetPowers(IEnumerable<Man> mans)
	{
		return new int[] {
			mans.Max(x => x.A),
			mans.Max(x => x.B),
			mans.Max(x => x.C),
			mans.Max(x => x.D),
			mans.Max(x => x.E),
		};
	}

	public static int[] GetKiiteru(IEnumerable<Man> mans, Man man)
	{
		var exceptMans = mans.ToList();
		exceptMans.Remove(man);

		var list = new List<int>();

		if (exceptMans.Max(x => x.A) < man.A)
		{
			list.Add(1);
		}
		if (exceptMans.Max(x => x.B) < man.B)
		{
			list.Add(2);
		}
		if (exceptMans.Max(x => x.C) < man.C)
		{
			list.Add(3);
		}
		if (exceptMans.Max(x => x.D) < man.D)
		{
			list.Add(4);
		}
		if (exceptMans.Max(x => x.E) < man.E)
		{
			list.Add(5);
		}
		return list.ToArray();
	}
	
	public static int[][] GetAllComb()
	{
		return new int[][] {
			new int[] {
				1,2
			},
			new int[] {
				1,3
			},
			new int[] {
				1,4
			},
			new int[] {
				1,5
			},
			new int[] {
				2,3
			},
			new int[] {
				2,4
			},
			new int[] {
				2,5
			},
			new int[] {
				3,4
			},
			new int[] {
				3,5
			},
			new int[] {
				4,5
			}
		};
	}

	static void Main()
	{
		var inputter = new Inputter();
		var n = inputter.GetNext().ToInt();
		
		var mans = new List<Man>();

		foreach (var i in Enumerable.Range(1, n))
		{
			var inputs = inputter.GetNext().Split().Select(ToInt).ToArray();

			mans.Add(new Man()
			{
				Id = i,
				A = inputs[0],
				B = inputs[1],
				C = inputs[2],
				D = inputs[3],
				E = inputs[4],
			});
		}

		var amax = mans.Max(x => x.A);
		var bmax = mans.Max(x => x.A);
		var cmax = mans.Max(x => x.A);
		var dmax = mans.Max(x => x.A);
		var emax = mans.Max(x => x.A);
		
		var maxMin = new []{
			amax,bmax,cmax,dmax,emax
		}.Min();

		foreach(var i in Enumerable.Range(0, maxMin).Reverse())
		{
			var kouhos = mans.Where(x => x.GetPowers().Values.Any(x => x >= i)).ToArray();
			
			if (kouhos.Length < 3)
			{
				continue;
			}
			
			foreach (var kouho1 in kouhos)
			{
				foreach (var kouho2 in kouhos.Where(x => x.Id != kouho1.Id))
				{
					foreach (var kouho3 in kouhos.Where(x => x.Id != kouho1.Id && x.Id != kouho2.Id))
					{
						var power = GetPower(new [] {
							kouho1, kouho2, kouho3
						});

						if (power == i)
						{

							Console.WriteLine(i);
							return;
						}
					}
				}
			}
		}
		
		//foreach (var man in mans)
		//{
		//	var best = 0;
		//	
		//	foreach (var comb in GetAllComb())
		//	{
		//		var one = man.GetPowers()[comb[0]];
		//		var two = man.GetPowers()[comb[1]];
		//		
		//		var min = one > two ? two : one;
		//		
		//		if (min > best)
		//		{
		//			best = min;
		//		}
		//	}
		//	
		//	
		//}


		Console.WriteLine();
	}
}
















