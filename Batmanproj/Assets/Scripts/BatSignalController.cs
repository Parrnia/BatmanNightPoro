using UnityEngine;

/// <summary>
/// کنترل Bat-Signal
/// این اسکریپت وظیفه‌ی روشن / خاموش کردن نور Bat-Signal
/// و چرخش آن فقط در زمان روشن بودن نور را بر عهده دارد.
/// </summary>
public class BatSignalController : MonoBehaviour
{
    // ======================
    // تنظیمات نور
    // ======================

    [Header("Light Settings")]

    // رفرنس به نور Bat-Signal
    public Light batLight;

    // شدت نور در حالت خاموش
    public float offIntensity = 0f;

    // شدت نور در حالت روشن
    // مقدار بالا برای دیده شدن واضح در صحنه
    public float onIntensity = 70f;

    // ======================
    // تنظیمات چرخش
    // ======================

    [Header("Rotation Settings")]

    // سرعت چرخش نور Bat-Signal
    public float rotateSpeed = 18f;

    void Start()
    {
        // اگر نور به‌صورت دستی اختصاص داده نشده باشد
        // اولین Light موجود در فرزندان آبجکت به‌صورت خودکار گرفته می‌شود
        if (batLight == null)
            batLight = GetComponentInChildren<Light>();

        // شروع بازی با Bat-Signal خاموش
        batLight.intensity = offIntensity;
    }

    void Update()
    {
        // ======================
        // چرخش Bat-Signal
        // ======================

        // چرخش فقط زمانی انجام می‌شود که نور روشن باشد
        if (batLight.intensity > 0.1f)
        {
            transform.Rotate(0f, rotateSpeed * Time.deltaTime, 0f);
        }

        // ======================
        // کنترل روشن / خاموش
        // ======================

        // با فشردن کلید B ، نور Bat-Signal
        // بین حالت روشن و خاموش سوییچ می‌شود
        if (Input.GetKeyDown(KeyCode.B))
        {
            bool isOn = batLight.intensity > 0.1f;
            batLight.intensity = isOn ? offIntensity : onIntensity;
        }
    }
}
