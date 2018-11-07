// Bruno de Almeida Zampirom - 28/09/2018
// Última modificação 01/10/2018

using System;
using System.Collections;
using UnityEngine;

public class PlayerScoreScript : MonoBehaviour
{

    public GameObject playerScoreEntryPrefab;

    SQLiteConnection bancoDeDados;

    // Use this for initialization
    void Start()
    {
        bancoDeDados = FindObjectOfType<SQLiteConnection>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        while (this.transform.childCount > 0) //Remove todos os objetos 'old' para após inserir os valores novos 
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }
        try
        {
            ArrayList listaNomes = bancoDeDados.SelectNomes();

            foreach (string nome in listaNomes) // Percorre o array criando novos prefabs e alterando os valores pré-definidos com os valores do array...
            {
                if (bancoDeDados.UserJaCorreu(nome))
                {
                    GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
                    go.transform.SetParent(this.transform, false);
                    go.transform.Find("Nome").GetComponent<TMPro.TextMeshProUGUI>().text = nome;
                    go.transform.Find("Tempo").GetComponent<TMPro.TextMeshProUGUI>().text = TempoVoltaCorrida.Formatar(bancoDeDados.TempoUser(nome));
                    go.transform.Find("Data").GetComponent<TMPro.TextMeshProUGUI>().text = bancoDeDados.DataUser(nome).ToString("dd/MM/yyyy");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
}
