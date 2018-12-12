using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    public class Day01x02
    {
        [SetUp]
        public void Setup()
        {
        }

        public int FindFirstRepeat(List<int> input)
        {
            var seen = new HashSet<int> { 0 };

            var current = 0;
            var index = 0;
            var exit = false;
            var firstRepeat = int.MinValue;
            while (!exit)
            {
                var value = input[index % input.Count];
                current += value;

                if (seen.Contains(current))
                {
                    firstRepeat = current;
                    exit = true;
                }
                else
                {
                    seen.Add(current);
                    index++;
                }
            }

            if (firstRepeat == int.MinValue)
                throw new ApplicationException();
            else
                return firstRepeat;
        }


        [Test]
        public void First()
        {
            var input = new List<int> { +1, -2, +3, +1};
            
            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(2, firstRepeat);
        }

        [Test]
        public void Second()
        {
            var input = new List<int> { +1, -1 };

            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(0, firstRepeat);
        }

        [Test]
        public void Third()
        {
            var input = new List<int> { +3, +3, +4, -2, -4 };

            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(10, firstRepeat);
        }

        [Test]
        public void Forth()
        {
            var input = new List<int> { -6, +3, +8, +5, -6 };

            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(5, firstRepeat);
        }

        [Test]
        public void Fifth()
        {
            var input = new List<int> { +7, +7, -2, -7, -4 };

            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(14, firstRepeat);
        }

        [Test]
        public void Actual()
        {
            var input = new List<int>();
            using(var file = new System.IO.StreamReader(@"Day01.txt"))
            {  
                string line;
                while((line = file.ReadLine()) != null)  
                {
                    input.Add(int.Parse(line));
                }
                file.Close();
            } 

            

            var firstRepeat = FindFirstRepeat(input);

            Assert.AreEqual(488, firstRepeat);
        }
    }
}