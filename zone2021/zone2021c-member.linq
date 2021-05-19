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
	$@"10
6 7 5 18 2
3 8 1 6 3
7 2 8 7 7
6 3 3 4 7
12 8 9 15 9
9 8 6 1 10
12 9 7 8 2
10 3 17 4 10
3 1 3 19 3
3 14 7 13 1";

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

	static void Main()
	{
		var inputter = new Inputter();
		var n = inputter.GetNext().ToInt();
		
		var list = new List<int[]> ();
		
		var ac = 0;
		
		foreach (var i in Enumerable.Range(1, n))
		{
			list.Add(inputter.GetNext().Split().Select(ToInt).ToArray());
		}

		void check(int[] selection)
		{
			var min = Enumerable.Range(0, 3).Select(n =>
			{
				var uses = selection.Select(x => x == n).ToArray();
				if (uses.Length == 0) return int.MaxValue;
				return list.Select(x => uses.Select((y, i) => y ? x[i] : int.MaxValue).Min()).Max();
			}).Min();

			if (min > ac)
			{
				ac = min;
			}
		}

		rep(3, i =>
		{
			rep(3, j =>
			{
				rep(3, k =>
				{
					rep(3, l =>
					{
						rep(3, m =>
						{
							check(new[] {
								i,j,k,l,m
							});
						});

					});

				});

			});
		});

		Console.WriteLine(ac);
	}
}
















