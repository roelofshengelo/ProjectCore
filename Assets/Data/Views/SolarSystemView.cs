﻿using System;
using System.Collections.Generic;
using System.Globalization;
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

        //public ulong zoomLevels = 1000000; 1x million
        public ulong zoomLevels = 150000000000;

        // Use this for initialization
        private void Start()
        {
            gameController = FindObjectOfType<GameController>();
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
            //MakeSpritesForOrbital(transform, solarSystem.Children[0]);

            //return;
            // Spawn a graphic for each object in the solar system
            for (var i = 0; i < solarSystem.Children.Count; i++)
            // Spawn a graphic for each orbital of the first sun
            //for (var i = 1; i < solarSystem.Children.Count - 1; i++)
            {
                //Orbital orbital = solarSystem.Children[i];
                MakeSpritesForOrbital(transform, solarSystem.Children[i]);
            }
        }

        private void MakeSpritesForOrbital(Transform transformParent, Orbital orbital)
        {
            var go = new GameObject();
            orbitalGameObjectMap[orbital] = go;
            go.transform.SetParent(transformParent);

            // Set our position.
            go.transform.position = orbital.Position / zoomLevels;

            var sr = go.AddComponent<SpriteRenderer>();
            sr.drawMode = SpriteDrawMode.Sliced;
            sr.sprite = Sprites[orbital.GraphicID];
            sr.size = new Vector2(1, 1);



            switch (orbital.Type)
            {
                case Orbital.OrbitalType.Star:
                    sr.name = "Star";
                    break;
                case Orbital.OrbitalType.Planet:
                    sr.name = "Planet";
                    break;
                case Orbital.OrbitalType.Moon:
                    sr.name = "Moon";
                    break;
                case Orbital.OrbitalType.Moonmoon:
                    sr.name = "Moonmoon";
                    Debug.Log("Orbital is set as Moonmoon, FIXME!!!");
                    break;
                case Orbital.OrbitalType.UnSet:
                    throw new ArgumentOutOfRangeException("Orbital doesn't have a (correct) OrbitalType");
                default:
                    throw new ArgumentOutOfRangeException("Orbital doesn't have a OrbitalType");
            }

            for (var i = 0; i < orbital.Children.Count - 1; i++)
            {
                MakeSpritesForOrbital(go.transform, orbital.Children[i]);
            }

        }

        private void UpdateSprites(Orbital orbital)
        {
            if (solarSystem.Children.Count > 0)
            {
                var go = orbitalGameObjectMap[orbital];
                go.transform.position = orbital.Position / zoomLevels;
                go.name = orbital.OffsetAngle.ToString(CultureInfo.InvariantCulture);

                for (var i = 0; i < orbital.Children.Count - 1; i++)
                {
                    UpdateSprites(orbital.Children[i]);
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

            if (solarSystem.Children.Count > 0)
            {
                for (var i = 0; i < solarSystem.Children.Count - 1; i++)
                {
                    UpdateSprites(solarSystem.Children[i]);
                }
            }
            else
            {
                Debug.Log("solarSystem has no children");
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