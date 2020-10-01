using System.Collections.Generic;
using System.Linq;

namespace Prime.Numbers
{
    class Program
    {
        static void Main()
        {
            var prompter = new Prompter();

            int outputCount = 0;
            while (outputCount == 0) outputCount = prompter.AskForInput();

            var values = Primes().Take(outputCount);
            prompter.WriteResult(values);
        }

        static IEnumerable<int> Primes()
        {
            for (var current = 2; ; current++)
            {
                if (IsPrime(current)) yield return current;
            }
        }

        static bool IsPrime(int value)
        {
            if (value <= 1)
            {
                return false;
            }

            for (var x = 2; x < value; x++)
            {
                var canNotBePrime = value % x == 0;
                if (canNotBePrime) return false;
            }

            return true;
        }
    }
}
