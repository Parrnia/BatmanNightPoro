using UnityEngine;

/// <summary>
/// کنترل‌کننده‌ی اصلی Batmobile / Batman
/// مسئول مدیریت:
/// - حرکت و چرخش
/// - تغییر حالت (Normal / Stealth / Alert)
/// - هماهنگی نور محیط و افکت‌های هشدار
/// </summary>
public class BatmanController : MonoBehaviour
{
    // ======================
    // تنظیمات حرکت
    // ======================

    // سرعت حرکت در حالت عادی
    public float normalMoveSpeed = 5f;

    // سرعت حرکت در حالت بوست
    public float boostMoveSpeed = 15f;

    // سرعت چرخش هنگام حرکت چپ و راست
    public float rotateSpeed = 200f;

    // کنترل افکت‌های بصری و صوتی حالت Alert
    public AlertEffectController alertEffects;

    // ======================
    // State ها (حالت‌های بتمن)
    // ======================

    /// <summary>
    /// حالت‌های مختلف عملکرد بتمن در گشت شبانه
    /// </summary>
    public enum BatmanState
    {
        Normal,   // گشت عادی
        Stealth,  // حرکت مخفیانه
        Alert     // حالت هشدار
    }

    // حالت فعلی بازی
    public BatmanState currentState = BatmanState.Normal;

    // ======================
    // نور و صدا
    // ======================

    // نور محیط شهر (برای القای فضای شبانه)
    public Light environmentLight;

    // صدای هشدار (در حالت Alert)
    public AudioSource alertAudio;

    void Update()
    {
        // 1. بررسی ورودی کاربر برای تغییر حالت بازی
        HandleStateInput();

        // 2. اعمال اثرات بصری و صوتی مربوط به حالت فعلی
        ApplyStateEffects();

        // ======================
        // چرخش ماشین
        // ======================

        // ورودی افقی (A/D یا ← →)
        float horizontalInput = Input.GetAxis("Horizontal");

        // چرخش حول محور Y
        transform.Rotate(0, horizontalInput * rotateSpeed * Time.deltaTime, 0);

        // ======================
        // حرکت رو به جلو / عقب
        // ======================

        // ورودی عمودی (W/S یا ↑ ↓)
        float verticalInput = Input.GetAxis("Vertical");

        // تعیین سرعت نهایی حرکت بر اساس حالت فعلی
        float currentMoveSpeed;

        if (currentState == BatmanState.Normal)
        {
            // در حالت عادی امکان بوست با کلید Shift وجود دارد
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                currentMoveSpeed = boostMoveSpeed;
            else
                currentMoveSpeed = normalMoveSpeed;
        }
        else if (currentState == BatmanState.Stealth)
        {
            // در حالت مخفیانه حرکت آهسته‌تر برای عدم جلب توجه
            currentMoveSpeed = normalMoveSpeed * 0.4f;
        }
        else // Alert
        {
            // در حالت هشدار سرعت کمی افزایش می‌یابد
            currentMoveSpeed = normalMoveSpeed * 1.2f;
        }

        // اعمال حرکت نهایی در راستای جهت نگاه ماشین
        Vector3 moveDirection =
            transform.forward * verticalInput * currentMoveSpeed * Time.deltaTime;

        transform.Translate(moveDirection, Space.Self);
    }

    // ======================
    // تغییر State با ورودی صفحه‌کلید
    // ======================

    /// <summary>
    /// بررسی کلیدهای فشرده‌شده برای تغییر حالت بازی
    /// </summary>
    void HandleStateInput()
    {
        // حالت عادی
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentState = BatmanState.Normal;
        }
        // حالت مخفیانه
        else if (Input.GetKeyDown(KeyCode.C))
        {
            currentState = BatmanState.Stealth;
        }
        // حالت هشدار
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = BatmanState.Alert;
        }
    }

    // ======================
    // اثر State روی نور و افکت‌ها
    // ======================

    /// <summary>
    /// تنظیم نور محیط و افکت هشدار بر اساس حالت فعلی
    /// </summary>
    void ApplyStateEffects()
    {
        if (currentState == BatmanState.Normal)
        {
            // نور طبیعی شهر
            environmentLight.intensity = 1f;

            // غیرفعال کردن افکت‌های هشدار
            alertEffects.StopAlert();
        }
        else if (currentState == BatmanState.Stealth)
        {
            // تاریک‌تر شدن محیط برای حس مخفی‌کاری
            environmentLight.intensity = 0.3f;

            alertEffects.StopAlert();
        }
        else if (currentState == BatmanState.Alert)
        {
            // افزایش نور برای القای هشدار
            environmentLight.intensity = 1.2f;

            // فعال‌سازی افکت‌های بصری و صوتی هشدار
            alertEffects.StartAlert();
        }
    }
}
