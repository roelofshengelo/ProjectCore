namespace Assets.Data.Models
{
    public class Star : Orbital
    {

        public void Generate()
        {
            Type = OrbitalType.Star;

            // Make a single star with a single planet orbiting
            var myStar = new Orbital(OrbitalType.Star);

            AddChild(myStar);

            var planet = new Orbital(OrbitalType.Planet);
            planet.MakeEarth();
            myStar.AddChild(planet);
        }


    }
}