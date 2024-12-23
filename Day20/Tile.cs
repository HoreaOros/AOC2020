using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    internal class Tile
    {
        public int Id { get; set; }
        public char[,] OrigImage { get; set; }
        public HashSet<int> SideNums;
        public int MatchingSides = 0;
    }
}
