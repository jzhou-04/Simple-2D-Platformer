using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.Gameplay
{
    public class LevelLoader : MonoBehaviour
    {
        public void LoadLevel(int levelIndex)
        {
            if (levelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(levelIndex);
            }
        }

        public void LoadLevelSelect()
        {
            SceneManager.LoadScene("Level Select");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
