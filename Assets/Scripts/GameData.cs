using System;
using UnityEngine;

public delegate void OnWinDelegate();

[CreateAssetMenu(fileName = "GameData", menuName = "Scriptable Objects/GameData")]
public class GameData : ScriptableObject
{
    private static int chanceToWin = 5;
    private static int mobQuantity = 0;
    private static int score = 0;
    
    private static int playerHp = 3;
    
    public static event Action<int> OnPlayerHpChanged;
    public static event Action OnAllMobsDead;
    public static event Action OnGameOver;
    public static OnWinDelegate OnEnterToNirvana;

    public static void Reset()
    {
        chanceToWin = 5;
        mobQuantity = 0;
        playerHp = 3;
    }

    public static int PlayerHp
    {
        get { return playerHp; }
        set
        {
            playerHp = value;
            OnPlayerHpChanged?.Invoke(playerHp); 
        }
    }

    public static int ChanceToWin
    {
        get { return chanceToWin; } 
        set
        {
            chanceToWin = value;
            if (chanceToWin == 0) OnGameOver?.Invoke();
        }
    }
    
    public static int MobQuantity
    {
        get { return mobQuantity; } 
        set
        {
            mobQuantity = value;
            if (mobQuantity == 0) OnAllMobsDead?.Invoke();
        }
    }
    
    public int Score { set { score = value; } }
    
}
