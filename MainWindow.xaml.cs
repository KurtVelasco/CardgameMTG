using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static Cardgame.MainWindow;

namespace Cardgame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class Mana
        {
            public int TOTAL_MANA {  get; set; }
            public int COLORLESS_MANA { get; set; }
            public int RED_MANA { get; set; }
            public int BLU_MANA { get; set; }
            public int WHT_MANA { get; set; }
            public int BLK_MANA { get; set; }
            public int GRN_MANA { get; set; }

        }
        public class Players
        {
            public string PLAYER_NAME {  get; set; }
            public int PLAYER_HEALTH {  get; set; }
            public List<string> PLAYER_STATE { get; set; }
            public List<Cards> PLAYER_DECK {  get; set; }  
            public List<Cards> PLAYER_HAND {  get; set; }   
            public List<Cards> PLAYER_DISCARD { get; set; }
            public Mana PLAYER_MANA { get; set; }
        }
        public class Cards
        {
            public string CARD_NAME { get; set; }
            public string MANA_COST { get; set; }
            public string CARD_TYPE { get; set; }
            public int POWER { get; set; }
            public int TOUGHNESS { get; set; }
            public List<string> CARD_ABILITIES { get; set; }

        }
        public enum CardTypes
        {
            Creature,
            Instant,
            Sorcery,
            Enchantment,
            Artifact,
            Planeswalker,
            Land,
        }
        public MainWindow()
        {
            InitializeComponent();
            ReadMTGCardsFromFile("randomCards.txt");
        }
        private List<Cards> ReadMTGCardsFromFile(string fileName)
        {
            List<Cards> mtgCards = new List<Cards>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                Cards currentCard = null;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    if (line.StartsWith("CARD_NAME:"))
                    {
                        currentCard = new Cards { CARD_ABILITIES = new List<string>() };
                        mtgCards.Add(currentCard);
                        currentCard.CARD_NAME = line.Substring("CARD_NAME: ".Length);
                    }
                    else if (line.StartsWith("MANA_COST:"))
                    {
                        currentCard.MANA_COST = line.Substring("MANA_COST: ".Length);
                    }
                    else if (line.StartsWith("CARD_TYPE:"))
                    {
                        currentCard.CARD_TYPE = line.Substring("CARD_TYPE: ".Length);
                    }
                    else if (line.StartsWith("POWER:"))
                    {
                        int power;
                        if (int.TryParse(line.Substring("POWER: ".Length), out power))
                        {
                            currentCard.POWER = power;
                        }
                    }
                    else if (line.StartsWith("TOUGHNESS:"))
                    {
                        int toughness;
                        if (int.TryParse(line.Substring("TOUGHNESS: ".Length), out toughness))
                        {
                            currentCard.TOUGHNESS = toughness;
                        }
                    }
                    else if (line.StartsWith("CARD_ABILITIES:"))
                    {
                        currentCard.CARD_ABILITIES = line.Substring("CARD_ABILITIES: ".Length).Split(',').ToList();
                    }
                }
            }

            return mtgCards;
        }
    


    private void DisplayCardOnGrid(Cards card)
        {
           
            Border cardBorder = new Border
            {
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness(2),
                Width = 150, 
                Height = 200, 
                Margin = new Thickness(10),
            };
            TextBlock cardTextBlock = new TextBlock
            {
                Text = $"Name: {card.CARD_NAME}\n" +
                       $"Mana Cost: {card.MANA_COST}\n" +
                       $"Type: {card.CARD_TYPE}\n" +
                       $"Power/Toughness: {card.POWER}/{card.TOUGHNESS}\n" +
                       $"Abilities: {string.Join(", ", card.CARD_ABILITIES)}",
            };
            cardBorder.Child = cardTextBlock;
            Gameboard.Children.Add(cardBorder);
        }
       
    }
}

