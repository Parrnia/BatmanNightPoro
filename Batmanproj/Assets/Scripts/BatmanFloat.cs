
using UnityEngine;

public class BatmanFloat : MonoBehaviour
{
    [Header("Float Settings")]
    public float amplitude = 0.5f;  // میزان بالا و پایین رفتن
    public float speed = 2f;        // سرعت حرکت

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;  // نقطه شروع را ذخیره کن
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);
    }
}
