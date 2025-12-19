using UnityEngine;

/// <summary>
/// اسکریپت اسکرول‌دهی تکسچر بک‌گراند
/// با جابه‌جایی UV متریال، حس حرکت مداوم (Endless Background)
/// را بدون حرکت دادن خود آبجکت ایجاد می‌کند.
/// </summary>
public class BackgroundScroller : MonoBehaviour
{
    // ======================
    // تنظیمات اسکرول
    // ======================

    // سرعت حرکت تکسچر در محور افقی (X)
    // مقدار مثبت باعث حرکت تصویر به سمت راست می‌شود
    public float scrollSpeedX = 0.02f;

    // سرعت حرکت تکسچر در محور عمودی (Y)
    // معمولاً برای حرکت رو به پایین یا بالا استفاده می‌شود
    public float scrollSpeedY = 0f;

    // ======================
    // متغیرهای داخلی
    // ======================

    // رفرنس به Renderer آبجکت
    private Renderer rend;

    // مقدار آفست فعلی تکسچر
    private Vector2 offset;

    void Start()
    {
        // گرفتن کامپوننت Renderer برای دسترسی به متریال
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // دریافت مقدار فعلی آفست تکسچر
        offset = rend.material.mainTextureOffset;

        // افزایش آفست بر اساس سرعت و زمان
        // استفاده از Time.deltaTime باعث می‌شود حرکت مستقل از فریم‌ریت باشد
        offset.x += scrollSpeedX * Time.deltaTime;
        offset.y += scrollSpeedY * Time.deltaTime;

        // اعمال آفست جدید به متریال
        rend.material.mainTextureOffset = offset;
    }
}
