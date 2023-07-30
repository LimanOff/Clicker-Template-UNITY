using Zenject;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Counter))]
public class CounterDisplay : MonoBehaviour
{
    [Inject(Id = "UI/Counter")] private Text _displayCounter;
    
    [Inject] private Counter _counter;

    public void Initialize()
    {
        _counter.CountChanged += UpdateDisplayCounter;

        UpdateDisplayCounter(_counter.Count);
        Debug.Log("<color=yellow>CounterDisplay</color> is <color=green>initialized</color>");
    }

    public void UpdateDisplayCounter(ulong count)
    {
        _displayCounter.text = GetFormatCount(count);
    }

    private string GetFormatCount(float number)
    {
        string output;
        ulong[] thresholds = {1000, 1000000, 1000000000,1000000000000,1000000000000000};
        char[] thresholds_suffixes = {'K','M','B','T','Q'};


	    if(number < thresholds[0])
        {
         return number.ToString();
        }

        for(int index = 0; index < thresholds.Length; index++)
        {
            int nextThreshold = index + 1;

            if(number >= thresholds[index] && (index == thresholds.Length-1 || number < thresholds[nextThreshold]))
            {
                number /= thresholds[index];
                output = string.Format("{0:#.00}",Mathf.Floor(number * 100) / 100) + thresholds_suffixes[index];
                return output;
            }
        }

	    return number.ToString();
    }
}
