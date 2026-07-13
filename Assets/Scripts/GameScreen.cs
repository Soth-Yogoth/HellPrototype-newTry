using System;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class GameScreen : MonoBehaviour
{
    [SerializeField] private Transform LeftBorder;
    [SerializeField] private Transform RightBorder;

    private static Transform leftBorder;
    private static Transform rightBorder;
    
    private void Start()
    {
        leftBorder = LeftBorder;
        rightBorder = RightBorder;
    }

    static public void PushBorders(float timer)
    {
        Debug.Log("PushBorders");
        
        leftBorder.DOLocalMoveX(leftBorder.position.x - 10, timer).SetEase(Ease.InQuart);
        rightBorder.DOLocalMoveX(rightBorder.position.x + 10, timer).SetEase(Ease.InQuart);
    }
}
