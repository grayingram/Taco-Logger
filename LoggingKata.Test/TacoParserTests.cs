using System;
using Xunit;
using LoggingKata;
namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldDoSomething()
        {
            TacoParser tacoParser = new TacoParser();

            ITrackable result = tacoParser.Parse("");
            Assert.Equal(null, result);
            // TODO: Complete Something, if anything
        }

        [Theory]
        [InlineData(14.32, 12.89, "Herbert Hills", "14.32,12.89,Herbert Hills")]
        
        public void ShouldParse(double latitude, double longitude, string name, string str)
        {
            TacoParser tacoParser = new TacoParser();
            Point myPoint = new Point();
            myPoint.Latitude = latitude;
            myPoint.Longitude = longitude;
            TacoBell tacoBell = new TacoBell();
            tacoBell.SetLocation(myPoint);
            tacoBell.SetName(name);
            
            ITrackable result = tacoParser.Parse(str);
            Assert.Equal(tacoBell.Location.Latitude, result.Location.Latitude);
            Assert.Equal(tacoBell.Location.Longitude, result.Location.Longitude);
            Assert.Equal(tacoBell.Name, result.Name);
            // TODO: Complete Should Parse
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void ShouldFailParse(string str)
        {
            TacoParser tacoParser = new TacoParser();

            ITrackable result = tacoParser.Parse(str);

            Assert.Equal(null, result);
        }
    }
}
