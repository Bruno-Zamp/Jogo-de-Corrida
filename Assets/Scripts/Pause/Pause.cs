// Bruno de Almeida Zampirom - 16/10/2018

using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private GameObject configPanel;

    private void Awake()
    {
        Cursor.visible = false; // Seta a visibilidade do cursor do mouse
    }

    private void pausarEdespausar()
    {
        panel.SetActive(!panel.active);
        if (!panel.active) // Despausado
        {
            configPanel.SetActive(false);
            Time.timeScale = 1f;
            Cursor.visible = false;
            AudioListener.volume = 1;
        }
        else // Pausado
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            AudioListener.volume = 0; // Muta o jogo
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
        Cursor.visible = true;
        SceneManager.LoadSceneAsync(i);
    }
    public void Sair()
    {
        Application.Quit();
    }
}
