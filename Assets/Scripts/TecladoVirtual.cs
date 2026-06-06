using UnityEngine;
using TMPro;

public class TecladoVirtual : MonoBehaviour
{
    [Header("Conexão com o PC")]
    [Tooltip("Arraste o campo de texto (TMP_InputField) do seu terminal para cá")]
    public TMP_InputField campoDeSenha;

    [Header("Configuração de Áudio Opcional")]
    public AudioSource caixaDeSom;
    public AudioClip somTecla;

    // Essa função será chamada pelos botões numéricos (0 a 9)
    public void DigitarCaractere(string caractere)
    {
        if (campoDeSenha != null)
        {
            // Adiciona o número digitado ao final do texto que já está lá
            campoDeSenha.text += caractere;
            TocarSomDeTecla();
        }
    }

    // Essa função será chamada pelo botão de "Apagar" ou "Corrigir"
    public void ApagarUltimo()
    {
        if (campoDeSenha != null && campoDeSenha.text.Length > 0)
        {
            // Remove a última letra/número da string
            campoDeSenha.text = campoDeSenha.text.Substring(0, campoDeSenha.text.Length - 1);
            TocarSomDeTecla();
        }
    }

    // Essa função será chamada pelo botão "Limpar Tudo" (Opcional)
    public void LimparTudo()
    {
        if (campoDeSenha != null)
        {
            campoDeSenha.text = "";
            TocarSomDeTecla();
        }
    }

    private void TocarSomDeTecla()
    {
        if (caixaDeSom != null && somTecla != null)
        {
            caixaDeSom.PlayOneShot(somTecla);
        }
    }
}