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

public class Masu
{
	public int Score { get; set; }
	public int TileId { get; set; }
}
 
public class Point
{
	public int Pi { get; set; }
	public int Pj { get; set; }
}
 
public static class PointExt
{
 
	public static Point GetPoint(this Point point, char str)
	{
		switch (str)
		{
			case 'U':
				return new Point()
				{
					Pi = point.Pi - 1,
					Pj = point.Pj,
				};
			case 'D':
				return new Point()
				{
					Pi = point.Pi + 1,
					Pj = point.Pj,
				};
			case 'L':
				return new Point()
				{
					Pi = point.Pi,
					Pj = point.Pj - 1,
				};
			case 'R':
				return new Point()
				{
					Pi = point.Pi,
					Pj = point.Pj + 1,
				};
			default:
				return null;
		}
	}
	public static bool IsInRange(this Point point)
	{
		return point.Pi >= 0 && point.Pi <= 49 && point.Pj >= 0 && point.Pj <= 49;
	}
}
 
public class PPath
{
	public List<int> UsedTiles { get; } = new List<int>();
	public List<char> MoveHistories { get; } = new List<char>();
	public Point CurrentPoint { get; set; }
	public int Score { get; set; }
 
	public string GetPath()
	{
		return new String(MoveHistories.ToArray());
	}
 
	public string GetCanMove(Masu[][] masus)
	{
		var left = CurrentPoint.GetPoint('L');
		var right = CurrentPoint.GetPoint('R');
		var up = CurrentPoint.GetPoint('U');
		var down = CurrentPoint.GetPoint('D');
 
		var moves = new List<string>();
 
		if (left.IsInRange() && !UsedTiles.Contains(masus[left.Pi][left.Pj].TileId))
		{
			moves.Add("L");
		}
 
		if (right.IsInRange() && !UsedTiles.Contains(masus[right.Pi][right.Pj].TileId))
		{
			moves.Add("R");
		}
 
		if (up.IsInRange() && !UsedTiles.Contains(masus[up.Pi][up.Pj].TileId))
		{
			moves.Add("U");
		}
 
		if (down.IsInRange() && !UsedTiles.Contains(masus[down.Pi][down.Pj].TileId))
		{
			moves.Add("D");
		}
 
		return String.Join(null, moves);
	}
	
	public bool CanMove(Masu[][] masus, char c)
	{
		var nextPoint = CurrentPoint.GetPoint(c);
		
		return (nextPoint.IsInRange() && !UsedTiles.Contains(masus[nextPoint.Pi][nextPoint.Pj].TileId));
	}
	
	public void Move(char move, Masu[][] masus)
	{
		CurrentPoint = CurrentPoint.GetPoint(move);
		var masu = masus[CurrentPoint.Pi][CurrentPoint.Pj];
		UsedTiles.Add(masu.TileId);
		MoveHistories.Add(move);
		Score += masu.Score;
	}
}

static class Program
{
	public static int ToInt(this string str)
	{
		return int.Parse(str);
	}
 
	public static PPath CreatePath(Point startPoint, Masu[][] masus, char[] initial = null, char yusen = ' ')
	{
		var path = new PPath()
		{
			CurrentPoint = startPoint,
		};
 
		path.UsedTiles.Add(masus[startPoint.Pi][startPoint.Pj].TileId);
 
 
		if (initial != null)
		{
			foreach (var c in initial)
			{
				path.Move(c, masus);
			}
		}
 
		var rand = new Random();
 
		while (true)
		{
			if (stop)
			{
				break;
			}
 
			var str = path.GetCanMove(masus);
 
			if (str.Length == 0)
			{
				break;
			}
 
			var ca = str.ToCharArray();
 
			char move;
 
			if (ca.Contains(yusen))
			{
				move = yusen;
			}
			else
			{
				move = ca[rand.Next() % ca.Length];
			}
 
			path.Move(move, masus);
		}
 
		return path;
	}
 
	public static PPath CreatePath2(Point startPoint, Masu[][] masus, char[] initial = null, char[] yusens = null)
	{
		var path = new PPath()
		{
			CurrentPoint = startPoint,
		};
 
		path.UsedTiles.Add(masus[startPoint.Pi][startPoint.Pj].TileId);
 
		if (initial != null)
		{
			foreach (var c in initial)
			{
				path.Move(c, masus);
			}
		}
 
		while (true)
		{
			if (stop)
			{
				break;
			}
			
			var deadEnd = 0;
			
			foreach (var u in yusens)
			{
				if (path.CanMove(masus, u))
				{
					path.Move(u, masus);
					break;
				} else {
					deadEnd++;
				}
			}
			
			if (deadEnd == 4) {
				break;
			}
		}
 
		return path;
	}

	static bool stop = false;
	static System.Timers.Timer timer = null;
 
	private static void Timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
	{
		timer.Enabled = false;
		stop = true;
	}
 
	static void Main()
	{
		stop = false;
		timer = new System.Timers.Timer();
		
		timer.Interval = 1.92 * 1000;
		timer.Elapsed += Timer1_Elapsed;
		timer.Enabled = true;
		
		var startPointInputs = Console.ReadLine().Split().Select(ToInt).ToArray();
 
		var startPoint = new Point()
		{
			Pi = startPointInputs[0],
			Pj = startPointInputs[1],
		};
 
		var tileNums = new List<int[]>();
 
		foreach (var i in Enumerable.Range(0, 50))
		{
			tileNums.Add(Console.ReadLine().Split().Select(ToInt).ToArray());
		}
 
		var masuList = new List<Masu[]>();
 
		foreach (var i in Enumerable.Range(0, 50))
		{
			var scoreNums = Console.ReadLine().Split().Select(ToInt).ToArray();
 
			var yokoMasus = new List<Masu>() {};
			
			foreach (var j in Enumerable.Range(0, 50))
			{
				var masu = new Masu()
				{
					TileId = tileNums[i][j],
					Score = scoreNums[j],
				};
 
				yokoMasus.Add(masu);
			}
			
			masuList.Add(yokoMasus.ToArray());
		}
		
		var masus = masuList.ToArray();
 
		var paths = new List<PPath>();
 
		PPath yosagePath = null;
 
		var yusens2 = new char[][] {
			new char[] {
				'D','L','U','R'
			},
			new char[] {
				'U','R','D','L'
			},
			new char[] {
				'D','L','U','R'
			},
			new char[] {
				'U','L','D','R'
			},
		};
 
		while (true)
		{
			if (stop)
			{
				break;
			}
 
			paths.Add(CreatePath(startPoint, masus));
 
			yosagePath = paths.OrderBy(x => x.Score).Last();
 
			var bestMove = yosagePath != null ? yosagePath.MoveHistories.ToArray() : null;
 
			foreach (var i in Enumerable.Range(15, Math.Max(bestMove.Length - 15, 0)))
			{
				if (stop)
				{
					break;
				}
				var newMove = bestMove.SkipLast(i).ToArray();
				paths.Add(CreatePath(startPoint, masus, newMove));
 
				foreach (var yus in yusens2)
				{
					if (stop)
					{
						break;
					}
					paths.Add(CreatePath2(startPoint, masus, null, yus));
				}
			}
		}
 
		var bestPath = paths.OrderBy(x => x.Score).Last();
		
		Console.WriteLine(new String(bestPath.GetPath()));
	}
}
