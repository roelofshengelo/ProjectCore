namespace Assets.Data.Models
{
    public class Star : Orbital
    {

        public void Generate()
        {
            // Make a single star with a single planet obiting
            Orbital myStar = new Orbital();
            // SR TODO: change to correct ID
            myStar.GraphicID = 1;

            this.AddChild(myStar);

            //Orbital planet = new Orbital();
            //planet.MakeEarth();
            //planet.GraphicID = 2;
            //myStar.AddChild(planet);

        }
    }
}
