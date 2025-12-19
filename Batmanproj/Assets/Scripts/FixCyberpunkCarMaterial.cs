using UnityEngine;
using UnityEditor;

/// <summary>
/// اسکریپت ابزار (Editor Tool) برای اصلاح خودکار متریال ماشین Cyberpunk
/// این اسکریپت:
/// 1. تکسچر بدنه ماشین را لود می‌کند
/// 2. یک متریال URP/Lit جدید می‌سازد
/// 3. متریال را روی تمام MeshRendererهای ماشین اعمال می‌کند
/// 4. LOD Group ماشین را غیرفعال می‌کند
///
/// فقط از منوی Tools در Unity Editor قابل اجراست
/// و در Runtime اجرا نمی‌شود.
/// </summary>
public class FixCyberpunkCarMaterial
{
    // ======================
    // منوی ابزار در یونیتی
    // ======================

    [MenuItem("Tools/Fix Cyberpunk Car Material")]
    static void FixMaterial()
    {
        // ======================
        // لود تکسچر بدنه ماشین
        // ======================

        // مسیر صحیح تکسچر Color (Albedo / Base Color)
        string texturePath = "Assets/Cyberpunk Car/Textures/Cyberpunk_Car_Body_A.tga";

        // بارگذاری تکسچر از مسیر پروژه
        Texture2D bodyTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);

        // بررسی وجود تکسچر
        if (bodyTexture == null)
        {
            Debug.LogError("❌ Body texture not found! Check path.");
            return;
        }

        // ======================
        // ساخت متریال جدید URP
        // ======================

        // ایجاد متریال با Shader استاندارد URP
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.name = "Auto_Car_Body_Mat";

        // ست کردن تکسچر روی Base Map
        mat.SetTexture("_BaseMap", bodyTexture);

        // ======================
        // ذخیره متریال در پروژه
        // ======================

        AssetDatabase.CreateAsset(mat, "Assets/Auto_Car_Body_Mat.mat");

        // ======================
        // یافتن ماشین در Scene
        // ======================

        GameObject car = GameObject.Find("Cyberpunk_Car");
        if (car == null)
        {
            Debug.LogError("❌ Cyberpunk_Car not found in Scene");
            return;
        }

        // ======================
        // غیرفعال کردن LOD Group
        // ======================

        // برای جلوگیری از تغییر متریال در فاصله‌های مختلف
        LODGroup lod = car.GetComponent<LODGroup>();
        if (lod != null)
            lod.enabled = false;

        // ======================
        // اعمال متریال روی تمام MeshRendererها
        // ======================

        foreach (MeshRenderer mr in car.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = mat;
        }

        // پیام موفقیت
        Debug.Log("✅ Car material fixed automatically!");
    }
}
