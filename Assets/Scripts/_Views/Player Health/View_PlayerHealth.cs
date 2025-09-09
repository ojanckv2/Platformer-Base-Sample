using Ojanck.Core.Scene;
using TMPro;
using UnityEngine;

public class View_PlayerHealth : SceneServiceView
{
    [SerializeField] private TextMeshProUGUI textRemainingHeart;

    public void SetRemainingHearts(int hearts)
    {
        textRemainingHeart.text = hearts.ToString();
    }
}