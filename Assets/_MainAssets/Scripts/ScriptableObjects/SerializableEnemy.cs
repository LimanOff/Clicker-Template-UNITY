using UnityEngine;
using System;

[Serializable]
public struct SerializableEnemy
{
    public string Name;

    public EnemySprite Avatar;
    public EnemySprite Background;

    public ulong MaxHealth;
    public ulong Price;

    public SerializableEnemy(string name, EnemySprite avatar, EnemySprite background, ulong maxHealth, ulong price)
    {
        Name = name;
        Avatar = avatar;
        Background = background;
        MaxHealth = maxHealth;
        Price = price;
    }

    [Serializable]
    public struct EnemySprite
    {
        public byte[] Encode;
        public int Width;
        public int Height;

        public EnemySprite(byte[] encode, int width, int height)
        {
            Encode = encode;
            Width = width;
            Height = height;
        }
    }
}
