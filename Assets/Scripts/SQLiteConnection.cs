// Bruno de Almeida Zampirom - 23/09/2018

using UnityEngine;
// Imports necessários 
using System.Data;
using Mono.Data.SqliteClient;

public class SQLiteConnection : MonoBehaviour
{
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string dbfile = "URI=File:SQLiteDB.db";

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
            "nome VARCHAR(26) NOT NULL PRIMARY KEY, " +
            "senha VARCHAR(26) NOT NULL," +
            "tempo INTEGER, " +
            "momento real" +
            ");";
        command.CommandText = createTable;
        command.ExecuteNonQuery();
    }
    public void CreateAccount(string nome, string senha) //Executa as inserçoes do nome e senha de usuário
    {

        string insertValues = string.Format("INSERT INTO perfil(nome,senha,tempo,momento) VALUES('{0}','{1}',null,null);",nome,senha);
        command.CommandText = insertValues;
        command.ExecuteNonQuery();
    }
    public void AtualizaTempo(string user, string tempo)
    {
        string updateTempo = string.Format("UPDATE perfil " +
                             "SET tempo = {0}, momento = julianday('now', 'localtime') " +
                             "WHERE nome = {1}",user,tempo);
        command.CommandText = updateTempo;
        command.ExecuteNonQuery();
    }
    public void Select() // Select de todos 
    {
        string select = "SELECT nome, senha, tempo, time(momento) FROM perfil;";
        command.CommandText = select;

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            string nome = reader.GetString(0);
            string senha = reader.GetString(1);
            int tempo = reader.GetInt32(2);
            System.DateTime momento = reader.GetDateTime(3);
            Debug.Log("nome= " + nome + 
                      "\ntempo= "+ tempo+
                      "\nsenha= "+ senha +
                      "\nmomento= "+ momento.ToString());
        }
    }
    public bool Select(string user, string senha) // Select de um usuário e senha específico  
    {
        string select = string.Format("SELECT 1 FROM perfil WHERE nome = '{0}' AND senha = '{1}' ;", user, senha);
        command.CommandText = select;
        reader = command.ExecuteReader();
        if (reader.Read()) 
        {
            return true; // Existe essa combinação registrada no banco
        }
        return false; // Não existe essa combinação . . .
    }

    public bool UserNameExists(string user)
    {
        string select = string.Format("SELECT 1 FROM perfil WHERE nome = '{0}';", user);
        command.CommandText = select;
        reader = command.ExecuteReader();
        if (reader.Read())
        {
            return true; // Existe esse username no Banco
        }
        return false; // Não existe esse username . . .
    }

    public void DropTable() //Drop da Tabela
    {
        string dropTable = "DROP TABLE IF EXISTS perfil;";
        command.CommandText = dropTable;
        command.ExecuteNonQuery();
    }
}