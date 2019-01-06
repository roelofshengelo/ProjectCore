using System;
using System.Collections.Generic;
using System.Text;
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

        /// <summary>
        /// Starting angle around the parent, in Radians
        /// </summary>
        public float OrbitalInitAngle;

        /// <summary>
        /// Current angle around the parent, in Radians
        /// </summary>
        public float OrbitalOffsetAngle;



        private ulong _radius = 0;
        private ulong _diameter = 0;
        private ulong _circumference = 0;
        /// <summary>
        /// Average distance to object which this orbits in km
        /// </summary>

        public ulong OrbitRadius
        {
            get => _radius;
            set
            {
                _radius = value;
                _diameter = (ulong)(2 * Mathf.PI * _radius);
                _circumference = (ulong)Mathf.PI * _diameter;
            }
        }

        public float OrbitalDiameter => _diameter;

        public float OrbitalCircumference => _circumference;

        /// https://en.wikipedia.org/wiki/Orbital_period
        /// https://nssdc.gsfc.nasa.gov/planetary/factsheet/index.html

        /// <summary>
        /// How many days does it take to make a full orbit
        /// </summary>
        public int OrbitalPeriod;

        /// <summary>
        /// How many hours does this object have in a day
        /// TODO Make this so that different species react on this period, imagine having days of 750 hours, good-luck adjusting
        /// </summary>
        public int RotationPeriod;

        /// <summary>
        /// How massive is this object, in kilograms (kg).
        /// </summary>
        public float Mass;

        public int Diameter;

        //public int Radius => Diameter / 2;

        //public int Volume;

        public int Density;

        public double Gravity;

        public Orbital Parent;
        // Max value is:  18,446,744,073,709,551,615
        // Pluto is:               4,000,000,000,000

        //public ulong TimeToOrbit; // In Seconds?   TODO: Kepler's Third Law

        public Orbital()
        {
            OrbitalPeriod = 365 * 24; // 1 Earth year in hour
            Orbitals = new List<Orbital>();
            OrbitalInitAngle = 0; // Random.Range(0, Mathf.PI * 2);
            OrbitalOffsetAngle = 0;
            Type = OrbitalType.Planet;

            GraphicID = 0; // Type must be set before calling GraphicIDForType
        }

        public Orbital(OrbitalType orbitalType)
        {
            OrbitalPeriod = 365 * 24; // 1 Earth year in hour
            Orbitals = new List<Orbital>();
            OrbitalInitAngle = 0; // Random.Range(0, Mathf.PI * 2);
            OrbitalOffsetAngle = 0;
            Type = orbitalType;

            GraphicID = GraphicIDForType();
        }


        // http://www.stjarnhimlen.se/comp/tutorial.html
        // We need to be able to get an X, Y (and maybe Z) coordinate for our location
        // for the purpose of rendering the Oribtal on screen
        public Vector3 Position(ulong daysPastSinceStart, ulong zoomlevels)
        {



            var circumferencePerDay = this.OrbitalCircumference / this.OrbitalPeriod;
            var circumferenceTotal = circumferencePerDay * daysPastSinceStart;

            //this.OrbitalOffsetAngle = 5;

            //var offSetX = Mathf.Sin(circumferenceTotal) * OrbitRadius;
            //var offSetY = -Mathf.Cos(circumferenceTotal) * OrbitRadius;
            //const int offSetZ = 0; // Z is locked to zero -- but consider adding Inclination if in 3D

            //var myOffset = new Vector3(offSetX, offSetY, offSetZ);
            //if (Parent != null) myOffset += Parent.Position(daysPastSinceStart, zoomlevels);
            //return myOffset;

            //get
            //{
            //// TODO: Convert our orbit info into a vector that we can use
            //// to render something as a Unity GameObject

            //// Consider whether or not we should be saving Vector3 in a 
            //// private variable whenever we update our angle, or if it's
            //// no slower to just calculate on demand like this.
            //var offSetX = Mathf.Sin(OrbitalInitAngle + OrbitalOffsetAngle) * OrbitRadius;
            //var offSetY = -Mathf.Cos(OrbitalInitAngle + OrbitalOffsetAngle) * OrbitRadius;
            //const int offSetZ = 0; // Z is locked to zero -- but consider adding Inclination if in 3D

            //var myOffset = new Vector3(offSetX, offSetY, offSetZ);
            //if (Parent != null) myOffset += Parent.Position(daysPastSinceStart, zoomlevels);
            //return myOffset;


            //var radiansPerDay = 360 / (uint)OrbitalPeriod;
            //var r = OrbitRadius;
            //var t = radiansPerDay * daysPastSinceStart;
            //var h = 0;
            //var k = 0;

            //var offSetX = (r * Mathf.Sin(t) + h) / zoomlevels;
            //var offSetY = (r * Mathf.Cos(t) + k) / zoomlevels;

            //var myOffset = new Vector3(offSetX, offSetY, 0);

            //if (Parent != null) myOffset += Parent.Position(daysPastSinceStart, zoomlevels);

            //if (this.RotationPeriod.Equals(24))
            //{

            //    var str = new StringBuilder();
            //    //str.AppendLine("Earth position: (" + myOffset.x + "," + myOffset.y + ") should move with '" + radiansPerDay + "' radians a day and a OrbitalPeriod of '" + OrbitalPeriod + "'");
            //    //str.AppendLine("daysPastSinceStart: " + daysPastSinceStart);
            //    str.AppendLine("OrbitRadius: " + this.OrbitRadius);
            //    str.AppendLine("Current radians: " + t + " of 360");

            //    Debug.Log(str.ToString());
            //}


            //return myOffset;

            //}
        }


        public void Update(ulong timeSinceStart)
        {
            // Advance our angle by the correct amount of time.
            //Debug.Log(string.Format("timeSinceStart: {0}", timeSinceStart));

            OrbitalOffsetAngle = timeSinceStart / ((float)OrbitalPeriod * 2 * Mathf.PI);
            //Debug.Log("timeSinceStart" + timeSinceStart);
            //Debug.Log("OrbitalOffsetAngle" + OrbitalOffsetAngle);
            // Update all of our children
            for (var i = 0; i < Orbitals.Count; i++)
            {
                Orbitals[i].Update(timeSinceStart);
                if (Orbitals[i].RotationPeriod.Equals(24))
                {
                    //Debug.Log("OrbitalOffsetAngle for Earth: " + Orbitals[i].OrbitalOffsetAngle);
                }
            }
        }

        public int OrbitTimeForDistance()
        {
            // FIXME: Make real math!
            return 365 * 24;
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

        private void OffsetBySomethingSomethingDarkSide()
        {

        }

        //public void MakeEarth()
        //{
        //    OrbitalOffsetAngle = 0;
        //    OrbitRadius = 150000000000;
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