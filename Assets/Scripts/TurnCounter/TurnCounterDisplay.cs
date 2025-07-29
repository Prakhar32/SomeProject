using TMPro;
using UnityEngine;

public class TurnCounterDisplay : MonoBehaviour
{
    public TurnCounter TurnCounter;
    public TextMeshProUGUI Text;

    void Start()
    {
        if(TurnCounter == null)
        {
            Destroy(this);
            throw new MissingReferenceException("TurnCounter is not assigned.");
        }

        if(Text == null)
        {
            Destroy(this);
            throw new MissingReferenceException("Text field cannot be null");
        }

        TurnCounter.SubscribeToTurnChange(displayTurn);
    }

    private void displayTurn()
    {
        Text.text = "Turn : " + TurnCounter.getTurn();
    }
}
