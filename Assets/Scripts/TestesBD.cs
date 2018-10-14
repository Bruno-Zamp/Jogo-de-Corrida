// Bruno de Almeida Zampirom - 08/10/2018

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestesBD : MonoBehaviour {
    private SQLiteConnection bancoDeDados;

    // Use this for initialization
    void Start () {
        bancoDeDados = GameObject.FindObjectOfType<SQLiteConnection>();
    }
	public void SelectALL()
    {
        try
        {
            bancoDeDados.Select();
            Debug.Log("Valores Buscados com sucesso");
        }
        catch(Exception ex)
        {
            Debug.LogError("Erro ao BUSCAR valores: "+ ex.Message);
        }
    }
    public void Deletar()
    {
        try
        {
            bancoDeDados.DropTable();
            Debug.Log("Tabela deletada com sucesso");
        }
        catch (Exception ex)
        {
            Debug.LogError("Erro ao DELETAR tabela: " + ex.Message);
        }
    }
}
