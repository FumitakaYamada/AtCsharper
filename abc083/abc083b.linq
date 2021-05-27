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
	
	static void Main()
	{
		var input = Console.ReadLine().Split(" ");
		var n = input[0].toInt();
		var a = input[1].toInt();
		var b = input[2].toInt();
		
		//var n = 20;
		//var a = 2;
		//var b = 5;
		
		var result = 0;
		
		foreach (var i in Enumerable.Range(1, n))
		{
			var sum = 0;
			foreach (var j in i.ToString().ToCharArray().Select(x => x.ToString().toInt()).ToArray())
			{
				sum += j;
			}

			if (a <= sum && sum <= b)
			{
				result += i;
			}
		}
		
		Console.WriteLine(result);
	}
}
