using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2018
{
    public class Day02x01
    {
        [SetUp]
        public void Setup()
        {
        }

        public class ChecksumPart
        {
            public bool Has2 { get; set; }
            public bool Has3 { get; set; }
        }
        public ChecksumPart CalculateChecksum(string input)
        {
            var result = new ChecksumPart();
            var dict = new Dictionary<char, int>();
            foreach (var letter in input)
            {
                if (dict.ContainsKey(letter))
                    dict[letter]++;
                else
                    dict.Add(letter, 1);
            }

            foreach (var pair in dict)
            {
                if (pair.Value == 2)
                    result.Has2 = true;
                if (pair.Value == 3)
                    result.Has3 = true;
            }

            return result;
        }

        public int CalculateChecksum(List<string> input)
        {
            var subChecks = input.Select(CalculateChecksum).ToList();

            var sumOfTwos = subChecks.Count(x=> x.Has2);
            var sumOfThrees = subChecks.Count(x=> x.Has3);

            return sumOfThrees * sumOfTwos;
        }


        [Test]
        public void First()
        {
            var input = "abcdef";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(false, checksum.Has2);
            Assert.AreEqual(false, checksum.Has3);
        }

        [Test]
        public void Second()
        {
            var input = "bababc";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(true, checksum.Has2);
            Assert.AreEqual(true, checksum.Has3);
        }

        [Test]
        public void Third()
        {
            var input = "abbcde";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(true, checksum.Has2);
            Assert.AreEqual(false, checksum.Has3);
        }

        [Test]
        public void Forth()
        {
            var input = "abcccd";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(false, checksum.Has2);
            Assert.AreEqual(true, checksum.Has3);
        }

        [Test]
        public void Fifth()
        {
            var input = "aabcdd";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(true, checksum.Has2);
            Assert.AreEqual(false, checksum.Has3);
        }

        [Test]
        public void Sixth()
        {
            var input = "abcdee";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(true, checksum.Has2);
            Assert.AreEqual(false, checksum.Has3);
        }

        [Test]
        public void Seventh()
        {
            var input = "ababab";

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(false, checksum.Has2);
            Assert.AreEqual(true, checksum.Has3);
        }

        [Test]
        public void Eigth()
        {
            var input = new List<string>{"abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee","ababab"};

            var checksum = CalculateChecksum(input);

            Assert.AreEqual(12, checksum);
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

            var checkSum = CalculateChecksum(input);

            Assert.AreEqual(7936, checkSum);
        }
    }
}