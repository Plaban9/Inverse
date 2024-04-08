
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class InversePass : ScriptableRenderPass
{

    private InverseFeature.CustomPassSettings settings;

    private RenderTargetIdentifier colorBuffer, temporaryBuffer;
    private int temporaryBufferID = Shader.PropertyToID("_TemporaryBuffer");

    Material material;

    public InversePass(InverseFeature.CustomPassSettings passSettings, Material material)
    {
        this.settings = passSettings;
        renderPassEvent = passSettings.renderPassEvent;
        this.material = material; 
    }


    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        colorBuffer = renderingData.cameraData.renderer.cameraColorTarget;
        RenderTextureDescriptor descriptor = renderingData.cameraData.cameraTargetDescriptor;

        cmd.GetTemporaryRT(temporaryBufferID, descriptor, FilterMode.Point);
        temporaryBuffer = new RenderTargetIdentifier(temporaryBufferID);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        CommandBuffer cmd = CommandBufferPool.Get();
        using (new ProfilingScope(cmd, new ProfilingSampler("InversePass")))
        {
            Blit(cmd, colorBuffer, temporaryBuffer, material);
            Blit(cmd, temporaryBuffer, colorBuffer);
        }
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        if (cmd == null) throw new ArgumentNullException("cmd");
        cmd.ReleaseTemporaryRT(temporaryBufferID);
    }
}