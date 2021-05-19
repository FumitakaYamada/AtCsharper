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
	
	static bool CheckContains(string str, List<string> words, int index)
	{
		for (var i = 5; i <= 7; i++)
		{
			var s = str.ToCharArray().Skip(index).Take(i);
			if (words.Contains(str))
			{
				CheckContains(str, words, index + i);
			}
			else
			{
				if (i == 5)
				{
					return false;
				}
				else
				{
					
				}
			}
		}
	}

	static void Main()
	{
		//var str = Console.ReadLine();
		var baseStr = "erasereraser";

		var words = new List<string>(){"dream", "dreamer", "erase", "eraser"};
		
		var index = 0;
		
		if (!CheckContains(baseStr, words, index))
		{
			Console.WriteLine("NO");
			return;
		}

		Console.WriteLine("YES");
	}
}
