# Unity 2D Platform Custom Physics

Tutorial oficial oferecido pela Unity para criar um jogo de plataforma 2D utilizando física customizada.

## Sobre o jogo
Movimente a personagem e pule por obstáculos utilizando os controles básicos do Unity.

Utilizando o componente Rigidbody2D como base, as mecânicas de um jogo de plataforma foram reescritas para que, no gameplay, se tornassem mais simples e próximas dos antigos jogos de plataforma.

## Controles
* Computador
  * Setas direcionais / A-D
    Movimentar personagem
  * Barra de espaço
    Pular
* Controle de X-Box
  * Analógico esquerdo
    Movimentar personagem
  * Y
    Pular

## Bugs conhecidos
Aqui ficarão descritos todos os bugs encontrados até o final do tutorial. Sinta-se a vontade para sugerir uma solução caso saiba como resolver.

* Ao andar nos chãos diagonais, a personagem é jogada para cima, parecendo que está fazendo um pulo. Isso acontece devido ao bloco de código que possibilita andar em colisões diagonais, presente em *Scripts/PhysicsObject.cs*.
---------
Link para o tutorial oficial: https://unity3d.com/pt/learn/tutorials/topics/2d-game-creation/intro-and-session-goals
