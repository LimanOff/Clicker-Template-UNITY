using YG;
using Zenject;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSaver : MonoBehaviour, IInitializable
{
    [Tooltip("Каждые TimeSave происходит сохранения данных")]
    public int TimeSave;

    private Coroutine _timer;

    private List<ISaveable> _saveablesEntities;

    [Inject]
    private void Construct(EnemySaver enemySaver, CounterSaver counterSaver)
    {
        _saveablesEntities = new()
        {
            enemySaver,
            counterSaver
        };
    }

    public void Initialize()
    {
        if(_timer != null)
        {
            StopCoroutine(_timer);
        }

        _timer = StartCoroutine(StartTimer());
    }

    private IEnumerator StartTimer()
    {
        while(true)
        {
            int currentSeconds = 0;

            while(currentSeconds != TimeSave)
            {
                currentSeconds++;
                yield return new WaitForSeconds(1f);
            }

            Save();
            Debug.Log("Произошло сохранение данных");
            yield return null;
        }
    }

    private void Save()
    {
        foreach(var saveableEntity in _saveablesEntities)
        {
            saveableEntity.SaveData();
        }
		
		YandexGame.SaveProgress();
    }
}
