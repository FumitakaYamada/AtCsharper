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
		var n = Console.ReadLine();
		var input = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToList();
		
		//var n = 4;
		//var input = new List<int>(){
		//	20, 18, 2, 18,
		//};
		
		var result = 0;
		
		var alice = 0;
		var bob = 0;
		
		int turn = 0;

		foreach (var num in input.OrderByDescending(x => x)) {
			if (turn % 2 == 0) {
				alice += num;
			} else {
				bob += num;
			}
			turn++;
		}
		
		Console.WriteLine(alice - bob);
	}
}
