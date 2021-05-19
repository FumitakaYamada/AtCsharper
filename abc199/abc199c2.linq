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
	public static int ToLong(this string str)
	{
		
		return int.Parse(str);
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
	
	// 0起点
	static int GetFlipNum(int index, int n)
	{
		if (index >= n) {
			return index - n;
		} else {
			return index + n;
		}
	}


	static void Main()
	{
		var n = Console.ReadLine().ToLong();
		var s = Console.ReadLine();
		var q = Console.ReadLine().ToLong();
		
		var strLen = 2 * n;

		var queries = new List<int[]>() { };

		foreach (var i in Enumerable.Range(0, (int)q))
		{
			var query = Console.ReadLine().Split().Select(ToLong).ToArray();
			queries.Add(query);
		}

		

		//DEBUG
		//var n = 2;
		//var s = "FLIP";
		//var q = 6;
		//var strLen = 4;
		//var queries = new int[][] {
		//	new int [] {
		//		1, 1, 3,
		//	},new int [] {
		//		2, 0, 0
		//	},new int [] {
		//		1,1,2,
		//	},new int [] {
		//		1, 2,3,
		//	},new int [] {
		//		2, 0, 0,
		//	},new int [] {
		//		1, 1, 4,
		//	},
		//}.ToList();

		var flip = false;
		
		var numbers = Enumerable.Range(0, strLen).ToArray();
		
		foreach (var query in queries)
		{
			var t = query[0];
			
			if (t == 2) {
				flip = !flip;
				continue;
			}

			var a = query[1] - 1;
			var b = query[2] - 1;

			if (flip)
			{
				a = GetFlipNum(a, n);
				b = GetFlipNum(b, n);
			}

			var aVal = numbers[a];
			var bVal = numbers[b];
			
			numbers[a] = bVal;
			numbers[b] = aVal;
			
			//// debug
			//numbers.Dump();
		}
		
		var ca = s.ToCharArray();

		var cl = new List<char>();

		foreach (var i in Enumerable.Range(0, strLen))
		{
			cl.Add(ca[numbers[i]]);
		}

		s = new String(cl.ToArray());

		if (flip)
		{
			var zenhan = s.Substring(0, n);
			var kouhan = s.Substring(n, n);

			s = kouhan + zenhan;
		}
		
		
		Console.WriteLine(s);
	}
}



























