// Bruno de Almeida Zampirom - 23/09/2018
// Refatorado 01/10/2018

using UnityEngine;

public class Menu : LoadLevel {

    public void LoadLevel(int index) // Índice da cena que será carregada
    {
        StartCoroutine(LoadAsynchronously(index));
    }
    public void Sair()
    {
        Application.Quit();
    }
    public void Ranking(GameObject Rkg)
    {
        Rkg.SetActive(!Rkg.active);   
    }
    public void Opcoes(GameObject Opc)
    {
        Opc.SetActive(!Opc.active);
    }
}
