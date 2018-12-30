using System;
using System.Diagnostics;

public class SolarSystem : Orbital
{
    public void Generate()
    {
        // Make a single star with a single planet orbiting

        Orbital myStar = new Orbital();
        myStar.GraphicID = 0;
        this.AddChild(myStar);
        
        for (int i = 0; i < 8; i++)
        {
            Planet planet = new Planet();
            planet.Generate(3);
            myStar.AddChild(planet);
        }
    }



}

