using System;
using System.Collections.Generic;
using System.Text;

namespace RedBlueBall
{
    public class RandomBalls
    {
        public static Lottery GenerateLottery()
        {
            Lottery lotte = new Lottery();
            int guidSeed = Guid.NewGuid().GetHashCode();
            Random random = new Random(guidSeed);
            lotte.BlueBall = random.Next(1, 17);
            while(lotte.RedBalls.Count < 6)
            {
                int redBall = random.Next(1, 34);
                while (lotte.RedBalls.Contains(redBall))
                {
                    redBall = random.Next(1, 34);
                }
                lotte.RedBalls.Add(redBall);
            }
            lotte.RedBalls.Sort();
            return lotte;
        }
    }

    public class Lottery
    {
        public Lottery() { RedBalls = new List<int>(); }
        public List<int> RedBalls { get; set; }

        public int BlueBall { get; set; }
    }
}
