// Bruno de Almeida Zampirom - 16/10/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCorrida : MonoBehaviour {
    [SerializeField]
    private TMPro.TextMeshProUGUI textoTempo;
	
    string Segundos(string s)
    {
        return (int.Parse(s) - (int.Parse(s)/60)*60).ToString("00");
    }
    string Minutos(string s)
    {
        return (int.Parse(s) / 60).ToString("00");
    }
    string Horas(string s)
    {

        return (int.Parse(s) / 3600).ToString("00");
    }
	// Update is called once per frame
	void Update () {
        string Tempo = Time.timeSinceLevelLoad.ToString("00");
        textoTempo.text = Horas(Tempo) +":"+Minutos(Tempo) +":"+Segundos(Tempo);
	}
}
