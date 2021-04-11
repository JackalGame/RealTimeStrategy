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
}
