using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class GlassEffectController : MonoBehaviour
{
    public float blurSize = 1.5f;
    [Range(0, 1)]
    public float feather = 0.3f;
    [Range(0, 0.05f)]
    public float distortStrength = 0.015f;
    public Color colorTint = new Color(1f, 1f, 1f, 0.4f);

    private Material material;

    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (material == null) return;

        material.SetFloat("_BlurSize", blurSize);
        material.SetFloat("_Feather", feather);
        material.SetFloat("_DistortStrength", distortStrength);
        material.SetColor("_Color", colorTint);
    }
}
