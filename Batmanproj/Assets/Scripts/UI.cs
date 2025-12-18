using UnityEngine;
using UnityEngine.UI;

public class UIBackgroundScroll : MonoBehaviour
{
    public float speed = 20f;

    private RawImage img;
    private Rect uvRect;

    void Start()
    {
        img = GetComponent<RawImage>();
        uvRect = img.uvRect;
    }

    void Update()
    {
        uvRect.x += speed * Time.deltaTime / 1000f;
        img.uvRect = uvRect;
    }
}
