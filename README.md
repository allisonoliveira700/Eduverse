# HackWeb — Escape Room no Unity 🎮

Projeto desenvolvido para o **HackWeb**, um hackathon da turma TIC29. A ideia era criar algo no tema **MetaVerso**, e a gente decidiu fazer um jogo de escape room em primeira pessoa com temática de hacking e segurança digital.

> Se quiser ver funcionando antes de ler tudo: [▶️ vídeo de demonstração no YouTube](https://youtu.be/tPwTM0gcZp8?si=DjDIWzS6D2yex8bE)

---

## O que é esse projeto?

É um jogo FPS (primeira pessoa) feito no Unity 6, onde você explora uma instalação digital e precisa resolver puzzles para avançar. Cada puzzle tem um conceito de segurança digital por trás — autenticação, criptografia, esse tipo de coisa — mas apresentado de um jeito que qualquer pessoa consegue jogar sem precisar saber de tecnologia.

Basicamente: você anda, interage com objetos, lê pistas nos hologramas das paredes, e vai destravando as áreas do mapa conforme resolve os enigmas.

---

## Tecnologias que usamos

- **Unity 6** (com URP — Universal Render Pipeline)
- **C#** pra toda a lógica do jogo
- **TextMeshPro** pra os textos da UI ficarem bonitos
- **Cinemachine** pra câmera
- **Input System** (a API nova do Unity, não a legada)
- **AI Navigation** pra navegação de NPCs

Escolhemos o URP principalmente porque ele tem suporte nativo a Bloom e pós-processamento, que é essencial pra dar aquela estética cyberpunk no jogo. O HDRP seria pesado demais pro escopo do projeto.

---

## Pacotes (manifest.json)

```json
{
  "com.unity.ai.navigation": "2.0.12",
  "com.unity.cinemachine": "3.1.2",
  "com.unity.inputsystem": "1.19.0",
  "com.unity.render-pipelines.universal": "17.3.0",
  "com.unity.timeline": "1.8.12",
  "com.unity.ugui": "2.0.0",
  "com.unity.visualscripting": "1.9.11"
}
```

---

## Estrutura de pastas

```
HackWeb/
│
├── Assets/
│   ├── Scripts/                  # todos os scripts C# estão aqui
│   │   ├── PlayerInteraction.cs
│   │   ├── TerminalEnigma.cs
│   │   ├── PuzzleChaves.cs
│   │   └── WallHologramFeedback.cs
│   │
│   ├── Scenes/
│   ├── Prefabs/
│   ├── Materials/
│   ├── Audio/
│   └── Fonts/
│
├── Packages/
│   ├── manifest.json
│   └── packages-lock.json
│
├── ProjectSettings/
└── README.md
```

---

## Os scripts — como cada um funciona

Foram 4 scripts principais. Cada um cuida de uma coisa só, o que deixou bem organizado.

### PlayerInteraction.cs

Esse é o "cérebro" da interação. A cada frame ele verifica se o jogador clicou o mouse, e quando clicou, dispara um Raycast a partir da câmera. Se o raio bater em algo com a tag `Interactable`, ele verifica qual puzzle está no objeto e chama o método certo.

```csharp
Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, distanciaInteracao)
```

Usa `Mouse.current.leftButton.wasPressedThisFrame` da nova Input System — não o `Input.GetMouseButtonDown(0)` antigo.

| Campo | Tipo | Padrão | O que faz |
|---|---|---|---|
| `distanciaInteracao` | float | 4f | Distância máxima de interação |

---

### TerminalEnigma.cs

O puzzle do computador. Quando você clica no PC, abre uma interface de terminal onde você digita uma senha. Se errar, aparece "TENTE NOVAMENTE" em vermelho. Se acertar, a parede invisível que bloqueia o caminho é destruída.

Uma coisa que levou um tempo pra resolver: o mesmo clique que fecha o terminal abria ele de novo no frame seguinte. Resolvemos isso com uma verificação no início do `AbrirTerminal()`:

```csharp
if (canvasComputador != null && canvasComputador.activeSelf) return;
```

Outro detalhe: a sanitização da senha remove zero-width spaces (`\u200B`) antes de comparar. Isso evita que a senha "correta" não seja reconhecida por causa de caracteres invisíveis que alguns teclados virtuais inserem.

| Campo | O que faz |
|---|---|
| `canvasComputador` | Painel de UI do terminal |
| `inputSenha` | Campo TMP onde o jogador digita |
| `paredeInvisivel` | A barreira que some ao acertar |
| `senhaCorreta` | A senha certa (padrão: `"2008"`) |
| `feedbackText` | Texto de feedback |
| `scriptDeMovimento` | Script de câmera/movimento do jogador |

---

### PuzzleChaves.cs

Esse puzzle simula criptografia assimétrica (chave pública + chave privada). O jogador precisa interagir com dois objetos na ordem correta. Se errar a ordem, as luzes apagam e começa do zero.

A parte mais interessante aqui foi usar variáveis `static` pra compartilhar o estado entre os dois objetos da cena, sem precisar de um GameManager externo:

```csharp
private static int faseAtualDoPuzzle = 0;
private static bool puzzleResolvido = false;
```

O reset automático acontece no `Awake()`, então ao recarregar a cena o puzzle sempre começa do zero.

```
fase 0 → aguardando Mesa 1 (Chave Pública)
fase 1 → Mesa 1 ok, aguardando Mesa 3 (Chave Privada)
fase 2 → puzzle concluído, barreira da escada destruída
```

| Campo | O que faz |
|---|---|
| `numeroDestaMesa` | Identificador: `1` = Chave Pública, `3` = Chave Privada |
| `indicadorVisualLuz` | Luz/partícula que acende ao ativar |
| `barreiraEscada` | Barreira destruída ao completar |
| `feedbackText` | Texto de feedback |

---

### WallHologramFeedback.cs

O mais simples dos quatro. Quando o jogador entra em uma zona de trigger, ativa um canvas holográfico na parede com uma dica ou pista. Depois de um tempo configurável, o holograma apaga sozinho.

Usa `OnTriggerEnter` com verificação de tag `Player`. Se o jogador sair e entrar de novo antes do timer acabar, o timer reinicia (pra não sobrepor Coroutines).

| Campo | Padrão | O que faz |
|---|---|---|
| `feedbackCanvas` | — | Canvas com o holograma |
| `displayTime` | 10f | Tempo que o holograma fica visível |

---

## Como rodar o projeto

**Pré-requisitos:**
- Unity Hub instalado
- Unity 6 (`6000.x.x` ou superior)
- Git

**Passos:**

```bash
git clone https://github.com/allisonoliveira700/Eduverse.git
cd Eduverse
```

Depois abre o Unity Hub, clica em "Add project from disk", seleciona a pasta e confirma que está usando Unity 6. Na primeira abertura ele vai baixar os pacotes do `manifest.json` automaticamente — pode demorar um pouco.

---

## Configurando no Unity Editor

### PlayerInteraction
1. No GameObject do jogador, adicione o componente `PlayerInteraction`
2. Ajuste `distanciaInteracao` se precisar (padrão: 4)

### TerminalEnigma
1. No GameObject do computador, adicione `TerminalEnigma`
2. Preencha os campos no Inspector (Canvas, InputField, parede, senha, feedback, script de movimento)
3. Adicione a tag `Interactable` no computador
4. Conecte os botões "Confirmar" e "Fechar" aos métodos `ConfirmarSenha()` e `FecharTerminal()`

### PuzzleChaves
1. Na Mesa 1: adicione `PuzzleChaves`, defina `numeroDestaMesa = 1`
2. Na Mesa 3: adicione `PuzzleChaves`, defina `numeroDestaMesa = 3`
3. Em um dos dois, arraste a barreira da escada pro campo `barreiraEscada`
4. Adicione a tag `Interactable` nas duas mesas

### WallHologramFeedback
1. Crie um GameObject vazio na parede
2. Adicione um Box Collider com `Is Trigger = true`
3. Adicione o componente e arraste o Canvas do holograma

---

## Tags necessárias no projeto

Certifique-se de criar essas tags no Unity (`Edit > Project Settings > Tags and Layers`):

- `Interactable` — para objetos clicáveis (computador, mesas)
- `Player` — para o personagem do jogador

---

## Fluxo do jogo (resumido)

```
Exploração livre em FPS
    ↓
Entra em zona de trigger → holograma na parede exibe dica
    ↓
Clica no terminal → digita a senha → parede some
    ↓
Clica nas mesas na ordem certa → barreira da escada some
    ↓
Avança para o próximo andar
```

---

## Equipe

Projeto desenvolvido pela turma EduVerse **TIC29** para o HackWeb.

---

