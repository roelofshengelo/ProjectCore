using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Assets.Data.Bridge;
using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data.Views
{
    public class SolarSystemView : MonoBehaviour
    {
        private GameController gameController;

        private Dictionary<Orbital, GameObject> orbitalGameObjectMap;
        private SolarSystem solarSystem;

        public Sprite[] Sprites;

        public Sprite[] Stars;
        public Sprite[] Planets;
        public Sprite[] Moons;

        //private int _numOfStars = 1;
        //private int _numOfPlanets = 1;
        //private int _numOfMoons = 1;
        private List<Sprite[]> _sprites;
        private TextureDataOrbitals textureData;


        public Sprite[] DebugSprites;
        public bool UseDebugSprites = false;
        private bool hasDebugSprites = false;

        //public ulong zoomLevels = 1000000; 1x million
        public int zoomLevels = 300;

        // Use this for initialization
        private void Start()
        {
            gameController = FindObjectOfType<GameController>();

            if (DebugSprites != null && DebugSprites.Length > 0) hasDebugSprites = true;
            var solarSystemId = 0;

            //if (Stars.Length > 0 && Planets.Length > 0 && Moons.Length > 0)
            //{
            //    _numOfStars = Stars.Length;
            //    _numOfPlanets = Planets.Length;
            //    _numOfMoons = Moons.Length;

            //    var textureData = new TextureDataOrbitals();
            //    textureData.SolarSystemId = solarSystemId;
            //    textureData.NumberOfStars = _numOfStars;
            //    textureData.NumberOfPlanets = _numOfPlanets;
            //    textureData.NumberOfMoons = _numOfMoons;

            //    var index = 0;
            //    for (var i = 0; i >= Stars.Length; i++)
            //    {
            //        Sprites[index] = Stars[i];
            //        index++;
            //    }
            //    for (var i = 0; i >= Planets.Length; i++)
            //    {
            //        Sprites[index] = Planets[i];
            //        index++;
            //    }
            //    for (var i = 0; i >= Moons.Length; i++)
            //    {
            //        Sprites[index] = Moons[i];
            //        index++;
            //    }
            //}


            ShowSolarSystem(solarSystemId);
        }

        public void ShowSolarSystemZero()
        {
            ShowSolarSystem(0);
        }


        public void ShowSolarSystem(int solarSystemID)
        {
            // First, clean up the solar system by deleting any old graphics.
            while (transform.childCount > 0)
            {
                var c = transform.GetChild(0);
                c.SetParent(null); // Become Batman
                Destroy(c.gameObject);
            }

            orbitalGameObjectMap = new Dictionary<Orbital, GameObject>();

            solarSystem = gameController.Galaxy.SolarSystems[solarSystemID];


            // Only spawn the first sun for now
            //MakeSpritesForOrbital(transform, solarSystem.Orbitals[0]);

            //return;
            // Spawn a graphic for each object in the solar system
            for (var i = 0; i < solarSystem.Orbitals.Count; i++)
            // Spawn a graphic for each orbital of the first sun
            //for (var i = 1; i < solarSystem.Orbitals.Count - 1; i++)
            {
                //Orbital orbital = solarSystem.Orbitals[i];
                MakeSpritesForOrbital(transform, solarSystem.Orbitals[i]);
            }
        }

        private void MakeSpritesForOrbital(Transform transformParent, Orbital orbital)
        {
            var go = new GameObject();
            orbitalGameObjectMap[orbital] = go;
            go.transform.SetParent(transformParent);

            // Set our position.
            go.transform.position = orbital.Position / zoomLevels;
            //go.transform.position = orbital.Position(gameController.DaysPastSinceStart, zoomLevels);

            var sr = go.AddComponent<SpriteRenderer>();
            sr.drawMode = SpriteDrawMode.Sliced;

            if (hasDebugSprites && UseDebugSprites)
            {
                var pointyGameObject = new GameObject();

                pointyGameObject.transform.SetParent(go.transform);
                //pointyGameObject.transform.position = go.transform.position;
                pointyGameObject.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 1);

                //orbitalGameObjectMap[orbital] = pointyGameObject;
                //pointyGameObject.transform.SetParent(go.transform);

                // Set our position.
                //pointyGameObject.transform.position = go.transform.position;

                // Add the thingy to the orbital
                var pointyThingy = pointyGameObject.AddComponent<SpriteRenderer>();
                pointyThingy.drawMode = SpriteDrawMode.Sliced;
                pointyThingy.name = "pointy-thingy";
                pointyThingy.sprite = DebugSprites[0];
            }




            switch (orbital.Type)
            {
                case Orbital.OrbitalType.Star:
                    sr.name = "Star";
                    sr.sprite = Stars[orbital.GraphicID];
                    //sr.size = new Vector2(15, 15);
                    break;
                case Orbital.OrbitalType.Planet:
                    sr.name = "Planet";
                    sr.sprite = Planets[orbital.GraphicID];
                    //sr.size = new Vector2(5, 5);
                    break;
                case Orbital.OrbitalType.Moon:
                    sr.name = "Moon";
                    sr.sprite = Moons[orbital.GraphicID];
                    //sr.size = new Vector2(2, 2);
                    break;
                case Orbital.OrbitalType.Moonmoon:
                    throw new ArgumentOutOfRangeException("Orbital doesn't have a sprite yet");
                default:
                    throw new ArgumentOutOfRangeException("Orbital doesn't have a OrbitalType");
            }

            for (var i = 0; i <= orbital.Orbitals.Count - 1; i++)
            {
                MakeSpritesForOrbital(go.transform, orbital.Orbitals[i]);
            }

        }

        private void UpdateSprites(Orbital orbital)
        {
            if (solarSystem.Orbitals.Count > 0)
            {
                var go = orbitalGameObjectMap[orbital];
                go.transform.position = orbital.Position / zoomLevels;
                //go.transform.position = orbital.Position(gameController.DaysPastSinceStart, zoomLevels);
                //go.name = orbital.OrbitalOffsetAngle.ToString(CultureInfo.InvariantCulture);

                for (var i = 0; i <= orbital.Orbitals.Count - 1; i++)
                {
                    UpdateSprites(orbital.Orbitals[i]);
                }
            }
            else
            {
                Debug.Log("orbital has no children");
            }
        }

        // Update is called once per frame
        private void Update()
        {
            // Why not loop through each of our orbital images and update their
            // unity position based on an zoom level that might have changed?

            if (solarSystem.Orbitals.Count > 0)
            {
                for (var i = 0; i <= solarSystem.Orbitals.Count - 1; i++)
                {
                    UpdateSprites(solarSystem.Orbitals[i]);
                }
            }
            else
            {
                Debug.Log("solarSystem has no children");
            }

        }

        public void SetZoomLevel(int zl)
        {
            zoomLevels = zl;
            // Update planet positions
            // Also consider scaling the graphics up/down -- but keep a minimum size
            // figure out what scale means that each planet will always at least be a 
            // few pixels big, no matter the zoom level.
        }
    }
}