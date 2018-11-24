using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.Rendering;

public class Configuracoes : MonoBehaviour
{

    [Space(20)]
    public Toggle CaixaModoJanela;
    public TMP_Dropdown Resolucoes, Qualidades, Vsync, API;
    [Space(20)]
    private int qualidadeGrafica, modoJanelaAtivo, resolucaoSalveIndex;
    private bool telaCheiaAtivada;
    private Resolution[] resolucoesSuportadas;
    private GraphicsDeviceType[] apis;

    void Awake()
    {
        resolucoesSuportadas = Screen.resolutions;
    }

    void Start()
    {
        //Ajustar dropbox
        ChecarResolucoes();
        AjustarQualidades();
        AjustarAPI();
        //
        if (PlayerPrefs.HasKey("RESOLUCAO"))
        {
            int numResoluc = PlayerPrefs.GetInt("RESOLUCAO");
            if (resolucoesSuportadas.Length <= numResoluc)
            {
                PlayerPrefs.DeleteKey("RESOLUCAO");
            }
        }
        if(PlayerPrefs.HasKey("api"))
        {
            int numapi = PlayerPrefs.GetInt("api");
            API.value = numapi;
        }
        //=============MODO JANELA===========//
        if (PlayerPrefs.HasKey("modoJanela"))
        {
            modoJanelaAtivo = PlayerPrefs.GetInt("modoJanela");
            if (modoJanelaAtivo == 1)
            {
                Screen.fullScreen = false;
                CaixaModoJanela.isOn = true;
            }
            else
            {
                Screen.fullScreen = true;
                CaixaModoJanela.isOn = false;
            }
        }
        else
        {
            modoJanelaAtivo = 0;
            PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
            CaixaModoJanela.isOn = false;
            Screen.fullScreen = true;
        }
        //========RESOLUCOES========//
        if (modoJanelaAtivo == 1)
        {
            telaCheiaAtivada = false;
        }
        else
        {
            telaCheiaAtivada = true;
        }
        if (PlayerPrefs.HasKey("RESOLUCAO"))
        {
            resolucaoSalveIndex = PlayerPrefs.GetInt("RESOLUCAO");
            Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
            Resolucoes.value = resolucaoSalveIndex;
        }
        else
        {
            resolucaoSalveIndex = (resolucoesSuportadas.Length - 1);
            Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, telaCheiaAtivada);
            PlayerPrefs.SetInt("RESOLUCAO", resolucaoSalveIndex);
            Resolucoes.value = resolucaoSalveIndex;
        }
        //=========QUALIDADES=========//
        if (PlayerPrefs.HasKey("qualidadeGrafica"))
        {
            qualidadeGrafica = PlayerPrefs.GetInt("qualidadeGrafica");
            QualitySettings.SetQualityLevel(qualidadeGrafica);
            Qualidades.value = qualidadeGrafica;
        }
        else
        {
            QualitySettings.SetQualityLevel((QualitySettings.names.Length - 1));
            qualidadeGrafica = (QualitySettings.names.Length - 1);
            PlayerPrefs.SetInt("qualidadeGrafica", qualidadeGrafica);
            Qualidades.value = qualidadeGrafica;
        }
    }
    //=========VOIDS DE CHECAGEM==========//
    private void ChecarResolucoes()
    {
        resolucoesSuportadas = Screen.resolutions;
        Resolucoes.options.Clear();
        for (int y = 0; y < resolucoesSuportadas.Length; y++)
        {
            Resolucoes.options.Add(new TMP_Dropdown.OptionData() { text = resolucoesSuportadas[y].width + "x" + resolucoesSuportadas[y].height +" "+resolucoesSuportadas[y].refreshRate+"Hz"});
        }
        Resolucoes.captionText.text = "Resolucao";
    }
    private void AjustarQualidades()
    {
        string[] nomes = QualitySettings.names;
        Qualidades.options.Clear();
        for (int y = 0; y < nomes.Length; y++)
        {
            Qualidades.options.Add(new TMP_Dropdown.OptionData() { text = nomes[y] });
        }
        Qualidades.captionText.text = "Qualidade";
    }
    private void AjustarAPI()
    {
# if UNITY_EDITOR
        apis = PlayerSettings.GetGraphicsAPIs(BuildTarget.StandaloneWindows64);
        API.ClearOptions();
        for(int i = 0; i< apis.Length; ++i)
        {
            API.options.Add(new TMP_Dropdown.OptionData() { text = apis[i].ToString()});
        }
        API.captionText.text = "APIs suportadas";
#endif
    }
    //=========VOIDS DE SALVAMENTO==========//
    public void SalvarPreferencias()
    {
        if (CaixaModoJanela.isOn == true)
        {
            modoJanelaAtivo = 0;
            telaCheiaAtivada = false;
        }
        else
        {
            modoJanelaAtivo = 1;
            telaCheiaAtivada = true;
        }
        PlayerPrefs.SetInt("api", API.value);
        PlayerPrefs.SetInt("qualidadeGrafica", Qualidades.value);
        PlayerPrefs.SetInt("modoJanela", modoJanelaAtivo);
        PlayerPrefs.SetInt("RESOLUCAO", Resolucoes.value);
        resolucaoSalveIndex = Resolucoes.value;
        AplicarPreferencias();
    }
    private void AplicarPreferencias()
    {
        //Setando como principal a Api escolida pelo usuário swap(nova APi principal, pela antiga)
#if UNITY_EDITOR
        object aux = apis.GetValue(0);
        apis.SetValue(apis.GetValue(API.value), 0);
        apis.SetValue(aux,API.value);

        //Aplicando
        PlayerSettings.SetGraphicsAPIs(BuildTarget.StandaloneWindows64, apis);
#endif
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("qualidadeGrafica"));
        Screen.SetResolution(resolucoesSuportadas[resolucaoSalveIndex].width, resolucoesSuportadas[resolucaoSalveIndex].height, !telaCheiaAtivada);
    }
}