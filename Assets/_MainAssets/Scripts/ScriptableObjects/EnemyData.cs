using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string Name;
    public Sprite Avatar;
    public Sprite Background;
    public ulong MaxHealth;
    public ulong Price;
}
