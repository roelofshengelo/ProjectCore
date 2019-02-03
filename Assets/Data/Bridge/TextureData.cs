//using UnityEngine;
//using System.Collections;
//using System.Collections.Generic;

//namespace Assets.Data.Bridge
//{
//    public class TextureData : MonoBehaviour
//    {

//        //public Texture2D TilesTexture;
//        public Sprite[] Sprites;
//        List<Sprite> _sprites;


//        // Use this for initialization
//        void Awake()
//        {
//            SetupSprites();
//        }

//        // Update is called once per frame
//        void Update()
//        {

//        }

//        public Sprite GetSprite(int id)
//        {
//            return _sprites[id];
//        }

//        private void SetupSprites()
//        {
//            _sprites = new List<Sprite>();

//            var resourceRequest = Resources.LoadAsync<Sprite>("/Media/Assets/Media");

//            var name = resourceRequest.asset.name;

//        }



//    }




//    //namespace Assets.Data.Bridge
//    //{
//    //    class TextureData
//    //    {

//    //    }

//    //    public class TextureDataOrbitals
//    //    {
//    //        public int SolarSystemId = -1;

//    //        public int NumberOfStars = 1;
//    //        public int NumberOfPlanets = 1;
//    //        public int NumberOfMoons = 1;

//    //    }


//}