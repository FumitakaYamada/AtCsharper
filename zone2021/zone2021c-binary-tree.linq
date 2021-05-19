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
		
		var powers = 5;
		var ac = 0;
		var wa = 1001001001;
		
		foreach (var i in Enumerable.Range(1, n))
		{
			list.Add(inputter.GetNext().Split().Select(ToInt).ToArray());
		}

		while (ac + 1 < wa)
		{
			var wj = (ac + wa) / 2;
			
			var overList = new List<int>();
			
			foreach (var i in list)
			{
				var num = 0;
				rep(i.Count(), x => {
					num += (i[x] >= wj ? 1 : 0) << x;
				});
				overList.Add(num);
			}
			
			overList = overList.Distinct().ToList();
			
			var ok = false;
			
			rep(overList.Count(), i => {
				rep(i+1, j => {
					rep(i+1, k => {
						if ((overList[i] | overList[j] | overList[k]) == ((1 << powers) - 1)) ok = true;
					});
				});
			});

			if (ok)
			{
				ac = wj;
			}
			else
			{
				wa = wj;
			}
		}
		


		Console.WriteLine(ac);
	}
}
















