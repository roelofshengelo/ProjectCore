using System;
using System.Collections.Generic;
using Assets.Data.Bridge;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Data.Models
{
    public class Orbital
    {
        // How BIG of a number do we need to represent in our space system?
        // Well, for example, Pluto is about 4 billion (4,000,000,000) kms from
        // the sun/

        // Keep track of the type of orbital, naming purposes?
        public enum OrbitalType
        {
            Star,
            Planet,
            Moon,
            Moonmoon // You know, a moon that orbits a moon :)
            //UnSet
        }

        public OrbitalType Type;

        public List<Orbital> Orbitals;

        public TextureDataOrbitals TextureData;

        public int GraphicID;

        public float InitAngle;

        public float OffsetAngle; // Angle around the parent, in Radians

        /// <summary>
        /// Average distance to object which this orbits in km
        /// </summary>
        public ulong OrbitalDistance;

        /// https://en.wikipedia.org/wiki/Orbital_period
        /// https://nssdc.gsfc.nasa.gov/planetary/factsheet/index.html

        /// <summary>
        /// How many days does it take to make a full orbit
        /// </summary>
        public int OrbitalPeriod;

        /// <summary>
        /// How many hours does this object have in a day
        /// </summary>
        public int RotationPeriod;

        /// <summary>
        /// How massive is this object, in kilograms (kg).
        /// </summary>
        public float Mass;

        public int Diameter;

        public int Density;

        public double Gravity;

        public Orbital Parent;
        // Max value is:  18,446,744,073,709,551,615
        // Pluto is:               4,000,000,000,000

        public ulong TimeToOrbit; // In Seconds?   TODO: Kepler's Third Law

        public Orbital()
        {
            TimeToOrbit = 365 * 24 * 60 * 60; // 1 Earth year // 31536000 seconds
            Orbitals = new List<Orbital>();
            InitAngle = 0; // Random.Range(0, Mathf.PI * 2);
            OffsetAngle = 0;
            Type = OrbitalType.Planet;

            GraphicID = 0; // Type must be set before calling GraphicIDForType
        }

        public Orbital(OrbitalType orbitalType)
        {
            TimeToOrbit = 365 * 24 * 60 * 60; // 1 Earth year // 31536000 seconds
            Orbitals = new List<Orbital>();
            InitAngle = 0; // Random.Range(0, Mathf.PI * 2);
            OffsetAngle = 0;
            Type = orbitalType;

            GraphicID = GraphicIDForType();
        }



        // We need to be able to get an X, Y (and maybe Z) coordinate for our location
        // for the purpose of rendering the Oribtal on screen
        public Vector3 Position
        {
            get
            {
                // TODO: Convert our orbit info into a vector that we can use
                // to render something as a Unity GameObject

                // Consider whether or not we should be saving Vector3 in a 
                // private variable whenever we update our angle, or if it's
                // no slower to just calculate on demand like this.
                var offSetX = Mathf.Sin(InitAngle + OffsetAngle) * OrbitalDistance;
                var offSetY = -Mathf.Cos(InitAngle + OffsetAngle) * OrbitalDistance;
                const int offSetZ = 0; // Z is locked to zero -- but consider adding Inclination if in 3D

                var myOffset = new Vector3(offSetX, offSetY, offSetZ);

                if (Parent != null) myOffset += Parent.Position;

                return myOffset;
            }
        }

        public void Update(ulong timeSinceStart)
        {
            // Advance our angle by the correct amount of time.
            //Debug.Log(string.Format("timeSinceStart: {0}", timeSinceStart));

            OffsetAngle = timeSinceStart / ((float)TimeToOrbit * 2 * Mathf.PI);
            //Debug.Log("timeSinceStart" + timeSinceStart);
            //Debug.Log("OffsetAngle" + OffsetAngle);
            // Update all of our children
            for (var i = 0; i < Orbitals.Count; i++)
            {
                Orbitals[i].Update(timeSinceStart);
                if (Orbitals[i].RotationPeriod.Equals(24))
                {
                    Debug.Log("OffsetAngle for Earth: " + Orbitals[i].OffsetAngle);
                }
            }
        }

        public ulong OrbitTimeForDistance()
        {
            // FIXME: Make real math!
            return 365 * 24 * 60 * 60;
        }

        private int GraphicIDForType()
        {
            if (Type == OrbitalType.Star)
                return 0;
            else if (Type == OrbitalType.Planet)
                return Random.Range(0, 2); //TODO Create PlanetGenerator
            else if (Type == OrbitalType.Moon)
                return 0;
            else
                Debug.Log("Type '" + Type + "' isn't set to handle current GraphicIDs");
            throw new IndexOutOfRangeException("Really, no automatic break???");
        }

        //public void MakeEarth()
        //{
        //    OffsetAngle = 0;
        //    OrbitalDistance = 150000000000;
        //    TimeToOrbit = 365 * 24 * 60 * 60;  // 1 year // 31536000 seconds
        //}

        public void AddOrbital(Orbital c)
        {
            c.Parent = this;
            Orbitals.Add(c);
        }

        public void RemoveOrbital(Orbital c)
        {
            c.Parent = null;
            Orbitals.Remove(c);
        }


    }
}