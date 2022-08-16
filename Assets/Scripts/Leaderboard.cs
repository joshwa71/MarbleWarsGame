using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Fusion;
using TMPro;

namespace Fusion.Sample.DedicatedServer {

public class Leaderboard : NetworkBehaviour
{
    // instantiate a list to hold all players

    public Dictionary<PlayerRef, NetworkObject> _playerMap;
    public GameObject _serverNetworkRunner;
    public NetworkObject _leaderboardCanvas;

    private NetworkRunner _runner;
    private List<NetworkObject> playerList = new List<NetworkObject>();   
    private TextMeshProUGUI tmpro = null;
    private StringBuilder sb = new StringBuilder();
    private bool _leaderboardSpawned;
    private NetworkObject  _leaderboard;
    private string _sb;

    private void Awake()
    {
        _leaderboardSpawned = false;
        _playerMap = _serverNetworkRunner.GetComponent<GameManager>()._playerMap; //s
        _runner = this.transform.GetComponent<NetworkRunner>();

    }


    public override void FixedUpdateNetwork()
     {
        if(_runner.CurrentScene == (int)SceneDefs.GAME) 
        {
            SpawnLeaderboard();
        }

        if(_playerMap.Count > 0 && _playerMap.Count != playerList.Count)
        {
            foreach(KeyValuePair<PlayerRef, NetworkObject> kvp in _playerMap)
            {
                Debug.Log("Playermap = " + _playerMap.Count);
                Debug.Log("In Loop");
                playerList.Add(kvp.Value);
                DoLeaderboard(kvp.Key, kvp.Value);
            }           
            Debug.Log("NetworkUpdate called");
        }
        if(_leaderboard != null){
        _leaderboard.GetComponent<LeaderboardHandler>()._leaderBoardString = _sb;

            Debug.Log("leaderboardstring  = " + _leaderboard.GetComponent<LeaderboardHandler>()._leaderBoardString);
        }

    }

    public void SpawnLeaderboard(){
        if(_leaderboard == null)
        {
            _leaderboard = _runner.Spawn(_leaderboardCanvas, Vector3.zero, Quaternion.identity, inputAuthority: null);
        }

    }

    public void DoLeaderboard(PlayerRef reference, NetworkObject player)
    {
        //sort the players by number of checkpoints passed (descending=most to least)
        playerList = playerList.OrderByDescending(x => x.GetComponent<NetworkStuff>().checkpoint_number).ToList();       
        Debug.Log("DoLeaderboard called");
        Debug.Log("Player list = " + playerList.Count);

        //compose the text list of cars
        for (int i = 0; i < playerList.Count; i++)
        {
            sb.AppendLine(string.Format("{0} {1}", i + 1, playerList[i].GetComponent<NetworkStuff>().playerNickNameTM.text));
            Debug.Log("String Builder = " +sb.ToString());
            _sb = sb.ToString();
        }   
        //
        //
    }
        
}
}