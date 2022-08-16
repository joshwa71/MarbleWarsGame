using System.Collections.Generic;
using UnityEngine;
using Fusion.Sockets;
using System;
using Photon.Pun;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Fusion.Sample.DedicatedServer {

  [SimulationBehaviour(Modes = SimulationModes.Server)]
  public class GameManager : SimulationBehaviour, INetworkRunnerCallbacks {

    [SerializeField] private NetworkObject _playerPrefab;
    [SerializeField] private NetworkObject _platformPrefab;
    [SerializeField] private NetworkObject _playerCamera;
    [SerializeField] private GameObject _loadScreen;
    [SerializeField] private NetworkRunner _runner;
    [SerializeField] private bool _ranCoroutine = false;
    
    private Checkpoints script_checkpoints = null;
    private string checkpoint_name = "";
    public int checkpoints_passed = 0;


    public Dictionary<PlayerRef, NetworkObject> _playerMap = new Dictionary<PlayerRef, NetworkObject>();

    private void Awake() {
      _runner = GetComponent<NetworkRunner>();
    }

    public override void FixedUpdateNetwork() {
      if(_playerMap.Count > 0 && _ranCoroutine == false){
        StartCoroutine(wait(_runner));   
        _ranCoroutine = true;   
      }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) {

      if (runner.IsServer && _playerPrefab != null) {

        var pos = 2 * UnityEngine.Random.insideUnitSphere;

        pos.x += 0;
        pos.y += 4;
        pos.z += 0;        

        runner.Spawn(_platformPrefab, Vector3.zero, Quaternion.identity, inputAuthority: null);
        var character = runner.Spawn(_playerPrefab, pos, Quaternion.identity, inputAuthority: player);
        
        _playerMap[player] = character;

        Log.Info($"Spawn for Player: {player}");


      }
    }

//OnplayerJoined calling coroutine twice

    IEnumerator wait(NetworkRunner runner)
    {
      yield return new WaitForSeconds(5);
      runner.SetActiveScene((int)SceneDefs.GAME);      
      foreach (PlayerRef ply in _playerMap.Keys){
        Debug.Log("Despawning player " + ply);
        DespawnLobby(runner, ply);   
      }
    }


    public void OnSceneLoadDone(NetworkRunner runner) { 
      foreach (PlayerRef ply in _playerMap.Keys){
        Debug.Log("Spawning player " + ply);
        SpawnGame(runner, ply);
      }
    }


    public void OnSceneLoadStart(NetworkRunner runner) {
      
    }


    void DespawnLobby(NetworkRunner runner, PlayerRef player)
    {
      if (_playerMap.TryGetValue(player, out var character)) {
        // Despawn Player
        runner.Despawn(character);
      }

    }


    void SpawnGame(NetworkRunner runner, PlayerRef player)
    {
        var pos = UnityEngine.Random.insideUnitSphere;
        pos.x += -76;
        pos.y += 76;
        pos.z += -136;  

        var character = runner.Spawn(_playerPrefab, pos, Quaternion.identity, inputAuthority: player);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) {

      if (_playerMap.TryGetValue(player, out var character)) {
        // Despawn Player
        runner.Despawn(character);

        // Remove player from mapping
        _playerMap.Remove(player);

        Log.Info($"Despawn for Player: {player}");
      }

      if (_playerMap.Count == 0) {
        Log.Info("Last player left, shutdown...");

        // Shutdown Server after the last player leaves
        runner.Shutdown();
      }
    }

    public void OnConnectedToServer(NetworkRunner runner) { }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
    public void OnDisconnectedFromServer(NetworkRunner runner) { }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) {

      // Quit application after the Server Shutdown
      Application.Quit(0);
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
  }
}