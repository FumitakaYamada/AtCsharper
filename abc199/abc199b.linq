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


	static void Main()
	{
		var n = Console.ReadLine().ToLong();
		var aInputs = Console.ReadLine().Split().Select(ToLong).ToArray();
		var bInputs = Console.ReadLine().Split().Select(ToLong).ToArray();
		
		var numbers = new List<int>();

		if (aInputs[0] > bInputs[0])
		{
			Console.WriteLine(0);
			return;
		}
		
		numbers = Enumerable.Range(aInputs[0], bInputs[0] - aInputs[0] + 1).ToList();
		
		for (var i = 1; i < aInputs.Length; i++) {


			var unmn = new List<int>();
			
			foreach (var num in numbers) {
				if (aInputs[i] > num || num > bInputs[i]) {
					unmn.Add(num);
				}
			}
			numbers.RemoveAll(x => unmn.Contains(x));
		}

		Console.WriteLine(numbers.Count());
	}
}
