using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;


public class LeaderboardHandler : NetworkBehaviour
{

    [Networked(OnChanged = nameof(OnLeaderboardChanged))]
    public NetworkString<_16> _leaderBoardString { get; set; }

    // Start is called before the first frame update
    private void Start()
    {
        _leaderBoardString = "Leaderboard";
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork() 
    {

    }

    static void OnLeaderboardChanged(Changed<LeaderboardHandler> changed)
    {   
        changed.Behaviour.OnLeaderboardChanged();
    }

    private void OnLeaderboardChanged(){
        this.transform.GetComponentInChildren<TextMeshProUGUI>().text = _leaderBoardString.ToString();
    }
}

