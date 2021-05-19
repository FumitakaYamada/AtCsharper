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
		var input1 = Console.ReadLine();
		var input2 = Console.ReadLine();

		var numbers = input2.Split(' ');

		var min = numbers.Select(x => GetDev(int.Parse(x))).Min();
		
		Console.WriteLine(min);
	}
	
	public static int GetDev(int num)
	{
		var i = 0;
		while (num % 2 == 0)
		{
			num = num / 2;
			i++;
		}
		return i;
	}
}
