using System;
using UnityEngine;
using System.Collections;
using UnityEditor;

namespace Assets.Data
{
    class CameraController : MonoBehaviour
    {
        // Camera movement
        float mainSpeed = 100.0f; //regular speed
        private float totalRun = 1.0f;

        //Camera background

        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once FieldCanBeMadeReadOnly.Global
        public Transform[] Backgrounds;

        // ReSharper disable once MemberCanBePrivate.Global
        public float[] ParallaxScales;

        // ReSharper disable once MemberCanBePrivate.Global
        public float Smoothing = 0f;

        //private Transform cam;

        private Vector3 previousCamPos;

        public CameraController(Transform[] backgrounds)
        {
            Backgrounds = backgrounds;
        }

        void Start()
        {
            if (Backgrounds is null) throw new ArgumentNullException("backgrounds of CameraController must be set!");
            if (ParallaxScales is null) throw new ArgumentNullException("parallaxScales of CameraController must be set!");
            //if (Smoothing is 0f) throw new ArgumentNullException("parallaxScales of CameraController must be set!");

            previousCamPos = transform.position;
            ParallaxScales = new float[Backgrounds.Length];

            for (int i = 0; i < Backgrounds.Length; i++)
            {
                ParallaxScales[i] = Backgrounds[i].position.z * -1;
                //Debug.Log(parallaxScales[i]);

            }

        }

        void Update()
        {
            //Mouse commands
            transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y));
            for (int i = 0; i < Backgrounds.Length; i++)
            {
                var parallaxX = (previousCamPos.x - transform.position.x) * ParallaxScales[i];
                var parallaxY = (previousCamPos.y - transform.position.y) * ParallaxScales[i];
                //var parallaxZ = (previousCamPos.z - transform.position.z);

                var backgroundTargetPosX = Backgrounds[i].position.x + parallaxX;
                var backgroundTargetPosy = Backgrounds[i].position.y + parallaxY;
                //var backgroundTargetPosZ = transform.position.z; //backgrounds[i].position.z - parallaxZ;

                var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosy, Backgrounds[i].position.z);

                Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, backgroundTargetPos, Smoothing * Time.deltaTime);
            }
            previousCamPos = transform.position;

            //Keyboard commands
            float f = 0.0f;
            Vector3 p = GetBaseInput();
            totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
            p = p * mainSpeed;

            p = p * Time.deltaTime;
            transform.Translate(p);
        }


        private Vector3 GetBaseInput()
        { //returns the basic values, if it's 0 than it's not active.
            Vector3 pVelocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                pVelocity += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                pVelocity += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                pVelocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                pVelocity += new Vector3(1, 0, 0);
            }
            return pVelocity;
        }
    }
}
