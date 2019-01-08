using UnityEngine;

public class CameraController : MonoBehaviour
{
    float mainSpeed = 100.0f; //regular speed
    private float totalRun = 1.0f;

    void Update()
    {
        //Keyboard commands
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