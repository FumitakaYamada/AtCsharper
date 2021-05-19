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

	static void Main()
	{
		var n = Console.ReadLine().toInt();
		
		int count = 0;
		foreach (var i in Enumerable.Range(1, 10000000)) {
			var str = i.ToString() + i.ToString();
			if (str.toInt() <= n) {
				count ++;
			} else {
				break;
			}
		}
		
		Console.WriteLine(count);
	}
}
