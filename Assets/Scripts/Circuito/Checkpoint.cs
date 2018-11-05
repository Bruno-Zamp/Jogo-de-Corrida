// Bruno de Almeida Zampirom - 28/10/2018

using UnityEngine;

public class Checkpoint : MonoBehaviour {
    //static Transform PlayerS;
	// Use this for initialization
	void Start () {
        //PlayerS = GameObject.FindGameObjectWithTag("Player").transform;
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") // Se for o player que colidiu com o checkpoint
        {
            if (transform == other.GetComponent<CarSystemCheckpoint>().checkpointList[CarSystemCheckpoint.checkpointAtual].transform) // Se este checkpoint for o "checkpointAtual" do circuito
            {
                if(CarSystemCheckpoint.checkpointAtual+1 < other.GetComponent<CarSystemCheckpoint>().checkpointList.Length)
                {
                    if(CarSystemCheckpoint.checkpointAtual == 0) // Caso o checkpoint for 0 acrescenta uma volta 
                    {
                        CarSystemCheckpoint.voltaAtual++; 
                    }
                    //Seta para o próximo checkpoint
                    CarSystemCheckpoint.checkpointAtual++; 
                }
                else
                {
                    CarSystemCheckpoint.checkpointAtual = 0; //Checkpoint volta a ser zero
                }
            }
        }
    }
}
