using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MinimapIcon : NetworkBehaviour
{
    [SerializeField] GameObject minimapIcon = null;

    public override void OnStartAuthority()
    {
        minimapIcon.layer = 11;
    }

    public void ChangeMinimapIconState(int layer)
    {
        if (connectionToClient == connectionToClient) { return; }
        Debug.Log("Radar found object");

        minimapIcon.layer = layer;
    }
}
