// Bruno de Almeida Zampirom - 23/09/2018
// Refatorado 01/10/2018

using UnityEngine;

public class Menu : LoadLevel {

    [SerializeField]
    private GameObject menu;

    public void LoadLevel(int index) // Índice da cena que será carregada
    {
        StartCoroutine(LoadAsynchronously(index));
        menu.SetActive(false);
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void Ranking(GameObject Rkg)
    {
        Rkg.SetActive(!Rkg.active);
        menu.SetActive(!menu.active);
    }
    public void Opcoes(GameObject Opc)
    {
        Opc.SetActive(!Opc.active);
        menu.SetActive(!menu.active);
    }
    public void Logout(GameObject Log)
    {
        Log.SetActive(true);
        menu.SetActive(false);
    }
}
