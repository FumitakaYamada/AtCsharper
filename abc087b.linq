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

class Program
{
	static void Main()
	{
		var a = int.Parse(Console.ReadLine());
		var b = int.Parse(Console.ReadLine());
		var c = int.Parse(Console.ReadLine());
		var x = int.Parse(Console.ReadLine());

		var result = 0;
		
		foreach (var i in Enumerable.Range(0, a + 1))
		{
			foreach (var j in Enumerable.Range(0, b + 1))
			{
				foreach (var k in Enumerable.Range(0, c + 1))
				{
					//Console.WriteLine("{0},{1},{2}", i, j, k);
					//(i * 500 + j * 100 + k * 50).Dump();
					if (i * 500 + j * 100 + k * 50 == x) {
						result ++;
					}
				}
			}
		}


		Console.WriteLine(result);
	}
}
