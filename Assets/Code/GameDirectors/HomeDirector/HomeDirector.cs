using System;
using Game.Systems.GameStateSystem.Director;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.GameDirectors.HomeDirector
{
    public class HomeDirector : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;

        [SerializeField]
        private Button resetGameButton;

        [SerializeField]
        private GameStateDirector gameStateDirector;

        #region Public Methods

        private void Start()
        {
            playButton.onClick.AddListener(() => SceneManager.LoadScene(1));
            resetGameButton.onClick.AddListener(() => gameStateDirector.ResetLevel());
        }

        #endregion
    }
}