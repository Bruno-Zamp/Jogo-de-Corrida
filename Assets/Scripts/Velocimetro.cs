// Bruno de Almeida Zampirom - 15/10/2018

using UnityEngine;

public class Velocimetro : MonoBehaviour {

    [SerializeField]
    private Rigidbody Carro;
    [SerializeField]
    TMPro.TextMeshProUGUI Texto;

    private float anguloMin = 119f;
    private float anguloMax = -121f;
    static Velocimetro velocimetro;

    // Use this for initialization
    void Start () {
        velocimetro = this;
	}

    private void Update()
    {
        ShowSpeed(Carro.velocity.magnitude);
        Texto.text = (Carro.velocity.magnitude * 3.6f).ToString("N0")+" KM/H";
    }

    public void ShowSpeed(float speed)
    {
        float ang = Mathf.Lerp(anguloMin, anguloMax, Mathf.InverseLerp(0,100, speed));
        velocimetro.transform.eulerAngles = new Vector3(0, 0, ang);
    }
}
