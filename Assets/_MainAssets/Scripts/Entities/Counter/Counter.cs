using UnityEngine;

public class Counter : MonoBehaviour
{
    public delegate void CountChangedHandler(ulong count);
    public CountChangedHandler CountChanged;

    [SerializeField] public ulong Count;

    private ulong _countMultiplier;
 
    public ulong CountMultiplier
    {
        get => _countMultiplier;

        set { _countMultiplier = value > 0 ? value : (ulong)1e15;}
    }

    public void IncrementCounter()
    {
        Count += CountMultiplier;

        CountChanged?.Invoke(Count);
    }
}
