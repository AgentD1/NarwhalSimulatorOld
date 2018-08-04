using UnityEngine;

[ExecuteInEditMode]
public class BoxBlur : MonoBehaviour {
    public Material BlurMaterial;
    [Range(0, 10)]
    public int iterations;
    [Range(0, 4)]
    public int downRes;

    private void OnRenderImage(RenderTexture s, RenderTexture d) {
        int width = s.width >> downRes;
        int height = s.height >> downRes;

        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        Graphics.Blit(s, rt);
        for (int i = 0; i < iterations; i++) {
            RenderTexture rt2 = RenderTexture.GetTemporary(width, height);
            Graphics.Blit(rt, rt2, BlurMaterial);
            RenderTexture.ReleaseTemporary(rt);
            rt = rt2;
        }
        Graphics.Blit(rt, d);
        RenderTexture.ReleaseTemporary(rt);
    }
}
