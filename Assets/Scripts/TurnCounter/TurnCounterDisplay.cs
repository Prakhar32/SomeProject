using TMPro;
using UnityEngine;

public class TurnCounterDisplay : MonoBehaviour
{
    public TurnCounter TurnCounter;
    private TextMeshProUGUI _text;

    void Start()
    {
        if(TurnCounter == null)
        {
            Destroy(this);
            throw new MissingReferenceException("TurnCounter is not assigned.");
        }

        _text = GetComponent<TextMeshProUGUI>();
        if(_text == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Text field cannot be null");
        }

        TurnCounter.SubscribeToTurnChange(displayTurn);
    }

    private void displayTurn()
    {
        _text.text = "Turn : " + TurnCounter.getTurnCounter();
    }
}
