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
	public class Route
	{
		public int Small {get;set;}
		public int Large {get;set;}
		
		public Route(int x, int y) {
			if (x > y) {
				Small = y;
				Large = x;
			}
			else
			{
				Small = x;
				Large = y;
			}
		}
	}
	
	public static int toInt(this string str)
	{
		return int.Parse(str);
	}

	// i must be smaller than j
	public static int[][] FindRoutes(Route[] routes, int i, int j)
	{
		var resRoots = new List<List<Route>> ();
		var firsts = routes.Where(x => x.Small == i).Where(x => x.Large <= j).ToArray();
		foreach (var x in firsts) {
			var route = new List<Route>();
			route.Add(x);
			
			var iterator = 0;
			while (route.Last().Large != j || iterator <= j - i) {
				var point = route.Last().Large;
				routes.Where
			}
			
			resRoots.Add(route);
		}
	}

	static void Main()
	{
		var n = Console.ReadLine().toInt();
		var colors = Console.ReadLine().Split(" ");

		var routes = new List<Route> {};
		
		foreach (var i in Enumerable.Range(1, n - 1)) {
			var route = Console.ReadLine().Split(" ");
			routes.Add(new Route(route[0].toInt(), route[1].toInt()));
		}

		foreach (var i in Enumerable.Range(1, n))
		{
			if (i == 1)
			{
				Console.WriteLine(i);
				continue;
			}
			
			
		}

		Console.WriteLine();
	}
}
