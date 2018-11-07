// Bruno de Almeida Zampirom - 08/10/2018

using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;

public class CriarConta : MonoBehaviour {

    [SerializeField]
    private TMP_InputField user; //Campo de texto do usuário
    [SerializeField]
    private TMP_InputField senha; //Campo de texto da senha
    [SerializeField]
    private TMP_InputField confirma; //Campo de texto da confirmaçao da senha

    [SerializeField]
    private TextMeshProUGUI userNotification; //Texto de notificação do usuario
    [SerializeField]
    private TextMeshProUGUI senhaNotification; //Texto de notificação da senha
    [SerializeField]
    private TextMeshProUGUI confirmaNotification; //Texto de notificação da confirmação senha

    [SerializeField]
    private TextMeshProUGUI sucesso; //Texto de notificação de sucesso na criação

    [SerializeField]
    private GameObject Login; //Panel Login
    [SerializeField]
    private GameObject ThisPanel; // Este Panel 'CriaConta'
    [SerializeField]
    private GameObject Canvas; // Canvas 

    private SQLiteConnection bancoDeDados;

    void Start () {
        bancoDeDados = GameObject.FindObjectOfType<SQLiteConnection>();
    }

    public void Criar()
    {
        bool erro=false;
        // Testa se o usuário inseriu um nome
        if(user.text.Length==0)
        {
            erro = true;
            userNotification.CrossFadeAlpha(1, 0.2f, false);
            userNotification.SetText("Você deve inserir um nome!");
        }
        else
        {
            // Testa se o usuário existe
            if (bancoDeDados.UserNameExists(user.text)) //Pesquisa no Banco de Dados se esse nome já existe... Você deve inserir um nome!
            {
                erro = true;
                userNotification.CrossFadeAlpha(1, 0.2f, false);
                userNotification.SetText("Este nome de usuário já existe");
            }
            else
            {
                userNotification.CrossFadeAlpha(0, 0.2f, false);
            }
        }

        // Testa se a senha contém ao menos 8 caracteres
        if(senha.text.Length<8)
        {
            erro = true;
            senhaNotification.CrossFadeAlpha(1, 0.2f, false);
            senhaNotification.text = "A senha deve conter ao menos 8 caracteres";

        }
        else
        {
            senhaNotification.CrossFadeAlpha(0, 0.2f, false);

            // Testa se as duas senhas são iguais
            if (senha.text != confirma.text)
            {
                erro = true;
                confirmaNotification.CrossFadeAlpha(1, 0.2f, false);
                confirmaNotification.text = "As duas senhas devem ser iguais";
            }
            else
            {
                senhaNotification.CrossFadeAlpha(0, 0.2f, false);
            }
        }

        if(!erro)
        {
            try
            {
                bancoDeDados.CreateAccount(user.text, senha.text); // Insere o usuário e senha do BD
                Debug.Log("Usuário inserido com sucesso!");
                sucesso.CrossFadeAlpha(1, 0.2f, true);
                sucesso.SetText("Sua conta foi criada com sucesso");
                StartCoroutine(Esperar(1)); // Esperar 3 segundos para voltar para tela de login

            }
            catch(Exception ex)
            {
                Debug.LogError("Erro ao inserir no banco de dados: "+ ex.Message);
            }
        }
        else
        {
            Debug.Log("erro");
        }
    }

    public void Voltar()
    {
        ThisPanel.SetActive(false);
        ResetAlert();
    }

    private void ResetAlert()
    {
        userNotification.CrossFadeAlpha(0f, 0f, false);
        senhaNotification.CrossFadeAlpha(0f, 0f, false);
        confirmaNotification.CrossFadeAlpha(0f, 0f, false);
        sucesso.CrossFadeAlpha(0f, 0f, false);
    }

    IEnumerator Esperar(int tempo)
    {
        yield return new WaitForSecondsRealtime(tempo);
        ThisPanel.SetActive(false);
        ResetAlert();
    }
}
