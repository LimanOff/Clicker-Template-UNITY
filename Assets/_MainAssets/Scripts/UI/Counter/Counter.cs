using UnityEngine;

public class Counter : MonoBehaviour
{
    public delegate void CountChangedHandler(ulong count);
    public CountChangedHandler CountChanged;

    [SerializeField] public ulong Count;

    [SerializeField] public ulong CountMultiplier;

    public void IncrementCounter()
    {
        Count += CountMultiplier;

        CountChanged?.Invoke(Count);
    }
}
