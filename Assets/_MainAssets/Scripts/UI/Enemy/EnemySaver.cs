using YG;
using Zenject;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EnemySaver : ISaveable, ILoadable, IInitializable
{
    private List<EnemyData> _allEnemiesSO;
    private List<SerializableEnemy> _serializableEnemies;
    private EnemyKeeper _enemyKeeper;

    [Inject]
    public EnemySaver(EnemyKeeper enemyKeeper)
    {
        _enemyKeeper = enemyKeeper;
    }

    public void Initialize()
    {
        _allEnemiesSO = FindAllEnemiesSOInstances();
        LoadData();
    }
    
    public void SaveData()
    {
        YandexGame.savesData.Enemies = SerializeEnemies(_enemyKeeper.GetAllEnemies());

        YandexGame.SaveProgress();
    }

    public void LoadData()
    {
        if(YandexGame.savesData.Enemies != null)
        {
            _enemyKeeper.SetEnemies(DeserializeEnemies());
        }
    }

    private List<SerializableEnemy> SerializeEnemies(List<EnemyData> enemies)
    {
        var serializeEnemies = new List<SerializableEnemy>(enemies.Count);

        for(int index = 0; index < serializeEnemies.Capacity; index++)
        {
            EnemyData enemyData = enemies[index];
            Texture2D avatarTextureWithReadWriteAccess = enemyData.Avatar.texture.DuplicateTextureWithAllAccess();
            Texture2D backgroundTextureWithReadWriteAcces = enemyData.Background.texture.DuplicateTextureWithAllAccess();

            var avatar = new SerializableEnemy.EnemySprite(avatarTextureWithReadWriteAccess.EncodeToPNG(), enemyData.Avatar.texture.width, enemyData.Avatar.texture.height);
            var background = new SerializableEnemy.EnemySprite(backgroundTextureWithReadWriteAcces.EncodeToPNG(), enemyData.Avatar.texture.width, enemyData.Avatar.texture.height);

            var serializableEnemy = new SerializableEnemy
            {
                Name = enemyData.Name,
                
                Avatar = avatar,
                Background = background,

                MaxHealth = enemyData.MaxHealth,
                Price = enemyData.Price
            };
            
            serializeEnemies.Add(serializableEnemy);
        }

        return serializeEnemies;
    }

    private List<EnemyData> DeserializeEnemies()
    {
        List<SerializableEnemy> serializeEnemies = YandexGame.savesData.Enemies;

        var enemies = new List<EnemyData>(serializeEnemies.Count);

        for(int index = 0; index < enemies.Capacity; index++)
        {
            SerializableEnemy serializeEnemy = serializeEnemies[index];

            Texture2D enemyAvatarTexture = new Texture2D(serializeEnemy.Avatar.Height,serializeEnemy.Avatar.Width);
            Texture2D enemyBackgroundTexture = new Texture2D(serializeEnemy.Background.Height,serializeEnemy.Background.Width);
            enemyAvatarTexture.LoadImage(serializeEnemy.Avatar.Encode);
            enemyBackgroundTexture.LoadImage(serializeEnemy.Background.Encode);
            Sprite enemyAvatarSprite = Sprite.Create(enemyAvatarTexture,new Rect(0, 0, enemyAvatarTexture.width, enemyAvatarTexture.height), new Vector2(0.5f,0.5f));
            Sprite enemyBackgroundSprite = Sprite.Create(enemyBackgroundTexture,new Rect(0, 0, enemyBackgroundTexture.width, enemyBackgroundTexture.height), new Vector2(0.5f,0.5f));

            EnemyData enemyData = new()
            {
                Name = serializeEnemy.Name,

                Avatar = enemyAvatarSprite,
                Background = enemyBackgroundSprite,                

                MaxHealth = serializeEnemy.MaxHealth,
                Price = serializeEnemy.Price
            };

            enemies.Add(_allEnemiesSO.Find(serializeEnemy => serializeEnemy.Name == enemyData.Name));
        }

        return enemies;
    }

    private List<EnemyData> FindAllEnemiesSOInstances()
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(EnemyData).Name);
        var enemyDataSOInstances = new List<EnemyData>(guids.Length);

        for(int index = 0; index < guids.Length; index++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[index]);
            enemyDataSOInstances.Add(AssetDatabase.LoadAssetAtPath<EnemyData>(path));
        }

        return enemyDataSOInstances;
    }
}
