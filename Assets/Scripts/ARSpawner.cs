using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARSpawner : MonoBehaviour
{
    [SerializeField] private GameObject m_spawnPrefab;

    private ARRaycastManager m_raycastManager;

    private void Awake()
    {
        m_raycastManager = GetComponent<ARRaycastManager>();
    }

    public void SpawnAtPosition(Vector2 screenSpacePosition)
    {
        if (m_spawnPrefab == null)
        {
            Debug.LogError("Spawn Prefab is null");
            return;
        }
        PlaceObject(screenSpacePosition);
    }

    private void PlaceObject(Vector2 castPosition)
    {
        List<ARRaycastHit> traceHits = new();
        m_raycastManager.Raycast(castPosition, traceHits, TrackableType.PlaneWithinPolygon);

        if (traceHits.Count > 0)
        {
            ARRaycastHit hit = traceHits.First();
            Vector3 hitPosition = hit.pose.position;
            Quaternion hitRotation = hit.pose.rotation;
            Instantiate(m_spawnPrefab, hitPosition, hitRotation);
        }
    }
}
