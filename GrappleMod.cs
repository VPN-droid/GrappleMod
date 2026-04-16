using BepInEx;
using UnityEngine;

[BepInPlugin("com.eli.grapplemod", "Grapple Mod", "1.0.0")]
public class GrappleMod : BaseUnityPlugin
{
    private float cooldown = 3f;
    private float lastUse = -10f;

    void Update()
    {
        // Right click (slot 1 ability)
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time >= lastUse + cooldown)
            {
                TryUseGrapple();
                lastUse = Time.time;
            }
        }
    }

    void TryUseGrapple()
    {
        var objects = FindObjectsOfType<MonoBehaviour>();

        foreach (var obj in objects)
        {
            var methods = obj.GetType().GetMethods();

            foreach (var m in methods)
            {
                if (m.Name.ToLower().Contains("grapple"))
                {
                    try
                    {
                        m.Invoke(obj, null);
                        Debug.Log("Grapple triggered via: " + m.Name);
                        return;
                    }
                    catch { }
                }
            }
        }
    }
}