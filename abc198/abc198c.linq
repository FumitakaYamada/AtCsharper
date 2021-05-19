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

	public static double GetEuclidDistance(double x, double y, double x2, double y2)
	{
		var xd = (x - x2);
		var yd = (y - y2);
		
		return Math.Sqrt(Math.Pow(xd, 2) + Math.Pow(yd, 2));
	}

	static void Main()
	{
		var input = Console.ReadLine().Split(" ");
		var r = input[0].toInt();
		var x = input[1].toInt();
		var y = input[2].toInt();
		
		var distance = GetEuclidDistance(0, 0, x, y);
		
		
		var hosuu = distance / r;

		if (hosuu < 2 && hosuu != 1)
		{
			Console.WriteLine(2);
			return;
		}
		
		

		Console.WriteLine(Math.Ceiling(hosuu));
	}
}
