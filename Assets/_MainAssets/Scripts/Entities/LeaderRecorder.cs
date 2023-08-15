using YG;
using Zenject;
using UnityEngine;
using System.Collections;

public class LeaderRecorder : MonoBehaviour
{
    public string LeaderboardName;

    private Counter _counter;

    [Inject]
    private void Construct(Counter counter)
    {
        _counter = counter;
    }

    private IEnumerator Start()
	{
        while(true)
        {
            SaveLeaderboard();
            yield return new WaitForSeconds(5f);
        }
	}

    private void SaveLeaderboard()
    {
        if(_counter.Count > YandexGame.savesData.MaxCount)
        {
            YandexGame.savesData.MaxCount = _counter.Count;
            YandexGame.NewLeaderboardScores(LeaderboardName, (int)_counter.Count);
            Debug.Log($"Данные отправлены в лидерборд: {LeaderboardName}");
            YandexGame.SaveProgress();
        }
    }
}
