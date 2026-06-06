using System.Collections;
using UnityEngine;

public class WallHologramFeedback : MonoBehaviour
{
    [Header("Configurações Visuais")]
    [Tooltip("Arraste o WallFeedbackCanvas aqui")]
    public GameObject feedbackCanvas; 
    public float displayTime = 10f; // Tempo que o aviso fica ligado na parede

    private Coroutine hideCoroutine;

    private void Start()
    {
        // Garante que o painel comece desligado
        if (feedbackCanvas != null)
        {
            feedbackCanvas.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detecta se foi o jogador que encostou
        if (other.CompareTag("Player"))
        {
            if (feedbackCanvas != null)
            {
                feedbackCanvas.SetActive(true); // Liga o holograma na parede

                // Se já houver um timer rodando, nós o reiniciamos
                if (hideCoroutine != null)
                {
                    StopCoroutine(hideCoroutine);
                }
                
                hideCoroutine = StartCoroutine(HideCanvasAfterTime());
            }
        }
    }

    private IEnumerator HideCanvasAfterTime()
    {
        // Espera os segundos definidos
        yield return new WaitForSeconds(displayTime);
        
        // Desliga o holograma
        feedbackCanvas.SetActive(false);
    }
}