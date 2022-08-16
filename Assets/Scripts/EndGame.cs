using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Fusion;
using System;
using UnityEngine.SceneManagement;


namespace Fusion.Sample.DedicatedServer {
    public class EndGame : SimulationBehaviour {

        [SerializeField] GameObject EndZoneCube;
        private bool _endzone;

        void Update() {

            _endzone = EndZoneCube.GetComponent<EndZone>().ended;

            if(_endzone){
                //Runner.Shutdown();
            //}

            //if (Runner == null) {
                SceneManager.LoadScene((byte)SceneDefs.END);
            }
        }  
    }
}