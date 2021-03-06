using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamColourSetter : NetworkBehaviour
{
    [SerializeField] private Renderer[] colourRenderers = new Renderer[0];

    [SyncVar(hook = nameof(HandleTeamColourUpdated))]
    private Color teamColour = new Color();

    #region Server

    public override void OnStartServer()
    {
        RTSPlayer player = connectionToClient.identity.GetComponent<RTSPlayer>();

        teamColour = player.GetTeamColour();
    }

    #endregion

    #region Client

    private void HandleTeamColourUpdated(Color oldColour, Color newColour)
    {
        foreach(Renderer renderer in colourRenderers)
        {
            foreach(Material material in renderer.materials)
            {
                renderer.material.SetColor("_BaseColor", newColour);
            }
        }
    }

    #endregion
}
