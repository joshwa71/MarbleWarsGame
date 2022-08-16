using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> chk = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (this.transform.GetChild(i).CompareTag("Checkpoint") == true)
                chk.Add(this.transform.GetChild(i).gameObject);
        }
    }
    public int ExtractNumberFromString(string s1)
    {
        //  chk (10) -> 10 -> int 10
        return System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(s1, "[^0-9]", ""));
    }
}