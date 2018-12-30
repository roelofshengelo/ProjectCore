using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Data.Models
{
    public class Orbital {

        public Orbital()
        {
            TimeToOrbit = 1;
            Children = new List<Orbital>();
            InitAngle = UnityEngine.Random.Range(0, Mathf.PI*2);
        }

        public Orbital Parent;
        public List<Orbital> Children;

        public float InitAngle;
        public float OffsetAngle; // Angle around the parent, in Radians
        // public float OrbitalDistance;    // Distance as AU -- 1 AU is the average distance from Sun to Earth
        // public uint OrbitalDistance; // Maaaaaybe in KMs? As long as nothing is more than 4.4 billion
        // from the sun -- which is the distance of Pluto
        public UInt64 OrbitalDistance;  // In **meters** -- maybe overkill precision? Maybe not.
        // Max value is:  18,446,744,073,709,551,615
        // Pluto is:               4,000,000,000,000

        public UInt64 TimeToOrbit;   // In Seconds?   TODO: Kepler's Third Law

        public int GraphicID;

        // How BIG of a number do we need to represent in our space system?
        // Well, for example, Pluto is about 4 billion (4,000,000,000) kms from
        // the sun/


        // Keep track of the type of orbital, naming purposes?
        public enum OrbitalType
        {
            Star,
            Planet,
            Moon,
            Moonmoon
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

                Vector3 myOffset = new Vector3(offSetX, offSetY, offSetZ);

                if(Parent != null)
                {
                    myOffset += Parent.Position;
                }

                return myOffset;
            }
        }

        public void Update(UInt64 timeSinceStart)
        {
            // Advance our angle by the correct amount of time.
            Debug.Log(string.Format("timeSinceStart: {0}", timeSinceStart));

            OffsetAngle = ((float)timeSinceStart / (float)TimeToOrbit) * 2 * Mathf.PI;
            Debug.Log(string.Format("OffsetAngle: {0}", OffsetAngle));

            // Update all of our children
            for (int i = 0; i < Children.Count; i++)
            {
                Children[i].Update(timeSinceStart);
            }
        }

        public ulong OrbitTimeForDistance()
        {
            // FIXME: Make real math!
            return 365 * 24 * 60 * 60;
        }

        public void MakeEarth()
        {
            OffsetAngle = 0;
            OrbitalDistance = 150000000000;
            TimeToOrbit = 365*24*60*60;
        }    

        public void AddChild(Orbital c)
        {
            c.Parent = this;
            Children.Add(c);
        }

        public void RemoveChild(Orbital c)
        {
            c.Parent = null;
            Children.Remove(c);
        }
    }
}
