// Bruno de Almeida Zampirom - 28/10/2018
// Última modificação 01/10/2018

using UnityEngine;

public class PlayerScoreScript : MonoBehaviour {

    public GameObject playerScoreEntryPrefab;

    Ranking ranking;

	// Use this for initialization
	void Start () {
 
        ranking = GameObject.FindObjectOfType<Ranking>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (ranking == null) 
        {
            return;
        }

        while(this.transform.childCount > 0) //Remove todos os objetos 'old' para após inserir os valores novos 
        {
            Transform c = this.transform.GetChild(0);
            c.SetParent(null);
            Destroy(c.gameObject);
        }

        string[] nomes = ranking.GetTodosNomes("Tempo"); // Recebe todos os nomes registrados

        foreach (string nome in nomes) // Percorre o array criando novos prefabs e alterando os valores pré-definidos com os valores do array...
        {
            GameObject go = (GameObject)Instantiate(playerScoreEntryPrefab);
            go.transform.SetParent(this.transform, false);
            go.transform.Find("Nome").GetComponent<TMPro.TextMeshProUGUI>().text = nome;
            go.transform.Find("Tempo").GetComponent<TMPro.TextMeshProUGUI>().text = ranking.GetScore(nome,"Tempo");
            go.transform.Find("Data").GetComponent<TMPro.TextMeshProUGUI>().text = ranking.GetScore(nome, "Data"); ;
        }
    }
}
