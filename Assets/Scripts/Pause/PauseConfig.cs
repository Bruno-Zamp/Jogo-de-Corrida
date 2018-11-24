// Bruno de Almeida Zampirom - 24/10/2018

using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class PauseConfig : MonoBehaviour {
    [SerializeField]
    private TMP_Dropdown Resolucoes, Qualidades, PostProcessingDropdown, Vsync;
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
        PostProcessingDropdown.value = postProcessingSet(); //Setando a config atual 
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

        //Atualizar Dropdown das qualidades gráficas
        Vsync.options.Clear();
        Vsync.options.Add(new TMP_Dropdown.OptionData() { text = "Desligado"});
        Vsync.options.Add(new TMP_Dropdown.OptionData() { text = "Meio (30fps)" });
        Vsync.options.Add(new TMP_Dropdown.OptionData() { text = "Cheio (60fps)" });
        Vsync.value = QualitySettings.vSyncCount;
        Vsync.captionText.text = "Vsync";
        
        // Atualizar Dropdown do Pós processamento
        PostProcessingDropdown.options.Clear();
        PostProcessingDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Nada" });
        PostProcessingDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Mínimo"});
        PostProcessingDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Médio" });
        PostProcessingDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Alto" });
        PostProcessingDropdown.options.Add(new TMP_Dropdown.OptionData() { text = "Máximo" });
        PostProcessingDropdown.captionText.text = "Pós Processamento";
    }
	public void Salvar() // Aplica configurações selecionadas pelo usuário
    {
        QualitySettings.SetQualityLevel(Qualidades.value);
        QualitySettings.vSyncCount = Vsync.value;
        Screen.SetResolution(resolucoesSuportadas[Resolucoes.value].width, resolucoesSuportadas[Resolucoes.value].height, telaCheia.isOn);
        postProcessingConfig(PostProcessingDropdown.value); 
        PostProcessingDropdown.value = postProcessingSet(); //Setando a config atual 
    }
    public void Voltar()
    {
        Config.SetActive(false);
    }
    // Configurar pós processamento
    private void postProcessingConfig(int n) // Altera o valor do pós processamento, informando um valor de 1-4 quanto mais alto o número mais filtros aplicados...
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
            postProcessing.motionBlur.enabled = false;
            postProcessing.eyeAdaptation.enabled = false;
            postProcessing.bloom.enabled = false;
            postProcessing.chromaticAberration.enabled = true;

        }
        else if (n == 3) // Configuração com pós processamento alto 
        {
            postProcessing.fog.enabled = true;
            postProcessing.antialiasing.enabled = true;
            postProcessing.ambientOcclusion.enabled = true;
            postProcessing.screenSpaceReflection.enabled = true;
            postProcessing.depthOfField.enabled = false;
            postProcessing.motionBlur.enabled = true;
            postProcessing.eyeAdaptation.enabled = false;
            postProcessing.bloom.enabled = false;
            postProcessing.chromaticAberration.enabled = true;

        }
        else if (n == 4) // Configuração com pós processamento máximo 
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
        if (postProcessing.bloom.enabled) // Única configuração que consta com Bloom é a máxima, então retorna (4)"máxima"
        {
            return 4;
        }
        else if(postProcessing.motionBlur.enabled) // Caso não for máxima e tiver motion blur , então retorna (3)"alta"
        {
            return 3;
        }
        else if (postProcessing.antialiasing.enabled) // Caso não for alta e tiver antialiasing , então retorna (2)"médio"
        {
            return 2;
        }
        else if(postProcessing.fog.enabled) // Caso não for médio e tiver fog , então retorna (1)"mínimo" 
        {
            return 1;
        }
        else
        {
            return 0; // Caso não tiver nada retorna 0, "nada"
        }
    }
}
