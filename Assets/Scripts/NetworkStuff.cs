using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using UnityEngine.UI;
using TMPro;
public class NetworkStuff : NetworkBehaviour
{
    public TextMeshPro playerNickNameTM;
    public static NetworkStuff Local { get; set; }
    public Transform playerModel;
    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_16> nickName { get; set; }
    private Checkpoints script_checkpoints;
    public int checkpoint_number = 0;


    // Start is called before the first frame update
    public override void Spawned()
    {
        if(Object.HasInputAuthority)
        {   
            Local = this;
            // Debug.Log("Spawned Player");
            // name = this.GetComponent<Text>().text;
            this.transform.GetChild(0).GetComponent<Camera>().enabled = true;
            this.transform.GetChild(1).GetComponent<Camera>().enabled = true;
            this.transform.GetChild(2).GetComponent<Camera>().enabled = true;
            this.transform.GetChild(3).GetComponent<Camera>().enabled = true;
            Debug.Log("Cameras Loaded");

            script_checkpoints = this.GetComponent<Checkpoints>();   
            
            RPC_SetNickName(PlayerPrefs.GetString("PlayerNickname"));
            playerNickNameTM.text = PlayerPrefs.GetString("PlayerNickname");
            Debug.Log($"RPC set nickname called");
        }

    }

    public override void FixedUpdateNetwork() {

    }

    static void OnNickNameChanged(Changed<NetworkStuff> changed)
    {
        Debug.Log($"{Time.time} nickName value changed : {changed.Behaviour.nickName}");

        changed.Behaviour.OnNickNameChanged();
    }

    private void OnNickNameChanged()
    {
        Debug.Log($"Nickname changed for player to {nickName} for player {gameObject.name}");

        playerNickNameTM.text = nickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        Debug.Log($"[RPC] SetNickName {nickName}");
        this.nickName = nickName;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (script_checkpoints != null)
        {
            if (other.CompareTag("Checkpoint") == true)
            {
                //count checkpoints passed
                Debug.Log(other.name);
                checkpoint_number = script_checkpoints.ExtractNumberFromString(other.name);
                Debug.Log("Player at checkpoint: " + checkpoint_number.ToString());
                
            }
        }     
    }
}
