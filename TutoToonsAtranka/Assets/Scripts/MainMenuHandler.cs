using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int sceneIndex = i;
            levelButtons[i].onClick.AddListener(() => LoadScene(sceneIndex));
        }
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index+1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
