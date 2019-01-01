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
        public Transform[] backgrounds;
        public float[] parallaxScales;
        public float smoothing;

        //private Transform cam;

        private Vector3 previousCamPos;

        void Start()
        {
            previousCamPos = transform.position;
            parallaxScales = new float[backgrounds.Length];

            for (int i = 0; i < backgrounds.Length; i++)
            {
                parallaxScales[i] = backgrounds[i].position.z * -1;
                Debug.Log(parallaxScales[i]);

            }

        }

        void Update()
        {
            //Mouse commands
            transform.Translate(new Vector3(0, 0, Input.mouseScrollDelta.y));
            for (int i = 0; i < backgrounds.Length; i++)
            {
                var parallaxX = (previousCamPos.x - transform.position.x) * parallaxScales[i];
                var parallaxY = (previousCamPos.y - transform.position.y) * parallaxScales[i];
                //var parallaxZ = (previousCamPos.z - transform.position.z);

                var backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
                var backgroundTargetPosy = backgrounds[i].position.y + parallaxY;
                //var backgroundTargetPosZ = transform.position.z; //backgrounds[i].position.z - parallaxZ;

                var backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosy, backgrounds[i].position.z);

                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
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
            Vector3 p_Velocity = new Vector3();
            if (Input.GetKey(KeyCode.W))
            {
                p_Velocity += new Vector3(0, 1, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                p_Velocity += new Vector3(0, -1, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                p_Velocity += new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                p_Velocity += new Vector3(1, 0, 0);
            }
            return p_Velocity;
        }
    }
}
