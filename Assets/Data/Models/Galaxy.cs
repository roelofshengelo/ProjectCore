using UnityEngine;
using System;
using System.Collections.Generic;

public class Galaxy {

    public Galaxy()
    {
        // Don't do procedural generation in any kind of constructor
        SolarSystems = new List<SolarSystem>();
    }

    public List<SolarSystem> SolarSystems;

    public void Generate(int numStars)
    {
        // We can set a SEED for the random number generator, so that it
        // starts from the same place every time
        UnityEngine.Random.InitState( 181818 );

        for (int i = 0; i < numStars; i++)
        {
            SolarSystem ss = new SolarSystem();
            ss.Generate();

            SolarSystems.Add(ss);
        }
    }

    public void LoadFromFile(string fileName)
    {
        // As an example, you may want to do this ISNTEAD
        // of Generate() -- so it's good to decouple from
        // constructor
    }

    public void Update(UInt64 timeSinceStart)
    {
        // TODO: Consider only updating PART of the galaxy if you have a CRAAAAAAZY number of solar system

        foreach(SolarSystem ss in SolarSystems)
        {
            ss.Update(timeSinceStart);
        }
    }
}
