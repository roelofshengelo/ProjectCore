using UnityEngine;

namespace Assets.Data.Models
{
    public class Planet : Orbital
    {
        public void Generate(int maxMoons)
        {
            // Randomize our values
            //OrbitalDistance = (ulong)(Random.Range(5, 1000) * 1000000); // 5-1000  million kilometers
            OrbitalDistance = (ulong)(Random.Range(5, 1000) * 10000000000); // 5-1000  million kilometers
            TimeToOrbit = OrbitTimeForDistance(); // TODO: Fix with real physics!
            GraphicID = Random.Range(1, 12); //TODO Create PlanetGenerator
            Type = OrbitalType.Planet;

            var numberOfMoons = Random.Range(0, maxMoons + 1);
            for (var i = 0; i < numberOfMoons; i++)
            {
                var moon = new Orbital(OrbitalType.Moon);
                AddChild(moon);
                moon.OrbitalDistance = 1000000000000; // FIXME: This makes no sense;
                moon.TimeToOrbit = moon.OrbitTimeForDistance() / 10; // TODO: Fix with real physics!

                // Be sure to set a GraphicID, else or your moons are suns!
                moon.GraphicID = 16; //TODO Create MoonGenerator //Random.Range(1, 12); 
            }
        }

        //public void MakeEarth()
        //{
        //    OffsetAngle = 0; // "North" of the sun
        //    OrbitalDistance = 150000000000; // 150 million KM
        //    TimeToOrbit = 365 * 24 * 60 * 60;
        //}

    }
}