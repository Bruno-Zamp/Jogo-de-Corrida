// Bruno de Almeida Zampirom - 28/10/2018

using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CommitTempoCircuito : MonoBehaviour {

    public static bool PermiteUsuarioMover = false; // Variável que possibilita o usuário interagir com o jogo

    [SerializeField]
    private TextMeshProUGUI TempoAtual, TempoRecorde;
    [SerializeField]
    private GameObject PanelFinal,Tempo,Volta,Velocimetro;

    public int TotalDeVoltas = 8; //Volta final do circuito
    public float tempoDeEsperaInicial = 2.4f;

    private bool ativa = true; // Para ser chamado em apenas um frame após o final da corrida
    SQLiteConnection bancoDeDados;

    private void Start()
    {
        PermiteUsuarioMover = false; // Variável que possibilita o usuário interagir com o jogo

    bancoDeDados = FindObjectOfType<SQLiteConnection>();
    }
    // Update is called once per frame
    void Update () {
        TempoPossivel();

	    if(CarSystemCheckpoint.voltaAtual == TotalDeVoltas && ativa) // Caso o usuário terminou a corrida
        {
            ativa = PermiteUsuarioMover = false;
            Tempo.SetActive(false);
            Volta.SetActive(false);
            Velocimetro.SetActive(false);
            PanelFinal.SetActive(true);
            try
            {
                float tempoDeVolta = Time.timeSinceLevelLoad - tempoDeEsperaInicial;
                if ( tempoDeVolta < bancoDeDados.TempoUser(Login.UsuarioAtual))
                {
                    bancoDeDados.AtualizaTempo(Login.UsuarioAtual, tempoDeVolta);
                }
                TempoAtual.text = TempoVoltaCorrida.Formatar(tempoDeVolta);
                TempoRecorde.text = TempoVoltaCorrida.Formatar(bancoDeDados.TempoUser(Login.UsuarioAtual)) + "   " + bancoDeDados.DataUser(Login.UsuarioAtual).ToString("dd/MM/yyyy");
            }
            catch(Exception e)
            {
                Debug.LogError("Erro na conexão com Banco de Dados");
                Debug.LogException(e);
            }
        }
	}
    void TempoPossivel() //Testa se o usuário ja esperou o tempo inicial
    {
        if (Time.timeSinceLevelLoad > tempoDeEsperaInicial)
            PermiteUsuarioMover = true;
    }
    public void reiniciar(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }
    public void menu(int i)
    {
        SceneManager.LoadSceneAsync(i);
    }
}
