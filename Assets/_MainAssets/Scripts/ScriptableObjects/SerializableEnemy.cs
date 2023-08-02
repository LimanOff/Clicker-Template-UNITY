using UnityEngine;
using System;

[Serializable]
public struct SerializableEnemy
{
    public string Name;
    public byte[] AvatarEncode;
    public int AvatarWidth;
    public int AvatarHeight;
    public ulong MaxHealth;
    public ulong Price;

    public SerializableEnemy(string name, byte[] avatarEncode, int avatarHeight, int avatarWidth,ulong maxHealth, ulong price)
    {
        Name = name;

        AvatarEncode = avatarEncode;
        AvatarHeight = avatarHeight;
        AvatarWidth = avatarWidth;

        MaxHealth = maxHealth;
        Price = price;
    }
}
