using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.SceneManagement;

 


namespace Fusion.Sample.DedicatedServer {    
    
    public class Loader : NetworkSceneManagerBase
    {
        public enum SceneIndex {
            LAUNCH,
            MENU,
            GAME,
            LOBBY
        };

         [SerializeField] private GameObject _loadScreen;

        // [Header("Scenes")]
        // [SerializeField] private SceneReference _launch;
        // [SerializeField] private SceneReference _menu;
        // [SerializeField] private SceneReference _game;
        // [SerializeField] private SceneReference _lobby;        
        

        private void Awake()
        {
            _loadScreen.SetActive(false);
        }

        protected override IEnumerator SwitchScene(SceneRef prevScene, SceneRef newScene, FinishedLoadingDelegate finished)
        {
            Debug.Log($"Switching Scene from {prevScene} to {newScene}");

            _loadScreen.SetActive(true);
                
            List<NetworkObject> sceneObjects = new List<NetworkObject>();

            int index;
            switch ((SceneIndex)(int)newScene)
            {
                case SceneIndex.LAUNCH: index = (int)SceneDefs.LAUNCH; break;
                case SceneIndex.MENU: index = (int)SceneDefs.MENU; break;
                case SceneIndex.GAME: index = (int)SceneDefs.GAME; break;
                case SceneIndex.LOBBY: index = (int)SceneDefs.LOBBY; break;
                default: index = (int)SceneDefs.MENU; break;
            }	
            yield return SceneManager.LoadSceneAsync(index, LoadSceneMode.Single);
            var loadedScene = SceneManager.GetSceneByBuildIndex( index );
            Debug.Log($"Loaded scene {index}: {loadedScene}");
            sceneObjects = FindNetworkObjects(loadedScene, disable: false);

            // Delay one frame
            yield return null;
            finished(sceneObjects);

            Debug.Log($"Switched Scene from {prevScene} to {newScene} - loaded {sceneObjects.Count} scene objects");

            _loadScreen.SetActive(false);
        }
    }
}