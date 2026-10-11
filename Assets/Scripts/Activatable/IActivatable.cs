using UnityEngine;

public interface IActivatable
{
    event System.Action OnActivate;
    event System.Action OnDeactivate;

    public void Activate();
    public void Deactivate();
}
