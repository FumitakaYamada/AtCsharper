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
		var in1 = Console.ReadLine().Split(' ').Select(x => x.toInt()).ToArray();
		var in2 = Console.ReadLine().Split(' ').Select(x => x.toInt()).ToArray();

		var a = in1[0];
		var b = in1[1];
		var c = in2[0];
		var d = in2[1];

		var x = a > b ? a : b;
		var y = c > d ? d : c;

		Console.WriteLine(x - y);
	}
}
