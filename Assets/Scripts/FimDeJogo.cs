using UnityEngine;

public class FimDeJogo : MonoBehaviour
{
    [Header("Interface de Vitória")]
    public GameObject canvasVitoria;

    [Header("Jogador")]
    public Behaviour scriptDeMovimento;

    [Header("Áudio de Vitória (NOVO)")]
    [Tooltip("Arraste o AudioSource que fica no gatilho final de saída para cá")]
    public AudioSource caixaDeSom;
    [Tooltip("Música de sucesso/triunfo ao escapar")]
    public AudioClip musicaVitoria;

    private void Start()
    {
        if (canvasVitoria != null)
            canvasVitoria.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (canvasVitoria != null)
                canvasVitoria.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            if (scriptDeMovimento != null)
                scriptDeMovimento.enabled = false;

            // TOCA A MÚSICA DE VITÓRIA AQUI!
            if (caixaDeSom != null && musicaVitoria != null)
            {
                caixaDeSom.PlayOneShot(musicaVitoria);
            }

            Debug.Log("O jogador escapou! Fim de jogo.");
        }
    }
}