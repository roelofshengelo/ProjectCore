using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Assets.Data.Models
{
    public class Planet : Orbital
    {
        /// <summary>
        /// Generate a random planet with a set number of moons
        /// The planet will have a random OrbitRadius and calculated TimeToOrbit by it's distance
        /// </summary>
        /// <param name="maxMoons">How many moons do you want?</param>
        public void Generate(int maxMoons)
        {
            Type = OrbitalType.Planet;
            // Randomize our values
            //GenerateOrbitalDistance();

            GenerateMoons(maxMoons);
        }

        public void GenerateRandomMoons(int maxMoons)
        {
            Type = OrbitalType.Planet;
            // Randomize our values
            //GenerateOrbitalDistance();

            if (maxMoons <= 0)
            {
                return;
            }

            var numberOfMoons = Random.Range(0, maxMoons);

            for (var i = 0; i <= numberOfMoons; i++)
            {
                GenerateMoons(numberOfMoons);
            }
        }

        public void AddMoon(Orbital moon)
        {
            if (moon.Type.Equals(OrbitalType.Moon))
            {
                AddOrbital(moon);
            }
        }

        public void AddMoon(List<Orbital> moons)
        {
            if (moons == null || moons.Count < 1) return;
            for (var i = 0; i <= moons.Count - 1; i++)
            {
                AddMoon(moons[i]);
            }
        }

        private void GenerateMoons(int maxMoons)
        {
            if (maxMoons <= 0)
            {
                return;
            }

            for (var i = 0; i <= maxMoons; i++)
            {
                var moon = new Orbital(OrbitalType.Moon);
                AddOrbital(moon);
                moon.OrbitRadius = this.OrbitRadius / 10; //1000000000000; // FIXME: This makes no sense;
                moon.OrbitalPeriod = moon.OrbitTimeForDistance() / 10; // TODO: Fix with real physics!
            }
        }

        private const int multiplierMass = (10 ^ 24);
        private const uint multiplierDistance = (10 ^ 6);

        public Orbital Mercury()
        {
            return new Orbital(OrbitalType.Planet)
            {
                Mass = (float)0.330 * multiplierMass,
                Diameter = 4879,
                Density = 5427,
                Gravity = 3.7,
                RotationPeriod = 1408,
                OrbitRadius = (long)57.9 * multiplierDistance,
                OrbitalPeriod = 88,
                OrbitalInitAngle = 67
            };

        }



        public Orbital Venus()
        {
            return new Orbital(OrbitalType.Planet)
            {
                Mass = (float)4.87 * multiplierMass,
                Diameter = 12104,
                Density = 5243,
                Gravity = 8.9,
                RotationPeriod = -5832,
                OrbitRadius = (long)108.2 * multiplierDistance,
                OrbitalPeriod = 224,
                OrbitalInitAngle = 30
            };
        }

        public Orbital Earth()
        {
            var earth = new Orbital(OrbitalType.Planet)
            {
                Mass = (float)5.97 * multiplierMass,
                Diameter = 12756,
                Density = 5514,
                Gravity = 9.8,
                RotationPeriod = 24,
                OrbitRadius = (long)149.6 * multiplierDistance,
                OrbitalPeriod = 365,
                OrbitalInitAngle = 338
            };
            var moon = new Orbital(OrbitalType.Moon)
            {
                Mass = (float)0.073 * multiplierMass,
                Diameter = 3475,
                Density = 3340,
                Gravity = 1.6,
                RotationPeriod = 708,
                OrbitRadius = (long)10.9 * multiplierDistance,
                OrbitalPeriod = 27,
                OrbitalInitAngle = 0
            };
            earth.AddOrbital(moon);
            return earth;
        }

        public Orbital Mars()
        {
            var mars = new Orbital(OrbitalType.Planet)
            {
                Mass = (float)0.642 * multiplierMass,
                Diameter = 6792,
                Density = 3933,
                Gravity = 3.7,
                RotationPeriod = 10,
                OrbitRadius = (long)227.9 * multiplierDistance,
                OrbitalPeriod = 687,
                OrbitalInitAngle = 300
            };
            // Todo Add moons
            return mars;
        }

        public Orbital Jupiter()
        {
            var jupiter = new Orbital(OrbitalType.Planet)
            {
                Mass = 1898 * multiplierMass,
                Diameter = 142984,
                Density = 1326,
                Gravity = 23.1,
                RotationPeriod = 10,
                OrbitRadius = (long)778.6 * multiplierDistance,
                OrbitalPeriod = 4331,
                OrbitalInitAngle = 260
            };
            // Todo Add moons
            return jupiter;
        }

        public Orbital Saturn()
        {
            var saturn = new Orbital(OrbitalType.Planet)
            {
                Mass = 568 * multiplierMass,
                Diameter = 120536,
                Density = 687,
                Gravity = 9,
                RotationPeriod = 10,
                OrbitRadius = (long)1433.5 * multiplierDistance,
                OrbitalPeriod = 10747,
                OrbitalInitAngle = 135
            };
            // Todo Add moons
            return saturn;
        }

        public Orbital Uranus()
        {
            var uranus = new Orbital(OrbitalType.Planet)
            {
                Mass = (float)86.8 * multiplierMass,
                Diameter = 51118,
                Density = 1271,
                Gravity = 8.7,
                RotationPeriod = -17,
                OrbitRadius = (long)2872.5 * multiplierDistance,
                OrbitalPeriod = 30589,
                OrbitalInitAngle = 120
            };
            // Todo Add moons
            return uranus;
        }
        public Orbital Neptune()
        {
            var neptune = new Orbital(OrbitalType.Planet)
            {
                Mass = 102 * multiplierMass,
                Diameter = 49528,
                Density = 1638,
                Gravity = 11.0,
                RotationPeriod = 16,
                OrbitRadius = (long)4495.1 * multiplierDistance,
                OrbitalPeriod = 59800,
                OrbitalInitAngle = 100
            };
            // Todo Add moons
            return neptune;
        }
        public Orbital Pluto()
        {
            var pluto = new Orbital(OrbitalType.Planet)
            {
                Mass = (float)0.0146 * multiplierMass,
                Diameter = 2370,
                Density = 2095,
                Gravity = 0.7,
                RotationPeriod = -153,
                OrbitRadius = (long)5906.4 * multiplierDistance,
                OrbitalPeriod = 90560,
                OrbitalInitAngle = 50
            };
            // Todo Add moons
            return pluto;
        }

        //public void MakeEarth()
        //{
        //    OrbitalInitAngle = 0; // "North" of the sun
        //    OrbitRadius = 150000000000; // 150 million KM
        //    TimeToOrbit = 365 * 24 * 60 * 60;
        //}

    }
}