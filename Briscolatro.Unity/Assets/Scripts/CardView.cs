using UnityEngine;
using TMPro;
using Briscolatro.Core.Cards;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text cardText;

    private Card card;

    public void Bind(Card card)
    {
        this.card = card;
        cardText.text = card.ToString();
    }
}