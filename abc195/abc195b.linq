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
		var inputs = Console.ReadLine().Split().Select(ToDouble).ToArray();
		var a = inputs[0];
		var b = inputs[1];
		var w = inputs[2] * 1000;

		var max = Math.Floor(w / a);
		var min = Math.Ceiling(w / b);

		if (a > w / min || w / min > b)
		{
			Console.WriteLine("UNSATISFIABLE");
			return;
		}
		
		if (a > w / max || w / max > b)
		{
			Console.WriteLine("UNSATISFIABLE");
			return;
		}

		Console.WriteLine(min + " " + max);
	}
}
