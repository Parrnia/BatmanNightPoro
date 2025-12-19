using UnityEngine;

/// <summary>
/// ایجاد حرکت شناور (Float / Hover)
/// این اسکریپت باعث می‌شود آبجکت به‌صورت نرم
/// و سینوسی در محور Y بالا و پایین برود.
/// مناسب برای:
/// - بتمن شناور
/// - Bat-Signal
/// - المان‌های تزئینی UI یا محیط
/// </summary>
public class BatmanFloat : MonoBehaviour
{
    // ======================
    // تنظیمات شناوری
    // ======================

    [Header("Float Settings")]

    // دامنه حرکت (میزان بالا و پایین رفتن)
    public float amplitude = 0.5f;

    // سرعت حرکت سینوسی
    public float speed = 2f;

    // ======================
    // متغیرهای داخلی
    // ======================

    // ذخیره موقعیت اولیه آبجکت
    // تا حرکت همیشه نسبت به نقطه شروع انجام شود
    private Vector3 startPos;

    void Start()
    {
        // ذخیره موقعیت اولیه در شروع بازی
        startPos = transform.position;
    }

    void Update()
    {
        // محاسبه مقدار جابه‌جایی عمودی
        // استفاده از Sin باعث حرکت نرم و طبیعی می‌شود
        float offset = Mathf.Sin(Time.time * speed) * amplitude;

        // اعمال حرکت شناور فقط روی محور Y
        transform.position = new Vector3(
            startPos.x,
            startPos.y + offset,
            startPos.z
        );
    }
}
