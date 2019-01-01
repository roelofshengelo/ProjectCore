using UnityEngine;

namespace Assets.Data.Models
{
    public class Planet : Orbital
    {
        /// <summary>
        /// Generate a random planet with a set number of moons
        /// The planet will have a random OrbitalDistance and calculated TimeToOrbit by it's distance
        /// </summary>
        /// <param name="maxMoons">How many moons do you want?</param>
        public void Generate(int maxMoons)
        {
            Type = OrbitalType.Planet;
            // Randomize our values
            GenerateOrbitalDistance();

            GenerateMoons(maxMoons);
        }

        private void GenerateMoons(int maxMoons)
        {
            var numberOfMoons = Random.Range(0, maxMoons + 1);
            for (var i = 0; i < numberOfMoons; i++)
            {
                var moon = new Orbital(OrbitalType.Moon);
                AddChild(moon);
                moon.OrbitalDistance = 1000000000000; // FIXME: This makes no sense;
                moon.TimeToOrbit = moon.OrbitTimeForDistance() / 10; // TODO: Fix with real physics!
            }
        }

        private void GenerateOrbitalDistance()
        {
            //OrbitalDistance = (ulong)(Random.Range(5, 1000) * 1000000); // 5-1000  million kilometers
            //OrbitalDistance = (ulong)(Random.Range(5, 1000) * 10000000000);
            OrbitalDistance = 100000000;
            TimeToOrbit = OrbitTimeForDistance(); // TODO: Fix with real physics!
        }

        //public void MakeEarth()
        //{
        //    OffsetAngle = 0; // "North" of the sun
        //    OrbitalDistance = 150000000000; // 150 million KM
        //    TimeToOrbit = 365 * 24 * 60 * 60;
        //}

    }
}