using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class StencilBufferWriter : MonoBehaviour
{
    [SerializeField, MaterialSlot] 
    private int m_materialIndex;

    private Material m_stencilMaterial;

    private static readonly int StencilMaskId = Shader.PropertyToID("_StencilMaskId");

    private void Awake()
    {
        m_stencilMaterial = GetComponent<Renderer>().materials[m_materialIndex];
    }

    public void SetStencil(int stencilMaskValue)
    {
        Debug.Log("Settign Stencil Value");
        m_stencilMaterial.SetInt(StencilMaskId, stencilMaskValue);
        int stencilInt = m_stencilMaterial.GetInt(StencilMaskId);
        Debug.Log(stencilInt);
    }
}
