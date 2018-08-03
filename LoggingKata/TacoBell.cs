using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    public class TacoBell : ITrackable
    {
        //string Name { get; set; }
        public string  Name { get; set; }

        //Point Location { get; set; }
        public Point Location { get; set; }

        public void SetName(string name)
        {
            Name = name;
        }
        public void SetLocation(Point point)
        {
            Location = point;
        }

        public string GetName()
        {
            return Name;
        }
    }
}
