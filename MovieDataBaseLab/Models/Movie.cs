using System;
using System.Collections.Generic;

namespace MovieDataBaseLab.Models
{
    public partial class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public double? Runtime { get; set; }

        public override string ToString()
        {
            return string.Format("{0,-30}{1,-20}{2,-20}", $"Title: {Title}", $"Genre: {Genre}", $"Runtime: {Runtime} minutes");
        }
    }
}
