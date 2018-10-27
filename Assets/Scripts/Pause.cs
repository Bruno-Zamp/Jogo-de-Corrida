// Bruno de Almeida Zampirom - 16/10/2018

using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject configPanel;

    private void pausarEdespausar()
    {
        panel.SetActive(!panel.active);
        if (Time.timeScale.Equals(0f))
        {
            configPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    void Update () {
        
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            pausarEdespausar();
        }
	}
    public void Config()
    {
        configPanel.SetActive(!configPanel.active);
    }
    public void Resume()
    {
        pausarEdespausar();
    }
    public void Restart(int i)
    {
        pausarEdespausar();
        SceneManager.LoadSceneAsync(i);
    }
    public void Menu(int i)
    {
        pausarEdespausar();
        SceneManager.LoadSceneAsync(i);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
