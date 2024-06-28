using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PanelState
{
    Vuoto,
    OccupatoPlayer,
    OccupatoEnemy
}

public enum Turn
{
    Player,
    Enemy
}

public enum GameStatus
{
    InProgress,
    PlayerWin,
    EnemyWin,
    Draw,
    EndGame
}

public struct PanelInfo
{
    public PanelState state;
    public int x, y;
}

public class GameManager : MonoBehaviour
{
    public PanelInfo[,] grid = 
        new PanelInfo[3, 3];

    public Turn currentTurn = Turn.Player;
    public GameStatus currentStatus = GameStatus.InProgress;

    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;

    [SerializeField] private GameObject gridObject;

    private static GameManager _instance;
    public static GameManager Instance { get{ return _instance; } }

    void Awake()
    {
        _instance = this;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    private void Reset()
    {
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                grid[row, col].state = PanelState.Vuoto;
            }
        }

        foreach(Transform panel in gridObject.GetComponentsInChildren<Transform>())
        {
            ChangePanelColor(panel.gameObject, Color.white);
        }

        currentStatus = GameStatus.InProgress;
        currentTurn = Turn.Player;
    }

    public void PlaceMove(int x, int y, GameObject panel)
    {
        if(currentStatus != GameStatus.InProgress) return;

        switch(currentTurn)
        {
            case Turn.Player:
                grid[x,y].state = PanelState.OccupatoPlayer;

                ChangePanelColor(panel, playerColor);

                break;
            case Turn.Enemy:
                grid[x,y].state = PanelState.OccupatoEnemy;

                ChangePanelColor(panel, enemyColor);

                break;
        }

        if(CheckBoard())
        {
            currentStatus = GameStatus.EndGame;
            return;
        }

        currentTurn = currentTurn == Turn.Player ? Turn.Enemy : Turn.Player;

        

        // if(currentTurn == Turn.Player)
        // {
        //     currentTurn = Turn.Enemy;
        // }
        // else
        // {
        //     currentTurn = Turn.Player;
        // }
    }

    private bool CheckBoard()
    {
        int playerCount = 0;
        int enemyCount = 0;

        // check rows (orizzontale)
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                if(grid[row, col].state == PanelState.OccupatoPlayer)
                {
                    playerCount++;
                }
                else if(grid[row, col].state == PanelState.OccupatoEnemy)
                {
                    enemyCount++;
                }
            }

                if(CheckWin(playerCount, enemyCount))
                {
                    return true;
                }
                
                playerCount = 0;
                enemyCount = 0;
        }

        // check columns (verticale)
        for(int col = 0; col < grid.GetLength(1); col++)
        {
            for(int row = 0; row < grid.GetLength(0); row++)
            {
                if(grid[row, col].state == PanelState.OccupatoPlayer)
                {
                    playerCount++;
                }
                else if(grid[row, col].state == PanelState.OccupatoEnemy)
                {
                    enemyCount++;
                }
            }

            if(CheckWin(playerCount, enemyCount)) { return true; }

            playerCount = 0;
            enemyCount = 0;
        }

        // check diagonals
        if(grid[0,0].state == grid[1,1].state && grid[1,1].state == grid[2,2].state)
        {
            if(grid[0,0].state == PanelState.OccupatoPlayer) 
            {  
                playerCount = 3;
                return CheckWin(playerCount, enemyCount);
            }
            else if(grid[0,0].state == PanelState.OccupatoEnemy)
            {
                enemyCount = 3;
                return CheckWin(playerCount, enemyCount);
            }
        }

        if(grid[0,2].state == grid[1,1].state && grid[1,1].state == grid[2,0].state)
        {
            if(grid[0,2].state == PanelState.OccupatoPlayer) 
            {  
                playerCount = 3;
                return CheckWin(playerCount, enemyCount);
            }
            else if(grid[0,2].state == PanelState.OccupatoEnemy)
            {
                enemyCount = 3;
                return CheckWin(playerCount, enemyCount);
            }
        }

        if(CheckDraw()) { return true; }

        return false;
    }

    private bool CheckWin(int playerCount, int enemyCount)
    {
        if(playerCount == 3)
        {
            currentStatus = GameStatus.PlayerWin;
            Debug.Log("Player Win");
            return true;
        }
        else if(enemyCount == 3)
        {
            currentStatus = GameStatus.EnemyWin;
            Debug.Log("Enemy Win");
            return true;
        }

        return false;
    }

    private bool CheckDraw()
    {
        for(int row = 0; row < grid.GetLength(0); row++)
        {
            for(int col = 0; col < grid.GetLength(1); col++)
            {
                if(grid[row, col].state == PanelState.Vuoto)
                {
                    return false;
                }
            }
        }

        currentStatus = GameStatus.Draw;
        Debug.Log("Draw");
        return true;
    }

    private void ChangePanelColor(GameObject panel, Color color)
    {
        if(panel.TryGetComponent(out Image image))
        {
            image.color = color;
        }
    }
}
