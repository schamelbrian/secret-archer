using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _33DigitCancellingFractions
{
	public class Fraction
	{
		public int n;
		public int d;
		

		public Fraction()
		{
		}
		public Fraction(int D, int N)
		{
			d = D;
			n = N;
		}

		public Fraction(Fraction other)
		{
			d = other.d;
			n = other.n;
		}
		public Fraction simplify()
		{
			Fraction other = new Fraction(this);
			for (int i = 2; i <= other.n; i++)
			{
				if (d % i == 0 && n % i == 0)
				{
					other.d /= i;
					other.n /= i;
				}
			}

			return other;
		}

		public override string ToString()
		{
			return n + "/" + d;
		}

		public static bool operator ==(Fraction a, Fraction b)
		{
			return a.simplify().ToString() == b.simplify().ToString();
		}
		public static bool operator !=(Fraction a, Fraction b)
		{
			return !(a == b);
		}
	}
	class Program
	{

		static Fraction findmatching(Fraction f)
		{
			List<Fraction> r = new List<Fraction>();
			
			string fstr = f.ToString();

			//string fsstr = fs.ToString();

			string n = fstr.Substring(0, 2);
			string d = fstr.Substring(3, 2);

			int toremove = d.IndexOfAny(n.ToCharArray());
			if (toremove == -1)
				return new Fraction(-1, -1);
			n.Remove(toremove);
			d.Remove(toremove);

			Fraction frac = new Fraction(Convert.ToInt32(d), Convert.ToInt32(n));

			if (f == frac)
				return frac.simplify();

			return new Fraction(-1, -1);
		}
		static List<string> findfracs()
		{
			List<string> theList = new List<string>();

			for(int d = 11; d < 100; d++)
				if(d % 10 != 0 && d % 11 != 0)
					for (int n = 11; n < d; n++)
					{
						Fraction f = new Fraction(d, n);
						if (f.simplify().ToString().Length == 3)//num and denom each 1 digit
						{
							Fraction returned = findmatching(f);
							if(returned.n != -1)
								theList.Add(f.ToString() + " " + returned.ToString());
						}
					}
			return theList;
		}
		static void Main(string[] args)
		{

			string filename = "matrix";

			if (filename.Substring(filename.Length - 3) != ".txt")
				filename += ".txt";

			List<string> l = new List<string>();
			foreach(string s in findfracs())
				Console.WriteLine(s);
			Console.ReadKey();
		}





		static int[] parsetoints(string input)
		{
			input += ' ';
			List<int> theints = new List<int>();
			string snum = "";
			int[] ret = theints.ToArray();
			while (input.Length != 0)
			{
				if ((input[0].CompareTo('0') >= 0 && input[0].CompareTo('9') <= 0) || input[0] == '-')
				{
					snum += input[0];
				}
				else
				{
					try
					{
						theints.Add(Convert.ToInt32(snum));
						ret = theints.ToArray();
					}
					catch (FormatException ex)
					{
					}
					snum = "";
				}

				input = input.Substring(1);//processed the first character, remove it
			}
			return ret;

		}
		static int[][] parsegridtoints(string[] inputgrid)
		{
			int[][] ret = new int[inputgrid.Length][];

			for (int i = 0; (int)i < inputgrid.Length; i++)
				ret[i] = parsetoints(inputgrid[i]);

			return ret;
		}

		public static int mymin(int val, List<int> arr)
		{
			val = Math.Min(val, arr[0]);
			if (arr.Count == 1)
				return val;

			arr.RemoveAt(0);

			return mymin(val, arr);
		}
		public static int myMin(params int[] arr)
		{
			int val = Math.Min(Int32.MaxValue, arr[0]);


			List<int> l = new List<int>(arr);
			l.RemoveAt(0);
			return mymin(val, l);
		}
		public static int myMax(int val, List<int> arr)
		{
			val = Math.Max(val, arr[0]);
			if (arr.Count == 1)
				return val;

			arr.RemoveAt(0);

			return myMax(val, arr);
		}
		public static int myMax(params int[] arr)
		{
			int val = Math.Max(Int32.MinValue, arr[0]);


			List<int> l = new List<int>(arr);
			l.RemoveAt(0);
			return myMax(val, l);
		}

	}
}
