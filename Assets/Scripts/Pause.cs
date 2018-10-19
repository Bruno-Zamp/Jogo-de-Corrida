// Bruno de Almeida Zampirom - 16/10/2018

using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    [SerializeField]
    private GameObject panel;
 
    // Update is called once per frame
    void Update () {
        
	    if(Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.active);
            pausarEdespausar();
        }
	}
    public void Resume()
    {
        pausarEdespausar();
        panel.SetActive(false);
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
    private void pausarEdespausar()
    {
        if (Time.timeScale.Equals(0f))
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0f;
        }
    }
}
