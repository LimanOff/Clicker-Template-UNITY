using Zenject;
using UnityEngine;
using UnityEngine.UI;

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
        _displayCounter.text = GetFormatCount(count);
    }

    private string GetFormatCount(float number)
    {
        string output;
        ulong[] thresholds = {(ulong)1e3, (ulong)1e6, (ulong)1e9,(ulong)1e11,(ulong)1e14};
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
