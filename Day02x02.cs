using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02x02
    {
        [SetUp]
        public void Setup()
        {
        }

        public string FindCommonLetters(List<string> input)
        {
            var start = 1;
            var index = -1;
            var result = "";
            foreach (var line in input)
            {
                for (var i = start; i < input.Count(); i++)
                {
                    index = Compare(line, input[i]);

                    if (index != -1)
                    {
                        result = line.Remove(index,1);
                        break;
                    }
                }
                if (index != -1)
                        break;
                start++;
            }

            return result;
        }

        public int Compare(string first, string second)
        {
            var index = -1;
            var count = 0;
            for (var i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    index = i;
                    count++;
                }
            }

            if (count == 1)
                return index;
            else
                return  -1;
        }

        [Test]
        public void First()
        {
            var expected = 1;
            var actual = Compare("ABA", "ACA");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Second()
        {
            var expected = -1;
            var actual = Compare("ABC", "AEF");

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Third()
        {
            var input = new List<string>{"abcde", "fghij", "klmno", "pqrst", "fguij", "axcye","wvxyz"};

            var commonLetters = FindCommonLetters(input);

            Assert.AreEqual("fgij", commonLetters);
        }
        
        [Test]
        public void Actual()
        {
            var input = new List<string>();
            using(var file = new System.IO.StreamReader(@"Day02.txt"))
            {  
                string line;
                while((line = file.ReadLine()) != null)  
                {
                    input.Add(line);
                }
                file.Close();
            } 

            var commonLetters = FindCommonLetters(input);

            Assert.AreEqual("lnfqdscwjyteorambzuchrgpx", commonLetters);
        }
    }
}