using System.Collections.Generic;
using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data.Views
{
    public class SolarSystemView : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            gameController = GameObject.FindObjectOfType<GameController>();
            ShowSolarSystem(0);
        }

        GameController gameController;
        SolarSystem solarSystem;

        public Sprite[] Sprites;

        //public ulong zoomLevels = 1000000; 1x million
        public ulong zoomLevels = 150000000000;


        Dictionary<Orbital, GameObject> orbitalGameObjectMap;
    
        public void ShowSolarSystem(int solarSystemID)
        {
            // First, clean up the solar system by deleting any old graphics.
            while (transform.childCount > 0)
            {
                var c = transform.GetChild(0);
                c.SetParent(null);  // Become Batman
                Destroy(c.gameObject);
            }

            orbitalGameObjectMap = new Dictionary<Orbital, GameObject>();

            solarSystem = gameController.Galaxy.SolarSystems[solarSystemID];

            // Spawn a graphic for each object in the solar system

            for (var i = 0; i < solarSystem.Children.Count; i++)
            {
                //Orbital orbital = solarSystem.Children[i];
                MakeSpritesForOrbital(this.transform, solarSystem.Children[i]);
            }
        }

        void MakeSpritesForOrbital(Transform transformParent, Orbital orbital)
        {
            var go = new GameObject();
            orbitalGameObjectMap[orbital] = go;
            go.transform.SetParent(transformParent);

            // Set our position.
            go.transform.position = orbital.Position / zoomLevels;

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();

            if (orbital.GraphicID == 0)
            {
                Debug.Log("Creating a star!");
            }



            sr.sprite = Sprites[orbital.GraphicID];

            for (var i = 0; i < orbital.Children.Count - 1; i++)
            {
                MakeSpritesForOrbital(go.transform, orbital.Children[i]);
            }
        }

        void UpdateSprites(Orbital orbital)
        {

            if (solarSystem.Children.Count > 0)
            {
                var go = orbitalGameObjectMap[orbital];
                go.transform.position = orbital.Position/zoomLevels;
                go.name = orbital.OffsetAngle.ToString();

                Debug.Log("============================================");
                //Debug.Log(string.Format("orbital {0}({1})", orbital, orbital.GraphicID));
                Debug.Log(string.Format("orbital.Children {0}", orbital.Children.Count));

                for (var i = 0; i < orbital.Children.Count - 1; i++)
                {
                    Debug.Log(string.Format("i {0};", i));
                    UpdateSprites(orbital.Children[i]);
                }
            }
            else
            {
                Debug.Log("orbital has no cildren");
            }



     


        }

        // Update is called once per frame
        void Update()
        {
            // Why not loop through each of our orbital images and update their
            // unity position based on an zoom level that might have changed?

            if (solarSystem.Children.Count > 0)
            {
                for (int i = 0; i < solarSystem.Children.Count - 1; i++)
                {
                    //Debug.Log(string.Format("i {0};", i));
                    UpdateSprites(solarSystem.Children[i]);
                }
            }
            else
            {
                Debug.Log("solarSystem has no cildren");
            }




        }



        public void SetZoomLevel(ulong zl)
        {
            zoomLevels = zl;
            // Update planet positions
            // Also consider scaling the graphics up/down -- but keep a minimum size
            // figure out what scale means that each planet will always at least be a 
            // few pixels big, no matter the zoom level.
        }
    }
}
