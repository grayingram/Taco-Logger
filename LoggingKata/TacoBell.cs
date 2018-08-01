using System;
using System.Collections.Generic;
using System.Text;

namespace LoggingKata
{
    public class TacoBell : ITrackable
    {
        //string Name { get; set; }
        string ITrackable.Name { get; set; }

        //Point Location { get; set; }
        Point ITrackable.Location { get; set; }

        public void SetName(string name)
        {
            
        }
        public void SetPoint(Point point)
        {

        }
    }
}
