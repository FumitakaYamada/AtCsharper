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

	public static bool isReverse(this string str)
	{
		var ca = str.ToCharArray();
		var ca2 = ca.Reverse().ToArray();
		
		for (var i = 0; i < ca.Length; i++) {
			if (!ca[i].Equals(ca2[i])) {
				return false;
			}
		}
		
		return true;
	}

	static void Main()
	{
		var input = Console.ReadLine();

		if (isReverse(input))
		{
			Console.WriteLine("Yes");
			return;
		}
		
		var ketuZero = 0;

		var ca = input.ToCharArray();



		foreach (var c in ca.Reverse()) {
			if (c.Equals('0'))
			{
				ketuZero++;
				continue;
			}
			
			break;
		}

		if (ketuZero > 0)
		{
			foreach (var i in Enumerable.Range(1, ketuZero))
			{
				var head = "";

				foreach (var x in Enumerable.Range(1, i))
				{
					head = new String(head.Append('0').ToArray());
				}

				foreach (var c in input.ToCharArray())
				{
					head = new String(head.Append(c).ToArray());
				}


				if (isReverse(head))
				{
					Console.WriteLine("Yes");
					return;
				}
			}
		}

		Console.WriteLine("No");
	}
}
