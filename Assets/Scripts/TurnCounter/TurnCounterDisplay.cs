using TMPro;
using UnityEngine;

public class TurnCounterDisplay : MonoBehaviour
{
    private TurnCounter _turnCounter;
    private TextMeshProUGUI _text;

    public void SetTurnCounter(TurnCounter turnCounter) {  _turnCounter = turnCounter; }

    void Start()
    {
        if(_turnCounter == null)
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

        _turnCounter.SubscribeToTurnChange(displayTurn);
    }

    private void displayTurn()
    {
        _text.text = "Turn : " + _turnCounter.getTurnCounter();
    }
}
