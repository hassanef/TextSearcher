using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.BruteForce
{
    public class TextSegment2(string Text, int Left, int Right, int Top, int Bottom)
    {
        public string Text { get; } = Text;
        public int Left { get; } = Left;
        public int Right { get; } = Right;
        public int Top { get; } = Top;
        public int Bottom { get; } = Bottom;
    }

}
