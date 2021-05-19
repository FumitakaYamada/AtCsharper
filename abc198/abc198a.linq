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
		var input = Console.ReadLine();
		var n = input.toInt();
		

		if (n < 1) {
			Console.WriteLine(0);
			return;
		}
		else if (n == 2)
		{
			Console.WriteLine(1);
			return;
		}
		else
		{
			Console.WriteLine(n - 1);
			return;
		}
	}
}
