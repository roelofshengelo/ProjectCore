using UnityEngine;

namespace Assets.Data.Models
{
    public class Planet : Orbital
    {
        public void Generate(int maxMoons)
        {
            // Randomize our values
            OrbitalDistance = (ulong) Random.Range(5, 1000) * 1000000 * 1000; // 5-1000 million kilometers
            TimeToOrbit = OrbitTimeForDistance(); // TODO: Fix with real physics!
            GraphicID = Random.Range(1, 12); // TODO: Make this not poop

            var numberOfMoons = Random.Range(0, maxMoons + 1);
            for (var i = 0; i < numberOfMoons; i++)
            {
                var moon = new Orbital();
                AddChild(moon);
                moon.OrbitalDistance = 100000000000; // FIXME: This makes no sense;
                moon.TimeToOrbit = moon.OrbitTimeForDistance() / 10; // TODO: Fix with real physics!
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