using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;

namespace Assets.Data.Models
{
    public class SolarSystem : Orbital
    {

        public int numberOfPlanets = 8;
        public int numberOfMoonsPerPlanet = 1;
        //public bool randomizePlanets = false;
        public bool randomizeMoons = false;


        public bool Sol = true;
        // Really? We need to add a whole lot more planets if Pluto is a planet, just saying...
        public bool PlutoIsAPlanet = false;

        public void Generate()
        {
            // Manually create Sol
            if (Sol)
            {
                randomizeMoons = false;
                GenerateSol();
                return;
            }

            // Make a single star with a single planet orbiting
            var myStar = new Orbital(OrbitalType.Star);
            AddOrbital(myStar);

            for (var i = 0; i < numberOfPlanets; i++)
            {
                var planet = new Planet();
                if (randomizeMoons) { planet.GenerateRandomMoons(numberOfMoonsPerPlanet); }
                else { planet.Generate(numberOfMoonsPerPlanet); }
                myStar.AddOrbital(planet);
            }
            Debug.WriteLine("This solar system has:");
            Debug.WriteLine(this.Orbitals.Count + " stars");

        }

        private void GenerateSol()
        {
            //https://nssdc.gsfc.nasa.gov/planetary/factsheet/index.html
            var sol = new Orbital(OrbitalType.Star);

            sol.AddOrbital(new Planet().Mercury());
            sol.AddOrbital(new Planet().Venus());
            sol.AddOrbital(new Planet().Earth());
            sol.AddOrbital(new Planet().Mars());
            sol.AddOrbital(new Planet().Jupiter());
            sol.AddOrbital(new Planet().Saturn());
            sol.AddOrbital(new Planet().Uranus());
            sol.AddOrbital(new Planet().Neptune());
            if (PlutoIsAPlanet) sol.AddOrbital(new Planet().Pluto());

            AddOrbital(sol);
        }

    }
}