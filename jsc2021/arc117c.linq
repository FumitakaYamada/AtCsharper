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
	public static long toInt(this string str)
	{
		return long.Parse(str);
	}

	// a ^ n mod mod
	public static long modpow(long a, long n, long mod)
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
		Console.ReadLine();
		var colors = Console.ReadLine().ToCharArray();
		
		var charArray = colors.ToArray();
		
		for (var i = 0: i < colors.Length; i++) {
			
		}
		
		Console.WriteLine();
	}
}
