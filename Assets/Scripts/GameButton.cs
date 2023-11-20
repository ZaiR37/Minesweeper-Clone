using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ZaiR37;
using ZaiR37.Grid;

public class GameButton : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private GameObject iconGameObject;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    [Header("Status")]
    [SerializeField] private GridPosition gridPosition;
    [SerializeField] private bool bomb;

    public void ClickButton(bool value)
    {
        toggle.interactable = false;

        if (bomb)
        {
            GameManager.Instance.SetAllButtonOff();
            return;
        }

        if (IsAnyBombAround())
        {
            if (GameManager.Instance.CheckForWinning()) GameManager.Instance.Winning();
            return;
        }

        List<GameButton> buttonToCheck = GameManager.Instance.GetButtonToCheck();

        bool checking = true;
        while (checking)
        {
            buttonToCheck[0].IsAnyBombAround();

            try
            {
                GameManager.Instance.RemoveButtonToCheck(buttonToCheck[0]);
            }
            catch (System.Exception)
            {

            }

            buttonToCheck = GameManager.Instance.GetButtonToCheck();
            checking = !(buttonToCheck.Count == 0);
        }

        if (GameManager.Instance.CheckForWinning()) GameManager.Instance.Winning();
    }

    public bool IsAnyBombAround()
    {
        int bombCount = 0;
        GridPosition testGridPosition;
        List<GameButton> buttonToCheck = new List<GameButton>();

        /// RIGHT SIDE
        testGridPosition = new GridPosition(gridPosition.x + 1, gridPosition.y);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton;
            try
            {
                gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);
            }
            catch (System.Exception)
            {
                Debug.Log(testGridPosition);
                throw;
            }
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }

            if (gameButton.GetBombStatus()) bombCount++;
        }

        testGridPosition = new GridPosition(gridPosition.x + 1, gridPosition.y + 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        testGridPosition = new GridPosition(gridPosition.x + 1, gridPosition.y - 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        /// LEFT SIDE
        testGridPosition = new GridPosition(gridPosition.x - 1, gridPosition.y);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        testGridPosition = new GridPosition(gridPosition.x - 1, gridPosition.y + 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        testGridPosition = new GridPosition(gridPosition.x - 1, gridPosition.y - 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }


        testGridPosition = new GridPosition(gridPosition.x, gridPosition.y + 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        testGridPosition = new GridPosition(gridPosition.x, gridPosition.y - 1);
        if (GameManager.Instance.IsValidGridPosition(testGridPosition))
        {
            GameButton gameButton = GameManager.Instance.GetGameButton(testGridPosition.x, testGridPosition.y);

            if (gameButton.GetBombStatus()) bombCount++;
            if (!gameButton.GetToggleStatus())
            {
                buttonToCheck.Add(gameButton);
            }
        }

        toggle.isOn = true;
        GameManager.Instance.RemoveButtonWithoutBomb(this);

        if (bombCount > 0)
        {
            SetText(bombCount.ToString());
            return true;
        }

        GameManager.Instance.AddButtonToCheck(buttonToCheck);
        return false;
    }

    public bool GetBombStatus() => bomb;

    public void SetButtonOff()
    {
        if (bomb)
        {
            iconGameObject.SetActive(true);
            toggle.isOn = true;
        }

        toggle.interactable = false;
    }

    public void SetBombStatus() => bomb = true;
    public bool GetToggleStatus() => toggle.isOn;
    public void SetText(string text) => textMeshPro.text = text;
    public void SetPosition(int height, int width) => gridPosition = new GridPosition(height, width);
}
