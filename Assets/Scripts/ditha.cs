using UnityEngine;
[ExecuteInEditMode, ImageEffectAllowedInSceneView, RequireComponent(typeof(Camera))]
public class ditha : MonoBehaviour
{
    public Material ditherMat;
    [Range(0.0f, 1.0f)]
    public float ditherStrength = 0.1f;
    [Range(1, 32)]
    public int colourDepth = 4;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        ditherMat.SetFloat("_DitherStrength", ditherStrength);
        ditherMat.SetInt("_ColourDepth", colourDepth);
        Graphics.Blit(src, dest, ditherMat);
    }
}