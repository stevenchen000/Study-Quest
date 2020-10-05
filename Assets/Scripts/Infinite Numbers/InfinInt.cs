using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InfinInt : ISerializationCallbackReceiver
{
    [SerializeField]
    private List<int> numbers = new List<int>();
    private string[] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

    [SerializeField]
    private string value;

    public InfinInt()
    {
        numbers.Add(0);
    }

    public InfinInt(List<int> newNumbers)
    {
        numbers.AddRange(newNumbers);
    }



    
    public List<int> GetNumbers() { return numbers; }

    

    public override string ToString()
    {
        int lastNumber = numbers[numbers.Count - 1];
        string symbol = NumberToLetter(numbers.Count);
        return $"{lastNumber}{symbol}";
    }

    private string NumberToLetter(int num)
    {
        string result = "";

        int index = num;
        if(index == 1) { result = ""; }
        else if(index == 2) { result = "a"; }
        else
        {
            List<int> baseConversion = new List<int>();

            index -= 2;
            while (index > 0)
            {
                int remainder = index % 26;
                index = index / 26;
                baseConversion.Add(remainder);
            }

            if (baseConversion.Count > 1)
            {
                baseConversion[baseConversion.Count - 1]--;
            }

            for (int i = baseConversion.Count - 1; i >= 0; i--)
            {
                int baseIndex = baseConversion[i];
                result += letters[baseIndex];
            }
        }

        return result;
    }



















    public static InfinInt operator +(InfinInt a, InfinInt b)
    {
        List<int> numbersA = a.GetNumbers();
        List<int> numbersB = b.GetNumbers();
        int maxIndex = Math.Max(numbersA.Count, numbersB.Count);

        List<int> numbersResult = new List<int>();

        int overflow = 0;
        for(int i = 0; i < maxIndex; i++)
        {
            int valA = numbersA.Count > i ? numbersA[i] : 0;
            int valB = numbersB.Count > i ? numbersB[i] : 0;
            int resultVal = valA + valB + overflow;
            overflow = 0;

            while(resultVal > 999)
            {
                resultVal -= 1000;
                overflow++;
            }

            numbersResult.Add(resultVal);
        }

        if(overflow > 0) { numbersResult.Add(overflow); }

        return new InfinInt(numbersResult);
    }


    public void OnAfterDeserialize()
    {
        value = ToString();
    }

    public void OnBeforeSerialize()
    {
    }
}
