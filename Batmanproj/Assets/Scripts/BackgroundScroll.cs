using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public float speed = 0.05f;
    Renderer rend;
    Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        offset.x += speed * Time.deltaTime;
        rend.material.SetTextureOffset("_BaseMap", offset);
    }
}
