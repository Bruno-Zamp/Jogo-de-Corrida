// Bruno de Almeida Zampirom - 16/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoVoltaCorrida : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI textoTempo,textoVolta,textoCheckpoint;
    [SerializeField]
    private Animation lastLap;
    CommitTempoCircuito CommitTempo;

    bool auxlastLap = true; // Variável auxiliar, para que a animação 'lastLap' seja executada apenas uma vez
    private void Start()
    {
        auxlastLap = true;
        CommitTempo = FindObjectOfType<CommitTempoCircuito>();
    }
    static string Segundos(string s)
    {
        return (int.Parse(s) - (int.Parse(s)/60)*60).ToString("00");
    }
    static string Minutos(string s)
    {
        return (int.Parse(s) / 60).ToString("00");
    }
    static string Horas(string s)
    {
        return (int.Parse(s) / 3600).ToString("00");
    }
    public static string Formatar(float tempo)
    {
        string Tempo = (tempo).ToString("00"); 
        return  Horas(Tempo) + ":" + Minutos(Tempo) + ":" + Segundos(Tempo);
    }
	// Update is called once per frame
	void FixedUpdate () {
        // Configura Tempo
        textoTempo.text = Formatar(Time.timeSinceLevelLoad -2.4f); 
        // Configura Volta
        int aux = CarSystemCheckpoint.voltaAtual;
        textoVolta.text = aux.ToString() + "/" + CommitTempo.TotalDeVoltas.ToString();
        if(aux == CommitTempo.TotalDeVoltas && auxlastLap)
        {
            lastLap.Play();
            auxlastLap = false;
        }
        aux = CarSystemCheckpoint.checkpointAtual; // Teste de checkpoints. Usado pra debug
        textoCheckpoint.text = aux.ToString() + "/6";
    }
}
