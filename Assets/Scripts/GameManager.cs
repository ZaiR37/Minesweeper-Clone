namespace ZaiR37
{
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using ZaiR37.Grid;

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { private set; get; }
        [SerializeField] private Transform buttonPrefab;
        [SerializeField] private Transform buttonContainer;

        [Header("Size")]
        [SerializeField] private int gridWidth = 9;
        [SerializeField] private int gridHeight = 7;
        [SerializeField] private int randomBomb = 9;

        private List<GameButton> buttonToCheck;
        private List<GameButton> buttonWithoutBomb;
        private GameButton[,] buttonArray;

        private void Awake()
        {
            if (Instance) return;
            else Instance = this;
        }

        private void Start()
        {
            buttonWithoutBomb = new List<GameButton>();
            buttonArray = new GameButton[gridHeight, gridWidth];
            buttonToCheck = new List<GameButton>();

            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    Transform newButton = Instantiate(buttonPrefab, buttonContainer);
                    GameButton gameButton = newButton.GetComponent<GameButton>();

                    gameButton.SetPosition(i, j);

                    buttonArray[i, j] = gameButton;
                    buttonWithoutBomb.Add(gameButton);
                }
            }

            for (int i = 0; i < randomBomb; i++)
            {
                int randomNumber = Random.Range(0, buttonWithoutBomb.Count);

                GameButton gameButton = buttonWithoutBomb[randomNumber];
                buttonWithoutBomb.RemoveAt(randomNumber);

                gameButton.SetBombStatus();
            }
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(0);
        }

        public void SetAllButtonOff()
        {
            foreach (var button in buttonArray)
            {
                button.SetButtonOff();
            }
        }

        public bool IsValidGridPosition(GridPosition gridPosition)
        {
            return gridPosition.x >= 0 &&
                    gridPosition.y >= 0 &&
                    gridPosition.x < gridHeight &&
                    gridPosition.y < gridWidth;
        }

        public bool CheckForWinning()
        {
            return buttonWithoutBomb.Count == 0;
        }

        public void Winning()
        {
            Debug.Log("You Won!");
            foreach (var button in buttonArray)
            {
                if (button.GetBombStatus()) button.SetText("X");
            }
        }

        public GameButton GetGameButton(int x, int y) => buttonArray[x, y];


        public List<GameButton> GetButtonToCheck() => buttonToCheck;
        public void RemoveButtonWithoutBomb(GameButton gameButton) => buttonWithoutBomb.Remove(gameButton);
        public void RemoveButtonToCheck(GameButton gameButton) => buttonToCheck.Remove(gameButton);
        public void AddButtonToCheck(List<GameButton> gameButtons)
        {
            foreach (var button in gameButtons)
            {
                buttonToCheck.Add(button);
            }
        }
    }

}