using UnityEngine;

[RequireComponent(typeof(IActivatable))]
public class ActivatableInteractable : MonoBehaviour, IInteractable
{
    private IActivatable m_activatable;

    private void Awake()
    {
        m_activatable = GetComponent<IActivatable>();
    }

    public void OnInteract()
    {
        var activationManager = ActivationManager.Instance;
        if (activationManager == null)
        {
            Debug.Log("Activation Manager Is null");
            return;
        }
        activationManager.SelfWasInteractedWith(m_activatable);
    }
}
