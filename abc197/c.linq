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
		var str = Console.ReadLine();

		var inputs = str.Split(' ');

		var nums = inputs.Select(x => int.Parse(x)).ToArray();

		var height = nums[0];
		var width = nums[1];
		var initX = nums[2] - 1;
		var initY = nums[3] - 1;
		
		var strs = new List<string>();
		
		foreach(var i in Enumerable.Range(0, height))
		{
			strs.Add(Console.ReadLine());
		}
		
		//strs.Dump();

		var result = 1;
		var point = new { x = initX, y = initY };

		point = new { x = initX - 1, y = initY };
		while (point.x >= 0)
		{
			if (isClear(GetChar(strs, point.x, point.y)))
			{
				result++;
			} else {
				break;
			}
			point = new { x = point.x - 1, y = point.y };
		}

		point = new { x = initX + 1, y = initY };
		while (point.x < height)
		{
			if (isClear(GetChar(strs, point.x, point.y)))
			{
				result++;
			}
			else
			{
				break;
			}
			point = new { x = point.x + 1, y = point.y };
		}

		point = new { x = initX, y = initY - 1 };
		while (point.y >= 0)
		{
			if (isClear(GetChar(strs, point.x, point.y)))
			{
				result++;
			}
			else
			{
				break;
			}
			point = new { x = point.x, y = point.y - 1 };
		}

		point = new { x = initX, y = initY + 1 };
		while (point.y < width)
		{
			if (isClear(GetChar(strs, point.x, point.y)))
			{
				result++;
			}
			else
			{
				break;
			}
			point = new { x = point.x, y = point.y + 1 };
		}

		Console.WriteLine(result);
	}

	public static char GetChar(List<string> strs, int x, int y)
	{
		var charArray = strs[x].ToCharArray();
		return charArray[y];
	}
	
	public static bool isClear(char c) {
		return c.Equals('.');
	}
}
