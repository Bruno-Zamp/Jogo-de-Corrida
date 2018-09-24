// Bruno de Almeida Zampirom - 23/09/2018

using System.Collections;
using System.Collections.Generic;
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
    public void Opcoes(GameObject Opc)
    {
        Opc.SetActive(true);
    }
    public void Voltar(GameObject Opc)
    {
        Opc.SetActive(false);
    }
}
