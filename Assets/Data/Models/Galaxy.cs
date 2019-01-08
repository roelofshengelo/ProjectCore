using System.Collections.Generic;
using UnityEngine;

namespace Assets.Data.Models
{
    public class Galaxy
    {
        public List<SolarSystem> SolarSystems;

        public Galaxy()
        {
            // Don't do procedural generation in any kind of constructor
            SolarSystems = new List<SolarSystem>();
        }

        public void Generate(int numStars)
        {
            // We can set a SEED for the random number generator, so that it
            // starts from the same place every time
            Random.InitState(181818);

            for (var i = 0; i < numStars; i++)
            {
                var ss = new SolarSystem();
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

        public void Update(ulong timeSinceStart)
        {
            // TODO: Consider only updating PART of the galaxy if you have a CRAAAAAAZY number of solar system

            foreach (var ss in SolarSystems) ss.Update(timeSinceStart);
        }

        public void Update(float timeSinceStart)
        {
            // TODO: Consider only updating PART of the galaxy if you have a CRAAAAAAZY number of solar system

            foreach (var ss in SolarSystems) ss.Update(timeSinceStart);
        }
    }
}