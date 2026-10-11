using UnityEngine;

public class ARInteractor : MonoBehaviour
{
    [SerializeField]
    private LayerMask m_ineractableLayerMask;

    private Camera m_camera;

    private void Awake()
    {
        m_camera = Camera.main;
    }

    public bool TryInteractAtPosition(Vector2 screenSpacePosition)
    {
        var worldSpacePosition = ScreenSpaceToWorldSpace(screenSpacePosition);
        var direction = worldSpacePosition - m_camera.transform.position;

        if (Physics.Raycast(m_camera.transform.position, direction, out RaycastHit hit, float.MaxValue, m_ineractableLayerMask))
        {
            if (hit.collider.TryGetComponent<IInteractable>(out var interactable))
            {
                interactable.OnInteract();
                return true;
            }
        }

        return false;
    }

    private Vector3 ScreenSpaceToWorldSpace(Vector2 screenSpacePosition)
    {
        return m_camera.ScreenToWorldPoint(new Vector3(screenSpacePosition.x, screenSpacePosition.y, 1.0f));
    }
}
