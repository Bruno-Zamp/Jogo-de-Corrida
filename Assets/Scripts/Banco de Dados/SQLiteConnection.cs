// Bruno de Almeida Zampirom - 23/09/2018

using UnityEngine;
// Imports necessários 
using System.Data;
using Mono.Data.SqliteClient;
using System;
using System.Collections;

public class SQLiteConnection : MonoBehaviour
{
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    private string dbfile = "URI=File:SQLiteDB.db";
    private DateTime momento;

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
            "tempo real, " +
            "momento real" +
            ");";
        command.CommandText = createTable;
        command.ExecuteNonQuery();
    }
    public void CreateAccount(string nome, string senha) //Executa as inserçoes do nome e senha de usuário
    {
        string insertValues = string.Format("INSERT INTO perfil(nome,senha,tempo,momento) VALUES('{0}','{1}',null,null);", nome, senha);
        command.CommandText = insertValues;
        command.ExecuteNonQuery();
    }
    public void AtualizaTempo(string user, float tempo) // Atualiza o tempo do usuário (o controle do menor tempo fica responsabilidade do script que chamar a função)
    {
        string updateTempo = string.Format("UPDATE perfil " +
                             "SET tempo = {0} , momento = julianday('now', 'localtime') " +
                             "WHERE nome = '{1}' ;", tempo, user);
        command.CommandText = updateTempo;
        command.ExecuteNonQuery();
        TempoUser(user);
    }
    public ArrayList SelectNomes() // Select de todos os nomes do banco
    {
        string select = "SELECT nome FROM perfil order by tempo;";
        command.CommandText = select;
        ArrayList listanomes = new ArrayList();

        reader = command.ExecuteReader();
        while (reader.Read())
        {
            string nome = reader.GetString(0);
            listanomes.Add(nome);
        }
        return listanomes;
    }
    public void Select()
    {
        string select = "SELECT nome, senha, tempo, datetime(momento) FROM perfil order by tempo;";
        command.CommandText = select;
        reader = command.ExecuteReader();
        while (reader.Read())
        {
            string nome = reader.GetString(0);
            string senha = reader.GetString(1);
            float tempo = reader.GetFloat(2);
            DateTime data = reader.GetDateTime(3);
            Debug.Log("Nome: " + nome +
                      "\n Senha: " + senha +
                      "\n Tempo: " + tempo +
                      "\n Data: " + data);
        }
    }
    public bool Select(string user, string senha) // Select de um usuário e senha específico (para verificar se usuário e senha estão corretos)  
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

    public bool UserNameExists(string user) // Verifica se este nome de usuário ja se encontra no BD
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
    public bool UserJaCorreu(string user) // Verifica se determinado usuário contém um tempo presente no BD
    {
        string select = string.Format("SELECT tempo FROM perfil WHERE nome = '{0}' ;", user); 
        command.CommandText = select;
        reader = command.ExecuteReader();
        reader.Read();
        if (reader.GetFloat(0)==0)
        {
            return false; // Não correu...
        }
        return true; // Já havia corrido
    }
    public float TempoUser(string user) // Busca o tempo de um usuário 
    {
        string select = string.Format("SELECT tempo FROM perfil WHERE nome = '{0}' ;", user);
        command.CommandText = select;
        reader = command.ExecuteReader();
        reader.Read();
        return reader.GetFloat(0); 
    }
    public DateTime DataUser(string user) // Busca a data em que o usuário postou o tempo no BD
    {
        string select = string.Format("SELECT datetime(momento) FROM perfil WHERE nome = '{0}' ;", user);
        command.CommandText = select;
        reader = command.ExecuteReader();
        reader.Read();
        return reader.GetDateTime(0);
    }
    public void DropTable() //Drop da Tabela
    {
        string dropTable = "DROP TABLE IF EXISTS perfil;";
        command.CommandText = dropTable;
        command.ExecuteNonQuery();
    }
}