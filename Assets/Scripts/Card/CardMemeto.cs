public class CardMemeto
{
    CardState cardState;
    public CardMemeto(CardState cardState)
    {
        this.cardState = cardState;
    }

    public CardState GetState()
    {
        return cardState;
    }
}
