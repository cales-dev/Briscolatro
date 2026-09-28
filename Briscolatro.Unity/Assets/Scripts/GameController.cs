using UnityEngine;
using Briscolatro.Core.GameStateMachine;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        private Game game;

        private void Start()
        {
            game = new Game();
            game.StartGame();

            Debug.Log($"State: {game.State}");
            Debug.Log($"Current player: {game.CurrentPlayer?.Name}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
