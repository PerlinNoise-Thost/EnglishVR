using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private Material m_Material;
    
    private bool isFading = false;  

    public void TriggerScreenFade()
    {
        isFading = true;
    }

    /*void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (isFading)
        {
            Graphics.Blit(src, dest, m_Material); 
        }
    }*/
    
}