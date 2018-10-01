// Bruno de Almeida Zampirom - 28/10/2018
// Última modificação 01/10/2018

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ranking : MonoBehaviour {

	Dictionary<string,Dictionary<string, string>> pontuacaoJogador;

    private void Start()
    {
        SetScore("Bruno", "Tempo", "25.50");
        SetScore("Bruno", "Data", "09/19/1997");

        SetScore("Jonas", "Tempo", "55.35");
        SetScore("Jonas", "Data", "19/12/1998");

        Init();
    }

    void Init()
    {
        if (pontuacaoJogador != null)
            return;
        pontuacaoJogador = new Dictionary<string, Dictionary<string, string>>();
    }

    public string GetScore(string nome, string tipo) // Retorna o tempo da volta
    {
        Init();
        if(!pontuacaoJogador.ContainsKey(nome) || !pontuacaoJogador[nome].ContainsKey(tipo))// Caso o usuário ou o tipo não exista 
        {
            return "0";
        }
        return pontuacaoJogador[nome][tipo];
    }

    public void SetScore(string nome, string tipo, string valor)
    {
        Init();
        if (!pontuacaoJogador.ContainsKey(nome))// Caso o usuário ou o tipo não exista 
        {
            pontuacaoJogador[nome] = new Dictionary<string, string>();
        }
        pontuacaoJogador[nome][tipo] = valor;
    }
    public string[] GetTodosNomes(string sortingtype) //Por qual tipo será organizada
    {
        Init();
        
        string [] nomes = pontuacaoJogador.Keys.ToArray();

        return nomes.OrderBy( n => GetScore(n, sortingtype)).ToArray();
    }

    public void SETTIME0()
    {
        SetScore("Gams", "Tempo", "00:30");
    }

    public void DEBUG_INITIAL_SETUP()
    {
        SetScore("Gams", "Tempo", "05:54");
        SetScore("Gams", "Data", "06/12/1998");

        SetScore("Jooj", "Tempo", "02:54");
        SetScore("Jooj", "Data", "15/12/2005");

        SetScore("Hokage", "Tempo", "04:54");
        SetScore("Hokage", "Data", "27/12/2009");
    }
}
