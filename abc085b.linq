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
		var n = Console.ReadLine().toInt();
		var nums = new List<int>();
		foreach (var i in Enumerable.Range(1, n)) {
			nums.Add(Console.ReadLine().toInt());
		}
		
		Console.WriteLine(nums.Distinct().Count());
	}
}
