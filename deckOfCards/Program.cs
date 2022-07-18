Player player = new Player();
Deck deck = new Deck();

bool isWork = true;
string userInput;

Console.WriteLine("Добро пожаловать за карточный стол!");

while (isWork)
{
    Console.WriteLine("Выберите действие:\n" +
    "1. Взять одну карту\n" +
    "2. Завершить игру и показать результат.");

    userInput = Console.ReadLine();

    switch (userInput)
    {
        case "1":
            player.GetCard(deck, ref isWork);
            break;
        case "2":
            player.ShowInfoCards();
            break;
        default:
            Console.WriteLine("Такой команды нет!");
            break;
    }
}



class Player
{
    private List<Card> _cardsInHand = new List<Card>();

    public void GetCard(Deck cards, ref bool isWork)
    {
        Card card = cards.GiveCard(ref isWork);
        _cardsInHand.Add(card);

        if (isWork == false)
        {
            Console.WriteLine($"В колоде не осталось карт, игра завершена!");
            ShowInfoCards();
        }
    }

    public void ShowInfoCards()
    {
        int count = 0;
        foreach (Card card in _cardsInHand)
        {
            count++;
            card.ShowInfo();

            if (count % 4 == 0)
                Console.WriteLine();
        }
    }
}

class Deck
{
    private Stack<Card> _cards = new Stack<Card>(NUMBER_OF_CARDS);

    private const int NUMBER_OF_CARDS = 52;

    public Deck()
    {
        string[] faces = { "Черви", "Крести", "Пики", "Буби" };
        string[] suits = { "Туз", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Валет", "Дама", "Король" };

        for (int i = 0; i < faces.Length; i++)
        {
            for (int j = 0; j < suits.Length; j++)
            {
                _cards.Push(new Card(suits[j], faces[i]));
            }
        }
        Shuffle();
    }

    public Card GiveCard(ref bool isWork)
    {
        Card card = _cards.Pop();

        if (_cards.Count == 0)
            isWork = false;
            
        return card;
    }

    private void Shuffle()
    {
        Card[] cards = new Card[NUMBER_OF_CARDS];
        Random random = new Random();

        for (int i = 0; i < NUMBER_OF_CARDS; i++)
        {
            cards[i] = _cards.Pop();
        }

        for (int i = 0; i < cards.Length; i++)
        {
            int swappingCardIndex = random.Next(0, NUMBER_OF_CARDS);

            Card temp = cards[i];
            cards[i] = cards[swappingCardIndex];
            cards[swappingCardIndex] = temp;
        }

        for (int i = 0; i < cards.Length; i++)
        {
            _cards.Push(cards[i]);
        }
    }
}

class Card
{
    public string Suit { get; private set; }
    public string Face { get; private set; }

    public Card(string suit, string face)
    {
        Suit = suit;
        Face = face;
    }

    public void ShowInfo()
    {
        Console.Write($"| {Face} {Suit} |  ");
    }
}