using UnityEngine;

public class ExampleSpriteRenderer : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprRend;

    void Awake()
    {
        sprRend = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sprRend.drawMode = SpriteDrawMode.Sliced;

        rb = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("image256x128");
        gameObject.transform.Translate(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        //Press the Space key to increase the size of the sprite
        if (Input.GetKey(KeyCode.Space))
        {
            sprRend.size += new Vector2(0.05f, 0.01f);
            Debug.Log("Sprite size: " + sprRend.size.ToString("F2"));
        }
    }
}