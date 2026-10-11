using System;
using UnityEngine;

[RequireComponent(typeof(StencilBufferWriter))]
public class Activatable : MonoBehaviour, IActivatable
{
    public event Action OnActivate;
    public event Action OnDeactivate;

    private StencilBufferWriter m_stencilBufferWriter;

    private void Awake()
    {
        m_stencilBufferWriter = GetComponent<StencilBufferWriter>();
    }

    public void Activate()
    {
        m_stencilBufferWriter.SetStencil(1);
        OnActivate?.Invoke();
    }

    public void Deactivate()
    {
        m_stencilBufferWriter.SetStencil(0);
        OnDeactivate?.Invoke();
    }
}
