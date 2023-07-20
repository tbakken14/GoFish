using System.Collections.Generic;

namespace GoFish.Models
{
    public class Hand
    {
        public List<int> Cards { get; set; }

        public Hand()
        {
            Cards = new List<int>();
        }
    }
}