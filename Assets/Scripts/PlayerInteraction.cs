using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInteraction : MonoBehaviour
{
    public float distanciaInteracao = 4f;
    void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            AtirarLaser();
        }
    }
    void AtirarLaser()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanciaInteracao))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("PASSO 1: O raio bateu no(a) " + hit.collider.gameObject.name);
                TerminalEnigma terminal = hit.collider.GetComponent<TerminalEnigma>();
                if (terminal != null)
                {
                    Debug.Log("PASSO 2: Achou o script! Mandando ligar a tela...");
                    terminal.AbrirTerminal();
                }
                else
                {
                    Debug.LogWarning("ALERTA: Bateu no PC, mas o script TerminalEnigma NÃO ESTÁ lá!");
                }
                // ADICIONE ESTA VERIFICAÇÃO PARA A FASE 2:
                PuzzleChaves abajur = hit.collider.GetComponent<PuzzleChaves>();
                if (abajur != null)
                {
                    abajur.InteragirComAbajur();
                }
                // ADICIONE ESTA VERIFICAÇÃO PARA A FASE 3:
                LivroDAO livro = hit.collider.GetComponent<LivroDAO>();
                if (livro != null)
                {
                    livro.LerLivro();
                }
                // ADICIONE ESTA VERIFICAÇÃO PARA O NOVO TERMINAL DE SAÍDA:
                TerminalSaida terminalSaida = hit.collider.GetComponent<TerminalSaida>();
                if (terminalSaida != null)
                {
                    terminalSaida.AbrirTerminal();
                }
            }
        }
    }
}