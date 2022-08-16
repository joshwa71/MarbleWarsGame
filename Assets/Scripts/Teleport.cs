using UnityEngine;
using UnityEngine.UI;

public class Teleport : MonoBehaviour
{

    public Transform teleportTarget;
    public int score = 0;

    void OnTriggerEnter (Collider other) 
    {
        other.transform.position = teleportTarget.transform.position;

    }

}
