using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HpBar : MonoBehaviour
{
    private Image img;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        img = GetComponent<Image>();
    }

    public void UpdateHpBar(float currentHp, float max)
    {
        img.fillAmount = currentHp / max;
    }
}
