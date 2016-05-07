using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class PlayerStatistics
    {
        public string name { get; set; }
        public int totalPlayed { get; set; }
        public int totalWon { get; set; }
        public int minsPlayed { get; set; }

        public PlayerStatistics(string name) {
            this.name = name;
            totalPlayed = 0;
            totalWon = 0;
            minsPlayed = 9000;
        }

        public override string ToString()
        {
            if (totalWon == 0)
                return this.name + "'s stats: won " + this.totalWon + " matches out of " + this.totalPlayed + " games, no best time.";
            else
                return this.name + "'s stats: won " + this.totalWon + " matches out of " + this.totalPlayed + " with a best time of " + this.minsPlayed + " seconds.";
        }

    }
}
