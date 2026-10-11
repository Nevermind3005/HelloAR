using UnityEngine;

public class ActivationManager : TSingleton<ActivationManager>
{
    private IActivatable m_activeActivatable;

    protected override void OnAwake()
    {
        m_bPersistent = false;
    }

    public void SelfWasInteractedWith(IActivatable newActivatable)
    {
        if (m_activeActivatable != newActivatable)
        {
            // Can be null when nothing is active
            m_activeActivatable?.Deactivate();
            m_activeActivatable = newActivatable;
            m_activeActivatable.Activate();
        }
        else
        {
            m_activeActivatable.Deactivate();
            m_activeActivatable = null;
        }
    }
}
