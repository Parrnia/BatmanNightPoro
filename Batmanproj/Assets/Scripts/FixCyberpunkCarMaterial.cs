using UnityEngine;
using UnityEditor;

public class FixCyberpunkCarMaterial
{
    [MenuItem("Tools/Fix Cyberpunk Car Material")]
    static void FixMaterial()
    {
        // مسیر تکسچر درست (Color)
        string texturePath = "Assets/Cyberpunk Car/Textures/Cyberpunk_Car_Body_A.tga";
        Texture2D bodyTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(texturePath);

        if (bodyTexture == null)
        {
            Debug.LogError("❌ Body texture not found! Check path.");
            return;
        }

        // ساخت Material
        Material mat = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        mat.name = "Auto_Car_Body_Mat";
        mat.SetTexture("_BaseMap", bodyTexture);

        // ذخیره Material
        AssetDatabase.CreateAsset(mat, "Assets/Auto_Car_Body_Mat.mat");

        // پیدا کردن ماشین در Scene
        GameObject car = GameObject.Find("Cyberpunk_Car");
        if (car == null)
        {
            Debug.LogError("❌ Cyberpunk_Car not found in Scene");
            return;
        }

        // خاموش کردن LOD Group
        LODGroup lod = car.GetComponent<LODGroup>();
        if (lod != null)
            lod.enabled = false;

        // ست کردن متریال روی همه MeshRendererها
        foreach (MeshRenderer mr in car.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material = mat;
        }

        Debug.Log("✅ Car material fixed automatically!");
    }
}
