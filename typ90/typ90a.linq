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
	public static int toInt(this string str)
	{
		return int.Parse(str);
	}
	
	// a ^ n mod mod
	public static int modpow(int a, int n, int mod)
	{
		var res = 1;
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
		var input = Console.ReadLine().Split().Select(toInt).ToArray();
		var n = input[0];
		var l = input[1];
		var k = Console.ReadLine().toInt();
		var numbers = Console.ReadLine().Split().Select(toInt).ToArray();

		var pieceLengths = new List<int>();

		var i = 0;
		
		foreach (var point in numbers) {
			var length = point - i;
			pieceLengths.Add(length);
			i = point;
		}
		
		// 最後のピース
		pieceLengths.Add(l - i);
		
		var mergeCount = n - k;
		
		foreach (var count in Enumerable.Range(1, mergeCount))
		{
			
		}
		
		
		Console.WriteLine(pieceLengths.Min(x => x));
	}
}
