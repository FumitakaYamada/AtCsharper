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
	$@"4
1 3 3 1";

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


	static void Main()
	{
		var inputter = new Inputter();
		var n = inputter.GetNext().ToInt();
		var inputs = inputter.GetNext().Split().Select(ToInt).ToArray();
		
		var total = inputs.Count();
		
		var min = int.MaxValue;
		
		var groups = new List<int[]>();
		var used = 0;
		
		void calc()
		{
			var vals = new List<int>();
			foreach (var g in groups)
			{
				var val = g.First();
				foreach (var i in g.Skip(1))
				{
					val = val | i;
				}
				
				vals.Add(val);
			}
			
			var first = vals.First();
			
			foreach (var v in vals.Skip(1))
			{
				first = first ^ v;
			}
			
			if (first < min)
			{
				min = first;
			}
		}
		
		void dfs()
		{
			if (total == used)
			{
				calc();
				return;
			}
			
			foreach (var i in Enumerable.Range(1, total - used))
			{
				var adding = inputs.Skip(used).Take(i).ToArray();
				groups.Add(adding);
				used += i;
				dfs();
				used -= i;
				groups.Remove(adding);
			}
		}
		
		dfs();
		
		Console.WriteLine(min);
	}
}
