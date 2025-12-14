using UnityEngine;

public class BatmanController : MonoBehaviour
{
    // ======================
    // حرکت
    // ======================
    public float normalMoveSpeed = 5f;
    public float boostMoveSpeed = 15f;
    public float rotateSpeed = 200f;

    // ======================
    // State ها
    // ======================
    public enum BatmanState
    {
        Normal,
        Stealth,
        Alert
    }

    public BatmanState currentState = BatmanState.Normal;

    // ======================
    // نور و صدا
    // ======================
    public Light environmentLight;
    public AudioSource alertAudio;

    void Update()
    {
        // 1. گرفتن ورودی تغییر حالت
        HandleStateInput();

        // 2. اعمال اثر هر حالت (نور و صدا)
        ApplyStateEffects();

        // 3. چرخش (A/D یا ← →)
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(0, horizontalInput * rotateSpeed * Time.deltaTime, 0);

        // 4. حرکت (W/S یا ↑ ↓)
        float verticalInput = Input.GetAxis("Vertical");

        // 5. تعیین سرعت بر اساس State
        float currentMoveSpeed;

        if (currentState == BatmanState.Normal)
        {
            // در حالت عادی Shift بوست می‌دهد
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                currentMoveSpeed = boostMoveSpeed;
            else
                currentMoveSpeed = normalMoveSpeed;
        }
        else if (currentState == BatmanState.Stealth)
        {
            // حرکت آهسته
            currentMoveSpeed = normalMoveSpeed * 0.4f;
        }
        else // Alert
        {
            // کمی سریع‌تر از حالت عادی
            currentMoveSpeed = normalMoveSpeed * 1.2f;
        }

        // 6. اعمال حرکت
        Vector3 moveDirection = transform.forward * verticalInput * currentMoveSpeed * Time.deltaTime;
        transform.Translate(moveDirection, Space.Self);
    }

    // ======================
    // تغییر State با کلیدها
    // ======================
    void HandleStateInput()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            currentState = BatmanState.Normal;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            currentState = BatmanState.Stealth;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            currentState = BatmanState.Alert;
        }
    }

    // ======================
    // اثر State روی نور و صدا
    // ======================
    void ApplyStateEffects()
    {
        if (currentState == BatmanState.Normal)
        {
            environmentLight.intensity = 1f;

            if (alertAudio.isPlaying)
                alertAudio.Stop();
        }
        else if (currentState == BatmanState.Stealth)
        {
            environmentLight.intensity = 0.3f;

            if (alertAudio.isPlaying)
                alertAudio.Stop();
        }
        else if (currentState == BatmanState.Alert)
        {
            environmentLight.intensity = 1.2f;

            if (!alertAudio.isPlaying)
                alertAudio.Play();
        }
    }
}
