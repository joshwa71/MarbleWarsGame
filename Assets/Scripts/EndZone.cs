using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndZone : MonoBehaviour
{
    public bool ended = false;

    private void OnTriggerEnter(Collider other){
        ended = true;
        Debug.Log("Ended!!");
    }
}
