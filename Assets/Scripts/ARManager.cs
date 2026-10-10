using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(ARInteractor))]
[RequireComponent(typeof(ARSpawner))]
public class ARManager : MonoBehaviour
{
    private ARInteractor m_interactor;
    private ARSpawner m_spawner;
    private bool m_bInteracting = false;

    private static readonly WaitForSeconds m_waitForSeconds0_25 = new(0.25f);

    private void Awake()
    {
        m_interactor = GetComponent<ARInteractor>();
        m_spawner = GetComponent<ARSpawner>();
    }

    private void Update()
    {
        if (m_bInteracting)
        {
            return;
        }

        var touchscreen = Touchscreen.current;
        var mouse = Mouse.current;

        if (touchscreen != null && touchscreen.primaryTouch.press.wasPressedThisFrame)
        {
            var position = touchscreen.primaryTouch.position.ReadValue();
            HandleInteraction(position);
        }
        else if (mouse != null && mouse.leftButton.wasPressedThisFrame)
        {
            var position = mouse.position.ReadValue();
            HandleInteraction(position);
        }
    }

    private void HandleInteraction(Vector2 screenSpacePosition)
    {
        m_bInteracting = true;
        if (!m_interactor.TryInteractAtPosition(screenSpacePosition))
        {
            m_spawner.SpawnAtPosition(screenSpacePosition);
        }
        StartCoroutine(SetInteractingToFalseWithDelay());
    }

    private IEnumerator SetInteractingToFalseWithDelay()
    {
        yield return m_waitForSeconds0_25;
        m_bInteracting = false;
    }
}
