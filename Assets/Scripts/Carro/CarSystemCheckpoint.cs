// Bruno de Almeida Zampirom - 28/10/2018

using UnityEngine;

public class CarSystemCheckpoint : MonoBehaviour {
    
    //Lista de checkpoints do circuito 
    public Transform[] checkpointList;
    //Variáveis estáticas do veículo 
    public static int checkpointAtual = 0;
    public static int voltaAtual = 0;

    //Caso seja reiniciado, reseta as variáveis
    private void Start()
    {
        checkpointAtual = voltaAtual= 0;
    }
}
