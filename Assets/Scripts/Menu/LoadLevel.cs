// Bruno de Almeida Zampirom - 23/09/2018

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject LoadingScene;
    public Slider slider;
    public TextMeshProUGUI textoprct;

    protected IEnumerator LoadAsynchronously(int sceneIndex)
    {
        LoadingScene.SetActive(true);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex); // Escolhe o modo de carregamento assíncrono setando a cena escolhida
        while(!operation.isDone) 
        {
            float progress = Mathf.Clamp( operation.progress / 0.9f, 0, 1); // Matemática aplicada para contornar o modo de carregamento do Unity que é realizado em dois estágios, o primeiro vai de 0 até 0.9 e segundo de 0.9 até 1, o que deve ser levado em consideração é o primeiro estágio, que carrega os objetos da cena em sí, ent
            slider.value = progress;
            textoprct.text = (progress * 100).ToString("0") + " %";
            yield return null;
        }
    }
}
