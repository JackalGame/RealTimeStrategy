using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<MinimapIcon>(out MinimapIcon minimapIcon)) return;

        other.GetComponent<MinimapIcon>().ChangeMinimapIconState(11);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<MinimapIcon>(out MinimapIcon minimapIcon)) return;

        other.GetComponent<MinimapIcon>().ChangeMinimapIconState(9);
    }

    public void IncreaseRadarRadius()
    {
        SphereCollider collider = GetComponent<SphereCollider>();
        float currentRadius = collider.radius;
        collider.radius = currentRadius * 1.5f;
    }
}
