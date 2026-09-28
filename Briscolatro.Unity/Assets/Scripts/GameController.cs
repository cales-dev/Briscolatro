using UnityEngine;
using Briscolatro.Core.GameStateMachine;

public class GameController : MonoBehaviour
{
    private Game game;

    [SerializeField] private CardView cardPrefab;
    [SerializeField] private Transform playerHandRoot;

    private void Start()
    {
        game = new Game();
        game.StartGame();

        SpawnPlayerHand();
    }

    private void SpawnPlayerHand()
    {
        float spacing = 1.3f;
        int cardCount = game.Player1.Hand.Count;

        float startX = -((cardCount - 1) * spacing) / 2f;

        for (int i = 0; i < cardCount; i++)
        {
            var card = game.Player1.Hand[i];

            CardView cardView = Instantiate(cardPrefab, playerHandRoot);

            cardView.transform.localPosition =
                new Vector3(startX + i * spacing, 0f, 0f);

            cardView.Bind(card);
        }
    }
}