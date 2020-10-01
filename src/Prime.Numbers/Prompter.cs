using System;
using System.Collections.Generic;

namespace Prime.Numbers
{
    public class Prompter
    {
        public int AskForInput()
        {
            Console.WriteLine("How many numbers do you want to print?");
            var input = Console.ReadLine();
            var canBeParsed = int.TryParse(input, out var inputAsNumber);
            return canBeParsed && inputAsNumber > 0 ? inputAsNumber : 0;
        }

        public void WriteResult(IEnumerable<int> values)
        {
            Console.WriteLine($"The values are: {string.Join(',', values)}");
        }
    }
}