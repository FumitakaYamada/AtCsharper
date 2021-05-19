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
	public class Inputter
	{
		public bool IsDebug { get; } = false;

		public static string _str =
	$@"16 16 8 16";

		private int _index = 0;
		private string[] lines = null;

		private string[] GetLines()
		{
			if (lines == null)
			{
				lines = _str.Split("\n")
					.Select(x => x.Replace("\n", "").Replace($@"
", "").Replace("\r", ""))
					.ToArray();
			}
			return lines;
		}

		public string GetNext()
		{
			if (IsDebug)
			{
				var str = GetLines()[_index];
				_index++;
				return str;
			}
			else
			{
				return Console.ReadLine();
			}
		}
	}
	
	public static int toInt(this string str)
	{
		return int.Parse(str);
	}

	public class Point
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Point(int x, int y)
		{
			X = x;
			Y = y;
		}
	}

	static void Main()
	{
		var inputter = new Inputter();
		var input = inputter.GetNext().Split(' ');

		var h = input[0].toInt();
		var w = input[1].toInt();
		var a = input[2].toInt();
		var b = input[3].toInt();

		var used = Enumerable.Range(0, w * h).Select(x => false).ToArray();
		
		var patterns = 0;
		
		var usedRect = 0;
		var usedSmall = 0;
		var current = new Point(0, 0);
		
		//var stack = new List<string>();
		
		void moveCurrent()
		{
			if (current.X == w -1)
			{
				current.X = 0;
				current.Y++;
				return;
			}
			
			current.X++;
		}
		
		void backCurrent()
		{
			if (current.X == 0)
			{
				current.X = w - 1;
				current.Y--;
				return;
			}
			
			current.X--;
		}

		void dfs()
		{
			if (current.X == 0 && current.Y == h)
			{
				patterns++;
				
				return;
			}
			
			if (used[current.X + current.Y * w])
			{
				moveCurrent();
				dfs();
				backCurrent();
				return;
			}
			
			foreach (var i in Enumerable.Range(0, 3))
			{
				switch (i)
				{
					case 0://ちょめ
						if (usedSmall == b)
						{
							continue;
						}
						//stack.Add("small");
						used[current.X + current.Y * w] = true;
						moveCurrent();
						usedSmall++;
						dfs();
						usedSmall--;
						backCurrent();
						used[current.X + current.Y * w] = false;
						//stack.RemoveAt(stack.Count - 1);
						break;
					case 1://よこなが
						if (usedRect == a || current.X >= w - 1 || used[current.X + 1 + current.Y * w])
						{
							continue;
						}
						//stack.Add("yoko");
						used[current.X + current.Y * w] = true;
						used[current.X + 1 + current.Y * w] = true;
						moveCurrent();
						usedRect++;
						dfs();
						usedRect--;
						backCurrent();
						used[current.X + current.Y * w] = false;
						used[current.X + 1 + current.Y * w] = false;
						//stack.RemoveAt(stack.Count - 1);
						break;
					case 2://たてなが
						if (usedRect == a || current.Y == h - 1)
						{
							continue;
						}
						//stack.Add("tate");
						used[current.X + current.Y * w] = true;
						used[current.X + current.Y * w + w] = true;
						moveCurrent();
						usedRect++;
						dfs();
						usedRect--;
						backCurrent();
						used[current.X + current.Y * w] = false;
						used[current.X + current.Y * w + w] = false;
						//stack.RemoveAt(stack.Count - 1);
						break;
				}
			}
		}

		dfs();

		Console.WriteLine(patterns);
	}
}
