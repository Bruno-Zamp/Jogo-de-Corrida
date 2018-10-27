using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class PauseConfig : MonoBehaviour {
    [SerializeField]
    private TMP_Dropdown Resolucoes, Qualidades, PostProcessing;
    [Space(20)]
    [SerializeField]
    Toggle telaCheia;
    private Resolution[] resolucoesSuportadas;
    [SerializeField]
    private GameObject Config;
    [SerializeField]
    private PostProcessingProfile postProcessing;

    // Use this for initialization
    void Start () {
        InserirValoresDropdown();
    }
	void InserirValoresDropdown()
    {
        //Atualizar Dropdown das resoluções
        resolucoesSuportadas = Screen.resolutions;
        Resolucoes.options.Clear();
        for (int y = 0; y < resolucoesSuportadas.Length; y++)
        {
            Resolucoes.options.Add(new TMP_Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height });
            if(resolucoesSuportadas[y].ToString() == Screen.currentResolution.ToString())
            {
                Resolucoes.value = y;
            }
        }
        Resolucoes.captionText.text = "Resolução";

        //Atualizar Dropdown das qualidades gráficas
        string[] nomes = QualitySettings.names;
        Qualidades.options.Clear();
        for (int y = 0; y < nomes.Length; y++)
        {
            Qualidades.options.Add(new TMP_Dropdown.OptionData() { text = nomes[y] });
        }
        Qualidades.value = QualitySettings.GetQualityLevel();
        Qualidades.captionText.text = "Qualidade";

        // Atualizar Dropdown do Pós processamento
        PostProcessing.options.Clear();
        PostProcessing.options.Add(new TMP_Dropdown.OptionData() { text = "Nada" });
        PostProcessing.options.Add(new TMP_Dropdown.OptionData() { text = "Mínimo"});
        PostProcessing.options.Add(new TMP_Dropdown.OptionData() { text = "Médio" });
        PostProcessing.options.Add(new TMP_Dropdown.OptionData() { text = "Máximo" });
        PostProcessing.captionText.text = "Pós Processamento";
    }
	public void Salvar() // Aplica configurações selecionadas pelo usuário
    {
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualidadeGrafica"));
        Screen.SetResolution(resolucoesSuportadas[Resolucoes.value].width, resolucoesSuportadas[Resolucoes.value].height, !telaCheia);
        postProcessingConfig(PostProcessing.value); 
        PostProcessing.value = postProcessingSet(); //Setando a config atual 
    }
    public void Voltar()
    {
        Config.SetActive(false);
    }
    // Configurar pós processamento
    private void postProcessingConfig(int n)
    {
        if (n == 0) // Configuração sem Pós processamento
        {
            postProcessing.fog.enabled = false;
            postProcessing.antialiasing.enabled = false;
            postProcessing.ambientOcclusion.enabled = false;
            postProcessing.screenSpaceReflection.enabled = false;
            postProcessing.depthOfField.enabled = false;
            postProcessing.motionBlur.enabled = false;
            postProcessing.eyeAdaptation.enabled = false;
            postProcessing.bloom.enabled = false;
            postProcessing.chromaticAberration.enabled = false;
        }
        else if(n == 1) // Configuração com pós processamento mínimo
        {
            postProcessing.fog.enabled = true;
            postProcessing.antialiasing.enabled = false;
            postProcessing.ambientOcclusion.enabled = false;
            postProcessing.screenSpaceReflection.enabled = true;
            postProcessing.depthOfField.enabled = false;
            postProcessing.motionBlur.enabled = false;
            postProcessing.eyeAdaptation.enabled = false;
            postProcessing.bloom.enabled = false;
            postProcessing.chromaticAberration.enabled = false;

        }
        else if (n == 2) // Configuração com pós processamento médio 
        {
            postProcessing.fog.enabled = true;
            postProcessing.antialiasing.enabled = true;
            postProcessing.ambientOcclusion.enabled = false;
            postProcessing.screenSpaceReflection.enabled = true;
            postProcessing.depthOfField.enabled = false;
            postProcessing.motionBlur.enabled = true;
            postProcessing.eyeAdaptation.enabled = false;
            postProcessing.bloom.enabled = false;
            postProcessing.chromaticAberration.enabled = true;

        }
        else if (n == 3) // Configuração com pós processamento máximo 
        {
            postProcessing.fog.enabled = true;
            postProcessing.antialiasing.enabled = true;
            postProcessing.ambientOcclusion.enabled = true;
            postProcessing.screenSpaceReflection.enabled = true;
            postProcessing.depthOfField.enabled = true;
            postProcessing.motionBlur.enabled = true;
            postProcessing.eyeAdaptation.enabled = true;
            postProcessing.bloom.enabled = true;
            postProcessing.chromaticAberration.enabled = true;

        }
    }
    private int postProcessingSet()
    {
        if (postProcessing.bloom.enabled) // Única configuração que consta com Bloom é a máxima, então retorna (3)"máxima"
        {
            return 3;
        }
        else if(postProcessing.antialiasing.enabled)
        {
            return 2;
        }
        else if(postProcessing.fog.enabled)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
