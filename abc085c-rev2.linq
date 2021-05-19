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
		var input = Console.ReadLine().Split(" ");
		var n = input[0].toInt();
		var y = input[1].toInt();

		var val = y / 1000;
		
		var saiteiSenenShiyouMaisuu = val - ((val / 5) * 5);

		var mansatuCount = val / 10;

		// 万札だけパターン
		if (saiteiSenenShiyouMaisuu == 0)
		{
			if (mansatuCount % 10 == 0 && mansatuCount == n)
			{
				Console.WriteLine("{0} {1} {2}", mansatuCount, 0, 0);
				return;
			}
		}

		while (mansatuCount >= 0)
		{
			if (mansatuCount >= n) {
				mansatuCount = n;
				continue;
			}

			if (mansatuCount == 14)
			{
				var a = 1;
			}

			var nokoriMaisuu = n - mansatuCount - saiteiSenenShiyouMaisuu;

			var nokoriKingaku = val - (mansatuCount * 10) - saiteiSenenShiyouMaisuu;
			
			var gosenOnlyMaisuu = nokoriKingaku / 5;
			
			var diffMaisuu = n - mansatuCount - gosenOnlyMaisuu - saiteiSenenShiyouMaisuu;
			
			if (diffMaisuu % 4 == 0) {
				var sen = diffMaisuu / 4 * 5 + saiteiSenenShiyouMaisuu;
				var gosen = nokoriMaisuu - sen + saiteiSenenShiyouMaisuu;

				if (gosen >= 0 && sen >= 0 && mansatuCount + gosen + sen == n && mansatuCount * 10 + gosen * 5 + sen == val)
				{
					Console.WriteLine("{0} {1} {2}", mansatuCount, gosen, sen);
					return;
				}
			}


			mansatuCount--;
		}
					
		
		//calcCount.Dump();
		
		Console.WriteLine("-1 -1 -1");
	}
}
