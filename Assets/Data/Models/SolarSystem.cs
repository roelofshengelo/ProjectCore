﻿namespace Assets.Data.Models
{
    public class SolarSystem : Orbital
    {
        public void Generate()
        {
            // Make a single star with a single planet orbiting

            var myStar = new Orbital {GraphicID = 0};
            AddChild(myStar);

            for (var i = 0; i < 8; i++)
            {
                var planet = new Planet();
                planet.Generate(3);
                myStar.AddChild(planet);
            }
        }
    }
}