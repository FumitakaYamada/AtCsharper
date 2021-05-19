<Query Kind="Program">
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
</Query>

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;

namespace Tasks
{
	public class A
	{
		public static void Main(string[] args)
		{
			var S = Console.ReadLine();
			var answer = 0L;
			var curr = 0;
			for (var i = 0; i < S.Length; i++)
			{
				if (S[i] == 'W') answer += i - curr++;
			}

			Console.WriteLine(answer);
		}
	}
}
