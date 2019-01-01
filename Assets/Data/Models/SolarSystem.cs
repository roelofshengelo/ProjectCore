namespace Assets.Data.Models
{
    public class SolarSystem : Orbital
    {
        public void Generate()
        {
            // Make a single star with a single planet orbiting

            var myStar = new Orbital(OrbitalType.Star);
            AddChild(myStar);

            for (var i = 0; i < 8; i++)
            {
                var planet = new Planet();
                planet.Generate(1); // TODO Fix with more than 1 moon!
                myStar.AddChild(planet);
            }
        }
    }
}