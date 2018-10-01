// Bruno de Almeida Zampirom - 23/09/2018

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Imports necessários 
using System.Data;
using Mono.Data.SqliteClient;
using UnityEngine.UI; // Obrigatório ao usar elementos da interface do usuário. 

public class SQLiteConnection : MonoBehaviour
{
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string dbfile = "URI=File:SQLiteDB.db";
    public InputField caixaDeTexto; 

    // Use this for initialization
    void Start()
    {
        Connection();
    }
    private void Connection() //Inicia a concexão e cria a tabela (caso ainda não existir...)
    {
        connection = new SqliteConnection(dbfile);
        command = connection.CreateCommand();
        connection.Open();

        string createTable = "CREATE TABLE IF NOT EXISTS perfil(" +
            "nome VARCHAR(50) PRIMARY KEY, " +
            "tempo INTEGER, " +
            "momento real" +
            ");";
        command.CommandText = createTable;
        command.ExecuteNonQuery();
    }
    public void Insert() //Executa as inserçoes dos nomes de usuário 
    {
        string insertValues = "INSERT INTO perfil(nome,tempo,momento) VALUES('" + caixaDeTexto.text + "', 0, julianday('now', 'localtime'));";
        command.CommandText = insertValues;
        command.ExecuteNonQuery();
    }
    public void AtualizaTempo()
    {
        string updateTempo = "UPDATE perfil " +
            "SET tempo = 0, momento = julianday('now', 'localtime') " +
            "WHERE nome = Bruno";
        command.CommandText = updateTempo;
        command.ExecuteNonQuery();
    }
    public void Select()
    {
        string select = "SELECT nome, tempo, time(momento) FROM perfil;";
        command.CommandText = select;

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            string nome = reader.GetString(0);
            int tempo = reader.GetInt32(1);
            System.DateTime momento = reader.GetDateTime(2);
            Debug.Log("nome= " + nome + 
                      "tempo= "+ tempo+
                      "momento= "+ momento.ToString());
        }
    }
    public void DropTable()
    {
        string dropTable = "DROP TABLE IF EXISTS perfil;";

        command.CommandText = dropTable;
        command.ExecuteNonQuery();
    }
}