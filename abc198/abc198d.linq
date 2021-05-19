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
	$@"asdfasdfas
ghjkghjkgh
qwqwqwqwhg";

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
	public static long ToLong(this string str)
	{
		return long.Parse(str);
	}

	public static int ToInt(this string str)
	{
		return int.Parse(str);
	}

	public static int ToInt(this char ch)
	{
		return int.Parse(ch.ToString());
	}

	public static double ToDouble(this string str)
	{
		return double.Parse(str);
	}

	// a ^ n mod mod
	public static long ModPow(long a, long n, long mod)
	{
		long res = 1;
		while (n > 0)
		{
			if ((n & 1) == 1)
			{
				res = res * a % mod;
			}
			a = a * a % mod;
			n = n >> 1;
		}
		return res;
	}

	static void Main()
	{
		var inputter = new Inputter();
		var a = inputter.GetNext();
		var b = inputter.GetNext();
		var c = inputter.GetNext();

		var aChars = new List<char>();
		var bChars = new List<char>();
		var cChars = new List<char>();

		foreach (var ch in a.ToCharArray())
		{
			aChars.Add(ch);
		}
		foreach (var ch in b.ToCharArray())
		{
			bChars.Add(ch);
		}
		foreach (var ch in c.ToCharArray())
		{
			cChars.Add(ch);
		}

		var allChars = new List<char>();

		allChars.AddRange(aChars);
		allChars.AddRange(bChars);
		allChars.AddRange(cChars);

		var distinctChars = allChars.Distinct().ToArray();

		if (distinctChars.Count() > 10) {
			Console.WriteLine("UNSOLVABLE");
			return;
		}

		var notZeroChars = new List<char>();

		notZeroChars.Add(aChars[0]);
		notZeroChars.Add(bChars[0]);
		notZeroChars.Add(cChars[0]);

		notZeroChars = notZeroChars.Distinct().ToList();


		var zeroToNine = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
		var oneToNine = new char[] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

		var al = aChars.Last();
		var bl = bChars.Last();
		var cl = cChars.Last();
		
		var af = aChars.First();
		var bf = bChars.First();
		var cf = cChars.First();
		
		bool allSame = false;
		
		if (aChars.Count == bChars.Count)
		{
			if (aChars.Count == cChars.Count)
			{
				allSame = true;
			}
		}
		
		var dic = new Dictionary<char, char>();
		var used = new List<char>();

		bool check()
		{
			if ((dic[al].ToInt() + dic[bl].ToInt()) % 10 != dic[cl].ToInt())
			{
				return false;
			}
			
			if (allSame)
			{
				var topDiff = dic[cf].ToInt() - dic[bf].ToInt() - dic[af].ToInt();
				if (topDiff != 0 && topDiff != 1)
				{
					return false;
				}
			}

			var anum = new String(a.ToCharArray().Select(x => dic[x]).ToArray()).ToLong();
			var bnum = new String(b.ToCharArray().Select(x => dic[x]).ToArray()).ToLong();
			var cnum = new String(c.ToCharArray().Select(x => dic[x]).ToArray()).ToLong();
			
			//(anum + " + " + bnum + " = " + cnum).Dump();

			if (anum + bnum == cnum)
			{
				Console.WriteLine(anum);
				Console.WriteLine(bnum);
				Console.WriteLine(cnum);

				return true;
			}
			
			return false;
		}

		bool dfs(int depth)
		{
			if (dic.Count == distinctChars.Length)
			{
				return check();
			}

			var ch = distinctChars[depth];

			foreach (var num in (notZeroChars.Contains(ch) ? oneToNine : zeroToNine).Where(x => !used.Contains(x)))
			{
				used.Add(num);
				dic.Add(ch, num);

				if (dfs(depth + 1))
				{
					return true;
				}

				dic.Remove(ch);
				used.Remove(num);
			}
			
			return false;
		}

		if (!dfs(0))
		{
			Console.WriteLine("UNSOLVABLE");
		}
	}
}
