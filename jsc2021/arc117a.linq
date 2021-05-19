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
		var input = Console.ReadLine().Split(' ');

		var a = (int)input[0].toInt();
		var b = (int)input[1].toInt();
		
		if (a == b) {
			var result = Enumerable.Range(1, a).ToList();
			result.AddRange(Enumerable.Range(1, b).Select(x => -x).ToArray());
			Console.WriteLine(String.Join(' ', result));
			return;
		}

		var plusIsBigger = a > b;

		var bigger = a > b ? a : b;
		var smaller = a > b ? b : a;
		
		var biggerSmallestSum = Enumerable.Range(1, bigger).Sum();
		var smallerSmallestSum = Enumerable.Range(1, smaller).Sum();
		
		var sumDiff = biggerSmallestSum - smallerSmallestSum;
		
		if (sumDiff <= (1000000000 - smaller)) {
			var biggerList = Enumerable.Range(1, bigger).ToList();
			var smallerList = Enumerable.Range(1, smaller).ToList();
			smallerList.Remove(smaller);
			smallerList.Add(sumDiff + smaller);
						
			if (plusIsBigger) {
				smallerList = smallerList.Select(x => -x).ToList();
			}
			else
			{
				biggerList = biggerList.Select(x => -x).ToList();
			}

			biggerList.AddRange(smallerList);

			Console.WriteLine(String.Join(' ', biggerList));
			return;
		}
		


		Console.WriteLine();
	}
}
