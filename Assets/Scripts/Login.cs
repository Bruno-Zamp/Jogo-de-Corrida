﻿using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class Login : MonoBehaviour {

    [SerializeField]
    private TMP_InputField User;
    [SerializeField]
    private TMP_InputField Senha;
    [SerializeField]
    private Toggle Lembrar;
    [SerializeField]
    private TextMeshProUGUI notification;
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private GameObject ThisPanel;

    SQLiteConnection bancoDeDados;

    void Start()
    {
        bancoDeDados = GameObject.FindObjectOfType<SQLiteConnection>();
        if (PlayerPrefs.HasKey("remember") && PlayerPrefs.GetInt("remember") == 1)
        {
            User.text = PlayerPrefs.GetString("rememberLogin");
            Senha.text = PlayerPrefs.GetString("rememberSenha");
            Lembrar.isOn = true;
        }
    }

    public void LoginJogo()
    {
        if (bancoDeDados.Select(User.text, Senha.text))
        {
            if (Lembrar.isOn) //Caso o usuário desejar lembrar dos seus dados
            {
                PlayerPrefs.SetInt("remember", 1);                      //Lembrando dados do usuário
                PlayerPrefs.SetString("rememberLogin", User.text);
                PlayerPrefs.SetString("rememberSenha", Senha.text);
            }
            /*else // Caso ele optou por lembrar anteriormente e não deseja mais lembrar
            {
                PlayerPrefs.DeleteKey("remember");
                PlayerPrefs.DeleteKey("rememberLogin");
                PlayerPrefs.DeleteKey("rememberSenha");
            }*/
            notification.CrossFadeAlpha(1, 0.1f, false);
            notification.color = Color.green;
            notification.text = "Login realizado com sucesso";

            StartCoroutine(Esperar(2));
        }
        else
        {
            notification.CrossFadeAlpha(1, 0.1f, false);
            notification.color = Color.red;
            notification.text = "Usuário ou Senha inválido";
        }     
    }

    private void ResetAlert()
    {
        notification.CrossFadeAlpha(0f, 0f, false);
    }
    
    public void CriarConta(GameObject CriarConta)
    {
        ResetAlert();
        CriarConta.SetActive(true);
    }

    IEnumerator Esperar(int tempo) // Caso o Login prossiga
    {
        yield return new WaitForSecondsRealtime(tempo); // Vai esperar determinado tempo para continuar
        Menu.SetActive(true);
        ThisPanel.SetActive(false);
        ResetAlert();
    }
}
