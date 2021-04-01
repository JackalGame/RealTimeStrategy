using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitBasePrefab = null;
    [SerializeField] private GameOverHandler gameOverHandlerPrefab = null;

    public static event Action ClientOnConnected;
    public static event Action ClientOnDisconnected;

    private bool isGameInProgress = false;

    public List<RTSPlayer> players { get; } = new List<RTSPlayer>();

    #region Server

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (!isGameInProgress) { return; }

        conn.Disconnect();
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        players.Remove(player);
        
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        players.Clear();

        isGameInProgress = false;
    }

    public void StartGame()
    {
        if(players.Count < 2) { return; }

        isGameInProgress = true;

        ServerChangeScene("Scene_Map_01");
    }

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        RTSPlayer player = conn.identity.GetComponent<RTSPlayer>();

        players.Add(player);

        player.SetTeamColour(UnityEngine.Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f));

        player.SetPartyOwner(players.Count == 1);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (SceneManager.GetActiveScene().name.StartsWith("Scene_Map"))
        {
            GameOverHandler gameOverHandlerInstance = Instantiate(gameOverHandlerPrefab);

            NetworkServer.Spawn(gameOverHandlerInstance.gameObject);

            foreach(RTSPlayer player in players)
            {
                GameObject baseInstance = Instantiate(
                    unitBasePrefab, GetStartPosition().position, Quaternion.identity);

                NetworkServer.Spawn(baseInstance, player.connectionToClient);
            }
        }
    }

    #endregion

    #region Client

    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);

        ClientOnConnected?.Invoke();
    }

    public override void OnClientDisconnect(NetworkConnection conn)
    {
        base.OnClientDisconnect(conn);

        ClientOnDisconnected?.Invoke();
    }

    public override void OnStopClient()
    {
        players.Clear();
    }

    #endregion

}

