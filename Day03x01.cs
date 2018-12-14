using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2018
{
    public class Day03x01
    {
        [SetUp]
        public void Setup()
        {
        }

        class Claim {
            public int Id { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }

            public Claim (string input)
            {
                //#123 @ 3,2: 5x4
                var match = Regex.Match(input, @"^#(\d+) @ (\d+),(\d+): (\d+)x(\d+)$");
                if (!match.Success)
                    throw new ArgumentException();
                
                Id = int.Parse(match.Groups[1].Value);
                X = int.Parse(match.Groups[2].Value);
                Y = int.Parse(match.Groups[3].Value);
                Width = int.Parse(match.Groups[4].Value);
                Height = int.Parse(match.Groups[5].Value);
            }

            public bool IsCollision(Claim other)
            {
                return this.X < other.X + other.Width &&
                    this.X + this.Width > other.X &&
                    this.Y < other.Y + other.Height &&
                    this.Y + this.Height > other.Y;
            }

            public List<Point> Overlap(Claim other)
            {
                if (this == other || !this.IsCollision(other))
                    return new List<Point>();
                
                var result = new List<Point>();
                for (var i = X; i < X+Width; i++)
                {
                    for(var j = Y; j < Y+Height; j++)
                    {
                        if (i >= other.X && i < other.X+ other.Width &&
                            j >= other.Y && j < other.Y +other.Height)
                            result.Add(new Point(i,j));
                    }

                }                
                return result;
            }
        }

        class Point
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Point() {}
            public Point(int x, int y) {X = x; Y = y;}

            public override bool Equals(object obj)
            {
                var other = obj as Point;
                if (other == null)
                    return false;
                
                return this.X == other.X && this.Y == other.Y;
            }

            public override int GetHashCode() => X ^ Y;
        }
        
        [Test]
        public void First()
        {
            var input = "#123 @ 3,2: 5x4";

            var claim = new Claim(input);
            
            Assert.AreEqual(123, claim.Id);
            Assert.AreEqual(3, claim.X);
            Assert.AreEqual(2, claim.Y);
            Assert.AreEqual(5, claim.Width);
            Assert.AreEqual(4, claim.Height);
        }

        [Test]
        public void First2()
        {
            var input = "#1 @ 912,277: 27x20";
            var claim = new Claim(input);
            
            Assert.AreEqual(1, claim.Id);
            Assert.AreEqual(912, claim.X);
            Assert.AreEqual(277, claim.Y);
            Assert.AreEqual(27, claim.Width);
            Assert.AreEqual(20, claim.Height);
        }

        [Test]
        public void Second()
        {
            var claim1 = new Claim("#1 @ 1,3: 4x4");
            var claim2 = new Claim("#2 @ 3,1: 4x4");
            var claim3 = new Claim("#3 @ 5,5: 2x2");
            
            Assert.IsTrue(claim1.IsCollision(claim2));
            Assert.IsTrue(claim2.IsCollision(claim1));
            Assert.IsFalse(claim1.IsCollision(claim3));
            Assert.IsFalse(claim3.IsCollision(claim1));
            Assert.IsFalse(claim2.IsCollision(claim3));
            Assert.IsFalse(claim3.IsCollision(claim2));
        }

        [Test]
        public void Third()
        {
            var claim1 = new Claim("#1 @ 1,3: 4x4");
            var claim2 = new Claim("#2 @ 3,1: 4x4");
            var claim3 = new Claim("#3 @ 5,5: 2x2");
            
            var overlap = new List<Point>();
            overlap.AddRange(claim1.Overlap(claim2));
            overlap.AddRange(claim1.Overlap(claim3));
            overlap.AddRange(claim2.Overlap(claim3));

            var result = overlap.Distinct();

            Assert.AreEqual(4, result.Count());
            Assert.IsTrue(result.Contains(new Point(3, 3)));
            Assert.IsTrue(result.Contains(new Point(4, 3)));
            Assert.IsTrue(result.Contains(new Point(3, 4)));
            Assert.IsTrue(result.Contains(new Point(4, 4)));
            Assert.IsFalse(result.Contains(new Point(1, 3)));
        }

        [Test]
        public void Forth()
        {
            var claim1 = new Claim("#1 @ 1,3: 4x4");
            
            var overlap = new List<Point>();
            overlap.AddRange(claim1.Overlap(claim1));

            var result = overlap.Distinct();

            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public void Actual()
        {
            var input = new List<string>();
            using(var file = new System.IO.StreamReader(@"Day03.txt"))
            {  
                string line;
                while((line = file.ReadLine()) != null)  
                {
                    input.Add(line);
                }
                file.Close();
            } 
            var claims = input.Select(x=>
            {
                try {
                    return new Claim(x);
                }
                catch (Exception)
                {
                    Console.WriteLine(x);
                    return null;
                }
            }).ToList();
            
            var overlap = new List<Point>();
            foreach (var claim in claims)
            {
                foreach (var claim2 in claims.Where(x=> x != claim))
                {
                    overlap.AddRange(claim.Overlap(claim2));
                }
            }

            var result = overlap.Distinct();

            Assert.AreEqual(110891, result.Count());
        }
    }
}