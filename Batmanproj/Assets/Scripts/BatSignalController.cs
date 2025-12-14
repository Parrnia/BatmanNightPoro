using UnityEngine;

public class BatSignalController : MonoBehaviour
{
    [Header("Light Settings")]
    public Light batLight;
    public float offIntensity = 0f;
    public float onIntensity = 70f;   // شدت زیاد و واضح

    [Header("Rotation Settings")]
    public float rotateSpeed = 18f;   // سرعت چرخش (واضح‌تر از قبل)

    void Start()
    {
        if (batLight == null)
            batLight = GetComponentInChildren<Light>();

        batLight.intensity = offIntensity;
    }

    void Update()
    {
        // ✅ چرخش فقط وقتی Bat-Signal روشنه
        if (batLight.intensity > 0.1f)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }

        // ✅ روشن / خاموش با B
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool isOn = batLight.intensity > 0.1f;
            batLight.intensity = isOn ? offIntensity : onIntensity;
        }
    }
}
