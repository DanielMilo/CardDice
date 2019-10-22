using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckTester : MonoBehaviour
{
    [SerializeField] int[] Cards;

    [SerializeField] int minimalThreshold;
    [SerializeField] int maximalThreshold;

    int[] initialDeck;

    // Start is called before the first frame update
    void Start()
    {
        initialDeck = new int[Cards.Length];
        Cards.CopyTo(initialDeck,0);

        Debug.Log("Deck: " + name);

        Debug.Log("Expected value: " + GetExpectedValue());

        Debug.Log("Chance to roll " + minimalThreshold + " or above: " + ChanceAboveThreshold(minimalThreshold));
        Debug.Log("Chance to roll " + maximalThreshold + " or below: " + ChanceBelowThreshold(maximalThreshold));

        Debug.Log("Expected w/ Advantage: " + GetAdvantageExpectedValue());
        Debug.Log("Expected w/ Disadvantage: " + GetDisadvantageExpectedValue());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int drawAndShuffle()
    {
        return Cards[Random.Range(0, Cards.Length)];
    }

    int sum()
    {
        int sum = 0;
        foreach (int card in Cards)
        {
            sum += card;
        }
        return sum;
    }

    float GetExpectedValue()
    {
        return (sum() / (float)Cards.Length);
    }

    float GetAdvantageExpectedValue()
    {
        float sum = 0;
        foreach (int card in Cards)
        {
            int oneCardSum = 0;
            List<int> temp = new List<int>(Cards);
            temp.Remove(card);
            foreach (int second in temp)
            {
                if(second > card)
                {
                    oneCardSum += second;
                }
                else
                {
                    oneCardSum += card;
                }
            }
            sum += oneCardSum / (float)temp.Count;
        }

        return sum / Cards.Length;
    }

    float GetDisadvantageExpectedValue()
    {
        float sum = 0;
        foreach (int card in Cards)
        {
            int oneCardSum = 0;
            List<int> temp = new List<int>(Cards);
            temp.Remove(card);
            foreach (int second in temp)
            {
                if (second < card)
                {
                    oneCardSum += second;
                }
                else
                {
                    oneCardSum += card;
                }
            }
            sum += oneCardSum / (float)temp.Count;
        }

        return sum / Cards.Length;
    }

    float ChanceAboveThreshold(int minimalDraw)
    {
        int aboveCards = 0;
        foreach (int card in Cards)
        {
            if (card >= minimalDraw)
                aboveCards++;
        }

        return (float)aboveCards / Cards.Length;
    }


    float ChanceBelowThreshold(int maximalDraw)
    {
        int belowCards = 0;
        foreach (int card in Cards)
        {
            if (card <= maximalDraw)
                belowCards++;
        }

        return (float)belowCards / Cards.Length;
    }

}
