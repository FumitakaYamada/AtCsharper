<Query Kind="Program">
  <Namespace>System.Windows.Forms.DataVisualization.Charting</Namespace>
</Query>

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Math;
using System.Text;
using System.Threading;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Library;

namespace Program
{
    public static class ProblemE
    {
        static bool SAIKI = false;
        static public int numberOfRandomCases = 0;
        static public void MakeTestCase(List<string> _input, List<string> _output, ref Func<string[], bool> _outputChecker)
        {
        }
        static public void Solve()
        {
            {
                System.Diagnostics.Process p = new System.Diagnostics.Process();

                p.StartInfo.FileName = "cat";
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.Arguments = @"/proc/cpuinfo";

                p.Start();

                string results = p.StandardOutput.ReadToEnd();

                p.WaitForExit();
                p.Close();

                if (!results.Contains(@"Intel(R) Xeon(R) Platinum 8275CL CPU @ 3.00GHz")) throw new Exception();
            }

            var N = NN;
            var M = NN;
            var ABCD = Repeat(0, M).Select(_ => new { A = NN - 1, B = NN - 1, C = NN, D = NN }).ToArray();
            var path = Repeat(0, N).Select(_ => new List<(long v, long C, long D)>()).ToArray();
            foreach (var item in ABCD)
            {
                path[item.A].Add((item.B, item.C, item.D));
                path[item.B].Add((item.A, item.C, item.D));
            }
            var nodeNum = N;
            var start = 0;
            var dist = Repeat(long.MaxValue >> 2, nodeNum).ToArray();
            dist[start] = 0;
            var q = new LIB_PriorityQueue();
            q.Push(0, (int)start);
            while (q.Count > 0)
            {
                var u = q.Pop();
                if (dist[u.Item2] < u.Item1) continue;
                foreach (var pathItem in path[u.Item2])
                {
                    var v = pathItem.v;
                    var rd = (long)Sqrt(pathItem.D);
                    var add = Max(0, rd - dist[u.Item2]);
                    var minv = long.MaxValue;
                    for (var i = Max(0, add - 1); i <= add + 1; ++i)
                    {
                        var time = dist[u.Item2] + i;
                        var chk = time + pathItem.C + (pathItem.D / (time + 1));
                        minv.Chmin(chk);
                    }
                    var alt = minv;
                    if (dist[v] > alt)
                    {
                        dist[v] = alt;
                        q.Push(alt, (int)v);
                    }
                }
            }
            if (dist[N - 1] >= (long.MaxValue >> 2)) Console.WriteLine(-1);
            else Console.WriteLine(dist[N - 1]);
        }
        class Printer : StreamWriter
        {
            public override IFormatProvider FormatProvider { get { return CultureInfo.InvariantCulture; } }
            public Printer(Stream stream) : base(stream, new UTF8Encoding(false, true)) { base.AutoFlush = false; }
            public Printer(Stream stream, Encoding encoding) : base(stream, encoding) { base.AutoFlush = false; }
        }
        static LIB_FastIO fastio = new LIB_FastIODebug();
        static public void Main(string[] args) { if (args.Length == 0) { fastio = new LIB_FastIO(); Console.SetOut(new Printer(Console.OpenStandardOutput())); } if (SAIKI) { var t = new Thread(Solve, 134217728); t.Start(); t.Join(); } else Solve(); Console.Out.Flush(); }
        static long NN => fastio.Long();
        static double ND => fastio.Double();
        static string NS => fastio.Scan();
        static long[] NNList(long N) => Repeat(0, N).Select(_ => NN).ToArray();
        static double[] NDList(long N) => Repeat(0, N).Select(_ => ND).ToArray();
        static string[] NSList(long N) => Repeat(0, N).Select(_ => NS).ToArray();
        static long Count<T>(this IEnumerable<T> x, Func<T, bool> pred) => Enumerable.Count(x, pred);
        static IEnumerable<T> Repeat<T>(T v, long n) => Enumerable.Repeat<T>(v, (int)n);
        static IEnumerable<int> Range(long s, long c) => Enumerable.Range((int)s, (int)c);
        static IOrderedEnumerable<T> OrderByRand<T>(this IEnumerable<T> x) => Enumerable.OrderBy(x, _ => xorshift);
        static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> x) => Enumerable.OrderBy(x.OrderByRand(), e => e);
        static IOrderedEnumerable<T1> OrderBy<T1, T2>(this IEnumerable<T1> x, Func<T1, T2> selector) => Enumerable.OrderBy(x.OrderByRand(), selector);
        static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> x) => Enumerable.OrderByDescending(x.OrderByRand(), e => e);
        static IOrderedEnumerable<T1> OrderByDescending<T1, T2>(this IEnumerable<T1> x, Func<T1, T2> selector) => Enumerable.OrderByDescending(x.OrderByRand(), selector);
        static IOrderedEnumerable<string> OrderBy(this IEnumerable<string> x) => x.OrderByRand().OrderBy(e => e, StringComparer.OrdinalIgnoreCase);
        static IOrderedEnumerable<T> OrderBy<T>(this IEnumerable<T> x, Func<T, string> selector) => x.OrderByRand().OrderBy(selector, StringComparer.OrdinalIgnoreCase);
        static IOrderedEnumerable<string> OrderByDescending(this IEnumerable<string> x) => x.OrderByRand().OrderByDescending(e => e, StringComparer.OrdinalIgnoreCase);
        static IOrderedEnumerable<T> OrderByDescending<T>(this IEnumerable<T> x, Func<T, string> selector) => x.OrderByRand().OrderByDescending(selector, StringComparer.OrdinalIgnoreCase);
        static string Join<T>(this IEnumerable<T> x, string separator = "") => string.Join(separator, x);
        static uint xorshift { get { _xsi.MoveNext(); return _xsi.Current; } }
        static IEnumerator<uint> _xsi = _xsc();
        static IEnumerator<uint> _xsc() { uint x = 123456789, y = 362436069, z = 521288629, w = (uint)(DateTime.Now.Ticks & 0xffffffff); while (true) { var t = x ^ (x << 11); x = y; y = z; z = w; w = (w ^ (w >> 19)) ^ (t ^ (t >> 8)); yield return w; } }
        static bool Chmax<T>(this ref T lhs, T rhs) where T : struct, IComparable<T> { if (lhs.CompareTo(rhs) < 0) { lhs = rhs; return true; } return false; }
        static bool Chmin<T>(this ref T lhs, T rhs) where T : struct, IComparable<T> { if (lhs.CompareTo(rhs) > 0) { lhs = rhs; return true; } return false; }
        static void Fill<T>(this T[] array, T value) => array.AsSpan().Fill(value);
        static void Fill<T>(this T[,] array, T value) => MemoryMarshal.CreateSpan(ref array[0, 0], array.Length).Fill(value);
        static void Fill<T>(this T[,,] array, T value) => MemoryMarshal.CreateSpan(ref array[0, 0, 0], array.Length).Fill(value);
        static void Fill<T>(this T[,,,] array, T value) => MemoryMarshal.CreateSpan(ref array[0, 0, 0, 0], array.Length).Fill(value);
    }
}
namespace Library {
    class LIB_FastIO
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LIB_FastIO() { str = Console.OpenStandardInput(); }
        readonly Stream str;
        readonly byte[] buf = new byte[2048];
        int len, ptr;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        byte read()
        {
            if (ptr >= len)
            {
                ptr = 0;
                if ((len = str.Read(buf, 0, 2048)) <= 0)
                {
                    return 0;
                }
            }
            return buf[ptr++];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        char Char()
        {
            byte b = 0;
            do b = read();
            while (b < 33 || 126 < b);
            return (char)b;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        virtual public string Scan()
        {
            var sb = new StringBuilder();
            for (var b = Char(); b >= 33 && b <= 126; b = (char)read())
                sb.Append(b);
            return sb.ToString();
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        virtual public long Long()
        {
            long ret = 0; byte b = 0; var ng = false;
            do b = read();
            while (b != '-' && (b < '0' || '9' < b));
            if (b == '-') { ng = true; b = read(); }
            for (; true; b = read())
            {
                if (b < '0' || '9' < b)
                    return ng ? -ret : ret;
                else ret = (ret << 3) + (ret << 1) + b - '0';
            }
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        virtual public double Double() { return double.Parse(Scan(), CultureInfo.InvariantCulture); }
    }
    class LIB_FastIODebug : LIB_FastIO
    {
        Queue<string> param = new Queue<string>();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        string NextString() { if (param.Count == 0) foreach (var item in Console.ReadLine().Split(' ')) param.Enqueue(item); return param.Dequeue(); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LIB_FastIODebug() { }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override string Scan() => NextString();
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override long Long() => long.Parse(NextString());
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override double Double() => double.Parse(NextString());
    }
    class LIB_PriorityQueue
    {
        long[] heap;
        int[] dat;
        public (long Key, int Value) Peek => (heap[0], dat[0]);
        public long Count
        {
            get;
            private set;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public LIB_PriorityQueue()
        {
            heap = new long[8];
            dat = new int[8];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(long key) => Push(key, 0);
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Push(long key, int val)
        {
            if (Count == heap.Length) Expand();
            var i = (int)Count++;
            ref long heapref = ref heap[0];
            ref int datref = ref dat[0];
            Unsafe.Add<long>(ref heapref, i) = key;
            Unsafe.Add<int>(ref datref, i) = val;
            while (i > 0)
            {
                var ni = (i - 1) / 2;
                var heapni = Unsafe.Add<long>(ref heapref, ni);
                if (key >= heapni) break;
                Unsafe.Add<long>(ref heapref, i) = heapni;
                Unsafe.Add<int>(ref datref, i) = Unsafe.Add<int>(ref datref, ni);
                i = ni;
            }
            Unsafe.Add<long>(ref heapref, i) = key;
            Unsafe.Add<int>(ref datref, i) = val;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (long Key, int Value) Pop()
        {
            ref long heapref = ref heap[0];
            ref int datref = ref dat[0];
            var ret = (heapref, datref);
            var cnt = (int)(--Count);
            var key = Unsafe.Add<long>(ref heapref, cnt);
            var val = Unsafe.Add<int>(ref datref, cnt);
            if (cnt == 0) return ret;
            var i = 0; while ((i << 1) + 1 < cnt)
            {
                var i1 = (i << 1) + 1;
				var i2 = (i << 1) + 2;
				if (i2 < cnt && Unsafe.Add<long>(ref heapref, i1) > Unsafe.Add<long>(ref heapref, i2)) i1 = i2;
				var heapi1 = Unsafe.Add<long>(ref heapref, i1);
				if (key <= heapi1) break;
				Unsafe.Add<long>(ref heapref, i) = heapi1;
				Unsafe.Add<int>(ref datref, i) = Unsafe.Add<int>(ref datref, i1);
				i = i1;
			}
			Unsafe.Add<long>(ref heapref, i) = key;
			Unsafe.Add<int>(ref datref, i) = val;
			return ret;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void Expand()
		{
			var len = heap.Length;
			var tmp = new long[len << 1];
			var tmp2 = new int[len << 1];
			Unsafe.CopyBlock(ref Unsafe.As<long, byte>(ref tmp[0]), ref Unsafe.As<long, byte>(ref heap[0]), (uint)(8 * len));
			Unsafe.CopyBlock(ref Unsafe.As<int, byte>(ref tmp2[0]), ref Unsafe.As<int, byte>(ref dat[0]), (uint)(4 * len));
			heap = tmp;
			dat = tmp2;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public (long Key, int Value)[] NoSortList()
		{
			var ret = new (long Key, int Value)[Count];
			ref long heapref = ref heap[0];
			ref int datref = ref dat[0];
			for (var i = 0; i < Count; ++i)
			{
				ret[i] = (Unsafe.Add<long>(ref heapref, i), Unsafe.Add<int>(ref datref, i));
			}
			return ret;
		}
	}
	class LIB_PriorityQueue<T>
	{
		T[] heap;
		Comparison<T> comp;
		public T Peek => heap[0];
		public long Count
		{
			get;
			private set;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(long cap, Comparison<T> cmp, bool asc = true)
		{
			heap = new T[cap];
			comp = asc ? cmp : (x, y) => cmp(y, x);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(Comparison<T> cmp, bool asc = true)
		{
			heap = new T[8];
			comp = asc ? cmp : (x, y) => cmp(y, x);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(long cap, bool asc = true) : this(cap, Comparer<T>.Default.Compare, asc) { }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(bool asc = true) : this(Comparer<T>.Default.Compare, asc) { }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Push(T val)
		{
			if (Count == heap.Length) Expand();
			var i = Count++;
			heap[i] = val;
			while (i > 0)
			{
				var ni = (i - 1) / 2;
				if (comp(val, heap[ni]) >= 0) break;
				heap[i] = heap[ni];
				i = ni;
			}
			heap[i] = val;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Pop()
		{
			var ret = heap[0];
			var val = heap[--Count];
			if (Count == 0) return ret;
			var i = 0; while ((i << 1) + 1 < Count)
			{
				var i1 = (i << 1) + 1;
				var i2 = (i << 1) + 2;
				if (i2 < Count && comp(heap[i1], heap[i2]) > 0) i1 = i2;
				if (comp(val, heap[i1]) <= 0) break;
				heap[i] = heap[i1]; i = i1;
			}
			heap[i] = val;
			return ret;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		void Expand()
		{
			var tmp = new T[Count << 1];
			for (var i = 0; i < heap.Length; ++i) tmp[i] = heap[i];
			heap = tmp;
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T[] NoSortList()
		{
			var ret = new List<T>();
			for (var i = 0; i < Count; ++i)
			{
				ret.Add(heap[i]);
			}
			return ret.ToArray();
		}
	}
	class LIB_PriorityQueue<TK, TV>
	{
		LIB_PriorityQueue<KeyValuePair<TK, TV>> q;
		public KeyValuePair<TK, TV> Peek => q.Peek;
		public long Count => q.Count;
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(long cap, Comparison<TK> cmp, bool asc = true)
		{
			q = new LIB_PriorityQueue<KeyValuePair<TK, TV>>(cap, (x, y) => cmp(x.Key, y.Key), asc);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(Comparison<TK> cmp, bool asc = true)
		{
			q = new LIB_PriorityQueue<KeyValuePair<TK, TV>>((x, y) => cmp(x.Key, y.Key), asc);
		}
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(long cap, bool asc = true) : this(cap, Comparer<TK>.Default.Compare, asc) { }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public LIB_PriorityQueue(bool asc = true) : this(Comparer<TK>.Default.Compare, asc) { }
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Push(TK k, TV v) => q.Push(new KeyValuePair<TK, TV>(k, v));
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public KeyValuePair<TK, TV> Pop() => q.Pop();
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public KeyValuePair<TK, TV>[] NoSortList() => q.NoSortList();
	}
}
