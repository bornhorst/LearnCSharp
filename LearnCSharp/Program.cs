using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace LearnCSharp
{
    
    class Program
    {
        // sqrt function
        static double TakeSquareRoot(int x)
        {
            return Math.Sqrt(x);
        }
        // dictionary for counting words in text
        static Dictionary<string, int> CountWords(string text)
        {
            // dictionary <key, value>
            Dictionary<string, int> frequencies;
            frequencies = new Dictionary<string, int>();
            // split text into words
            string[] words = Regex.Split(text, @"\W+");
            // if word and key match, then increment value for word count
            foreach(string word in words)
            {
                if (frequencies.ContainsKey(word))
                    frequencies[word]++;
                else
                    frequencies[word] = 1;
            }
            return frequencies;
        }
        // use nullable types to try parsing a string
        static int? TryParse(string text)
        {
            int ret;
            // check if the output matches the return type
            if (int.TryParse(text, out ret))
                return ret;
            else
                return null;
        }
        // covariance of delegate return type
        delegate Stream StreamFactory();
        static MemoryStream GenerateSampleData()
        {
            byte[] buffer = new byte[16];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = (byte)i;
            return new MemoryStream(buffer);
        }
        
        static void Main(string[] args)
        {
            // create 2 people, Jon and Tom
            Person jon = new Person("Alan Turing",
                                    new DateTime(1912, 6, 23),
                                    new DateTime(1954, 6, 7));
            Person tom = new Person("Donald Knuth",
                                    new DateTime(1938, 1, 10),
                                    null);
            // create StringProcessor delegate instances
            StringProcessor jonsVoice, tomsVoice, background;
            jonsVoice = new StringProcessor(jon.Say);
            tomsVoice = new StringProcessor(tom.Say);
            background = new StringProcessor(Background.Note);
            // invoke StringProcessor delegates
            jonsVoice("Hello, son.");
            tomsVoice("Hello, dad");
            background("An airplane flies past");

            // create list of integers
            List<int> integers = new List<int>();
            // add items into the list
            integers.Add(1);
            integers.Add(2);
            integers.Add(3);
            integers.Add(4);
            // create a converter delegate
            Converter<int, double> converter = TakeSquareRoot;
            // create new list of doubles
            List<double> doubles;
            // convert ints to doubles using generic method
            doubles = integers.ConvertAll<double>(converter);
            foreach(double d in doubles)
            {
                Console.WriteLine(d);
            }

            // use a Dictionary to count words
            string text = @"Do you like green eggs and ham?
                            I do not like them, Sam-I-am.
                            I do not like green eggs and ham.";
            Dictionary<string, int> frequencies = CountWords(text);
            foreach(KeyValuePair<string, int> entry in frequencies)
            {
                string word = entry.Key;
                int frequency = entry.Value;
                Console.WriteLine("{0}: {1}", word, frequency);
            }

            // check nullable type value for age TimeSpan
            Console.WriteLine("Years:{0}", jon.Age.Days/365);
            Console.WriteLine("Years:{0}", tom.Age.Days/365);

            // if possible, parse a string into ints using nullables
            int? parsed = TryParse("Not Valid");
            if (parsed != null)
                Console.WriteLine("Parsed to {0}", parsed.Value);
            else
                Console.WriteLine("Couldn't parse");

            // generate sample data using a stream factory delegate
            StreamFactory factory = GenerateSampleData;
            using(Stream stream = factory())
            {
                int data;
                while ((data = stream.ReadByte()) != -1)
                    Console.WriteLine(data);
            }

            // using anonymous method to create Action<string>
            Action<string> printReverse = delegate (string txt)
            {
                char[] chars = txt.ToCharArray();
                Array.Reverse(chars);
                Console.WriteLine(new string(chars));
            };
            // another anonymous method using Predicate<>, for returning bool
            Predicate<int> printRoot = delegate (int number)
            {
                double sqrt = Math.Sqrt(number);
                Console.WriteLine(sqrt);
                return sqrt > 0;
            };
            // a loop within an anonymous method
            Action<IList<double>> printMean = delegate (IList<double> numbers)
            {
                double total = 0;
                foreach (double value in numbers)
                    total += value;
                Console.WriteLine(total / numbers.Count);
            };
            // invoke action delegates
            printReverse("Hello world");
            Console.WriteLine(printRoot(2));
            printMean(new double[] { 1.5, 2.5, 3, 4.5 });

        }
    }
}
