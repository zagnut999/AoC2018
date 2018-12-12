using NUnit.Framework;
using System.Collections.Generic;

namespace AdventOfCode2018
{
    public class Day01x01
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void First()
        {
            var input = new [] { +1, -2, +3, +1};

            var current = 0;
            foreach (var value in input)
            {
                current += value;
            }

            Assert.AreEqual(3, current);
            //Assert.Pass();
        }

        [Test]
        public void Second()
        {
            var input = new [] { +1, +1, +1};

            var current = 0;
            foreach (var value in input)
            {
                current += value;
            }

            Assert.AreEqual(3, current);
        }

        [Test]
        public void Third()
        {
            var input = new [] { +1, +1, -2};

            var current = 0;
            foreach (var value in input)
            {
                current += value;
            }

            Assert.AreEqual(0, current);
        }

        [Test]
        public void Forth()
        {
            var input = new [] { -1, -2, -3};

            var current = 0;
            foreach (var value in input)
            {
                current += value;
            }

            Assert.AreEqual(-6, current);
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

            

            var current = 0;
            foreach (var value in input)
            {
                current += value;
            }

            Assert.AreEqual(582, current);
        }
    }
}