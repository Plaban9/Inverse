using UnityEngine;
using UnityEngine.Rendering.Universal;

public class InverseFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class CustomPassSettings
    {
        public RenderPassEvent renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        public Material material = null;
    }

    [SerializeField]
    public CustomPassSettings passSettings = new();
    InversePass pass;

    public override void Create()
    {
        pass = new InversePass(passSettings, passSettings.material);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        renderer.EnqueuePass(pass);
    }

    public Material Material
    {
        get => passSettings.material;
    }
}