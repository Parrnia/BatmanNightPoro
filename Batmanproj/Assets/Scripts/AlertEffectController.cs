using UnityEngine;

/// <summary>
/// کنترل‌کننده‌ی افکت‌های هشدار (Alert Mode)
/// شامل:
/// - نور چشمک‌زن قرمز/آبی
/// - صدای آژیر
/// این اسکریپت فقط زمانی فعال می‌شود که بازی در حالت Alert باشد.
/// </summary>
public class AlertEffectController : MonoBehaviour
{
    // ======================
    // نور و صدا
    // ======================

    // نور هشدار (پلیسی)
    public Light alertLight;

    // صدای آژیر در حالت Alert
    public AudioSource alarmAudio;

    // فاصله زمانی تغییر رنگ نور (چشمک زدن)
    public float blinkInterval = 0.3f;

    // تایمر داخلی برای کنترل چشمک زدن
    float timer;

    // مشخص می‌کند نور در حال حاضر قرمز است یا آبی
    bool isRed;

    // مشخص می‌کند که سیستم هشدار فعال است یا نه
    // فقط هنگام Alert State true می‌شود
    bool isActive;

    void Awake()
    {
        // تنظیمات اولیه نور هشدار
        if (alertLight != null)
        {
            alertLight.enabled = false;
            alertLight.color = Color.red;
            alertLight.intensity = 2f;
            alertLight.range = 5f;
        }

        // تنظیمات اولیه صدای آژیر
        if (alarmAudio != null)
        {
            alarmAudio.playOnAwake = false;
            alarmAudio.loop = true;
            alarmAudio.Stop();
        }
    }

    void Update()
    {
        // اگر در حالت Alert نیستیم یا نور نداریم، کاری انجام نده
        if (!isActive || alertLight == null) return;

        // مدیریت زمان برای تغییر رنگ نور
        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            timer = 0f;

            // تغییر رنگ بین قرمز و آبی
            isRed = !isRed;
            alertLight.color = isRed ? Color.red : Color.blue;
        }
    }

    // ======================
    // فعال‌سازی حالت Alert
    // ======================

    /// <summary>
    /// فعال‌سازی افکت‌های هشدار
    /// این متد فقط یک بار اجرا می‌شود
    /// و از اجرای مجدد در حالت فعال جلوگیری می‌کند.
    /// </summary>
    public void StartAlert()
    {
        // جلوگیری از اجرای تکراری Alert
        if (isActive) return;

        isActive = true;
        timer = 0f;
        isRed = true;

        // فعال کردن نور هشدار
        if (alertLight != null)
        {
            alertLight.enabled = true;
            alertLight.color = Color.red;
        }

        // پخش آژیر
        if (alarmAudio != null && !alarmAudio.isPlaying)
            alarmAudio.Play();
    }

    // ======================
    // غیرفعال‌سازی حالت Alert
    // ======================

    /// <summary>
    /// خاموش کردن افکت‌های هشدار
    /// هنگام خروج از حالت Alert
    /// </summary>
    public void StopAlert()
    {
        isActive = false;

        // خاموش کردن نور
        if (alertLight != null)
            alertLight.enabled = false;

        // توقف صدای آژیر
        if (alarmAudio != null)
            alarmAudio.Stop();
    }
}
