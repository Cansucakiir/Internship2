using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
   [SerializeField] private GameObject controlsPanel;
   public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
    public void ControlsButton()
    {
        controlsPanel.SetActive(true);
    }
    public void BackButton()
    {
        controlsPanel.SetActive(false);
    }
}
