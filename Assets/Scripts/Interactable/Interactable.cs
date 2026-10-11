using System;
using UnityEngine;

[Obsolete]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(StencilBufferWriter))]
public class Interactable : MonoBehaviour, IInteractable
{
    private Rigidbody m_rigidbody;
    private StencilBufferWriter m_stencilBufferWriter;
    private bool m_bSelected = false;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_stencilBufferWriter = GetComponent<StencilBufferWriter>();
    }

    public void OnInteract()
    {
        m_bSelected = !m_bSelected;
        m_stencilBufferWriter.SetStencil(m_bSelected ? 1 : 0);
    }
}
