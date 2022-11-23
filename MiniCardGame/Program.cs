// See https://aka.ms/new-console-template for more information

BlackJackDeck deck = new BlackJackDeck();
var hand = deck.DealCard();
foreach (var card in hand)
{
    Console.WriteLine($"{card.Value.ToString()} {card.Suite.ToString()}");
}

public abstract class Deck
{
    protected List<PlayingCardModel> fullDeck = new List<PlayingCardModel>();
    protected List<PlayingCardModel> drawPile = new List<PlayingCardModel>();
    protected List<PlayingCardModel> discardPile = new List<PlayingCardModel>();
    protected void CreateDeck()
    {
        fullDeck.Clear();
        
        for (int suit = 0; suit < 4; suit++)
        {
            for (int value = 0; value < 13; value++)
            {
                fullDeck.Add(new PlayingCardModel
                {
                    Suite = (CardSuite)suit,
                    Value = (CardValue)value
                });
            }
        }
    }

    public virtual void ShuffleDeck()
    {
        var random = new Random();
        drawPile = fullDeck.OrderBy(x => random.Next()).ToList();
    }

    public abstract List<PlayingCardModel> DealCard();

    protected virtual PlayingCardModel DrawOneCard()
    {
        PlayingCardModel output = drawPile.Take(1).First();
        drawPile.Remove(output);
        return output;
    }
}

public class PokerDeck : Deck
{
    public PokerDeck()
    {
        CreateDeck();
        ShuffleDeck();
    }
    public override List<PlayingCardModel> DealCard()
    {
        List<PlayingCardModel> output = new List<PlayingCardModel>();
        for (int i = 0; i < 5; i++)
        {
            output.Add(DrawOneCard());
            
        }

        return output;
    }

    protected List<PlayingCardModel> RequestCards(List<PlayingCardModel> cardToDiscard)
    {
        List<PlayingCardModel> output = new List<PlayingCardModel>();
        foreach (var card in cardToDiscard)
        {
            output.Add(DrawOneCard());
            discardPile.Add(card);
        }

        return output;
    }
}

public class BlackJackDeck : Deck
{

    public BlackJackDeck()
    {
        CreateDeck();
        ShuffleDeck();
    }
    public override List<PlayingCardModel> DealCard()
    {
        List<PlayingCardModel> output = new List<PlayingCardModel>();
        for (int i = 0; i < 2; i++)
        {
            output.Add(DrawOneCard());
            
        }

        return output;
    }

    public PlayingCardModel RequestCard()
    {
        return DrawOneCard();
    }
}