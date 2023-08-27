using YG;
using Zenject;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CounterDisplay : MonoBehaviour, IInitializable
{
    [Inject(Id = "UI/Counter")] private Text _displayCounter;
    
    [Inject] private Counter _counter;

    public void Initialize()
    {
        _counter.CountChanged += UpdateDisplayCounter;

        UpdateDisplayCounter(_counter.Count);
    }

    public void UpdateDisplayCounter(ulong count)
    {
        _displayCounter.text = GetFormatText(count);
    }

    public static string GetFormatText(float number)
    {
        string output;
        ulong[] thresholds = {(ulong)1e3, (ulong)1e6, (ulong)1e9,(ulong)1e11,(ulong)1e14};

        List<string> thresholds_suffixes = new List<string>(1);

        if(YandexGame.EnvironmentData.language == "ru")
        {
            string[] RUsuffixes = {"тыс", "млн", "млр", "трлн", "квин", "сек", "сеп", "окт", "нон", "дец", "унд", "дуо"};
            thresholds_suffixes.AddRange(RUsuffixes);
        }
        else
        {
            string[] ENGsuffixes = {"K", "M" ,"B" ,"T" ,"Q", "Sec", "Sep", "Oct", "Non", "Dec", "Und", "Duo"};
            thresholds_suffixes.AddRange(ENGsuffixes);
        }


	    if(number < thresholds[0])
        {
         return $"{number} $";
        }

        for(int index = 0; index < thresholds.Length; index++)
        {
            int nextThreshold = index + 1;

            if(number >= thresholds[index] && (index == thresholds.Length-1 || number < thresholds[nextThreshold]))
            {
                number /= thresholds[index];
                output = string.Format("{0:#.00}",Mathf.Floor(number * 100) / 100) + thresholds_suffixes[index];
                return $"{output} $";
            }
        }

	    return $"{number.ToString()} $";
    }
}
