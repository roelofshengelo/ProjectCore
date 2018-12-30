namespace Assets.Data.Models
{
    public class Star : Orbital
    {
        public void Generate()
        {
            // Make a single star with a single planet orbiting
            var myStar = new Orbital {GraphicID = 1};
            // SR TODO: change to correct ID

            AddChild(myStar);

            //Orbital planet = new Orbital();
            //planet.MakeEarth();
            //planet.GraphicID = 2;
            //myStar.AddChild(planet);
        }
    }
}