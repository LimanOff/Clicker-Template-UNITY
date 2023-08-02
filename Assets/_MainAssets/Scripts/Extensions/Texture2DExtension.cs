using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Texture2DExtension
{
    public static Texture2D DuplicateTextureWithAllAccess(this Texture2D source)
    {
        RenderTexture renderTex = RenderTexture.GetTemporary(
                    source.width,
                    source.height,
                    0,
                    RenderTextureFormat.Default,
                    RenderTextureReadWrite.Linear);

        Graphics.Blit(source, renderTex);

        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTex;

        Texture2D textureWithAllAccess = new Texture2D(source.width, source.height);
        textureWithAllAccess.ReadPixels(new Rect(0, 0, renderTex.width, renderTex.height), 0, 0);
        textureWithAllAccess.Apply();

        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTex);

        return textureWithAllAccess;
    }
}
