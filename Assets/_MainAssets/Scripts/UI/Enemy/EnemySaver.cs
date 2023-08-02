using YG;
using Zenject;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EnemySaver : MonoBehaviour, ISaveable, ILoadable, IInitializable
{
    private List<EnemyData> _allEnemiesSO;
    private List<SerializableEnemy> _serializableEnemies;
    private EnemyKeeper _enemyKeeper;
    private EnemyDisplay _enemyDisplay;

    [Inject]
    private void Construct(EnemyKeeper enemyKeeper, EnemyDisplay enemyDisplay)
    {
        _enemyKeeper = enemyKeeper;
        _enemyDisplay = enemyDisplay;
    }

    public void Initialize()
    {
        _allEnemiesSO = FindAllEnemiesSOInstances();
        LoadData();
        
        Debug.Log("<color=yellow>EnemySaver</color> is <color=green>Initialize</color>");
    }
    
    private void OnDestroy()
    {
        _enemyDisplay.EnemyKilled -= SaveData;
    }
    
    [ContextMenu("SaveData")]
    public void SaveData()
    {
        YandexGame.savesData.Enemies = SerializeEnemies(_enemyKeeper.GetAllEnemies());

        YandexGame.SaveProgress();
    }

    [ContextMenu("LoadData")]
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
            Texture2D textureWithReadWriteAccess = enemyData.Avatar.texture.DuplicateTextureWithAllAccess();

            var serializableEnemy = new SerializableEnemy
            {
                Name = enemyData.Name,
                
                AvatarEncode = textureWithReadWriteAccess.EncodeToPNG(),
                AvatarHeight = enemyData.Avatar.texture.height,
                AvatarWidth = enemyData.Avatar.texture.width,

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

            Texture2D enemyTexture = new Texture2D(serializeEnemy.AvatarHeight,serializeEnemy.AvatarWidth);
            enemyTexture.LoadImage(serializeEnemy.AvatarEncode);
            Sprite enemySprite = Sprite.Create(enemyTexture,new Rect(0, 0, enemyTexture.width, enemyTexture.height), new Vector2(0.5f,0.5f));

            EnemyData enemyData = new EnemyData()
            {
                Name = serializeEnemy.Name,

                Avatar = enemySprite,

                MaxHealth = serializeEnemy.MaxHealth,
                Price = serializeEnemy.Price
            };

            enemies.Add(_allEnemiesSO.Find(x => x.Name == enemyData.Name));
        }

        return enemies;
    }

    private List<EnemyData> FindAllEnemiesSOInstances()
    {
        string[] guids = AssetDatabase.FindAssets("t:"+ typeof(EnemyData).Name);
        List<EnemyData> enemyDataSOInstances = new List<EnemyData>(guids.Length);

        for(int index =0; index < guids.Length; index++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[index]);
            enemyDataSOInstances.Add(AssetDatabase.LoadAssetAtPath<EnemyData>(path));
        }

        return enemyDataSOInstances;
    }
}
