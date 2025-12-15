using UnityEngine;

public class AlertEffectController : MonoBehaviour
{
    public Light alertLight;
    public AudioSource alarmAudio;

    public float blinkInterval = 0.3f;

    float timer;
    bool isRed;
    bool isActive; // فقط وقتی Alert هست true میشه

    void Awake()
    {
        if (alertLight != null)
        {
            alertLight.enabled = false;
            alertLight.color = Color.red;
            alertLight.intensity = 2f;
            alertLight.range = 5f;
        }

        if (alarmAudio != null)
        {
            alarmAudio.playOnAwake = false;
            alarmAudio.loop = true;
            alarmAudio.Stop();
        }
    }

    void Update()
    {
        if (!isActive || alertLight == null) return;

        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            timer = 0f;
            isRed = !isRed;
            alertLight.color = isRed ? Color.red : Color.blue;
        }
    }

    // 🔴 وقتی وارد Alert State میشی
 public void StartAlert()
  {
    if (isActive) return; // 👈 کلید حل مشکل

    isActive = true;
    timer = 0f;
    isRed = true;

    if (alertLight != null)
    {
        alertLight.enabled = true;
        alertLight.color = Color.red;
    }

    if (alarmAudio != null && !alarmAudio.isPlaying)
        alarmAudio.Play();
}


    // 🔵 وقتی از Alert خارج میشی
    public void StopAlert()
    {
        isActive = false;

        if (alertLight != null)
            alertLight.enabled = false;

        if (alarmAudio != null)
            alarmAudio.Stop();
    }
}

