using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace cc.tools
{
    public enum command
    {
        START,
        ABOUT,
        EXIT,
        NONE
    }
    public class MainMenuControl : MonoBehaviour
    {
        [SerializeField] int sceneIndex = 1;
        [SerializeField] GameObject aboutPanel;
        [HideInInspector]
        public Draggable Selection;
        [HideInInspector]
        public command type = command.NONE;

        private void Start()
        {
            aboutPanel.SetActive(false);
        }
        public void LoadLevel()
        {
            SceneManager.LoadScene(sceneIndex);
        }
        public void Close()
        {
            Application.Quit();
        }

        



    }

}
