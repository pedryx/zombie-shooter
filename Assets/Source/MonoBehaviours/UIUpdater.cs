using System;

using UnityEngine;
using UnityEngine.UI;


class UIUpdater : MonoBehaviour
{

    public ZombieSpawner Spawner;
    public GameObject WaveCounter;
    public GameObject ZombieCounter;

    private void Start()
    {
        UpdateWave();
        UpdateCount();
        Spawner.OnWaveChange += Spawner_OnWaveChange;
        Spawner.OnZombieCountChange += Spawner_OnZombieCountChange;
    }

    private void UpdateCount()
    {
        ZombieCounter.GetComponent<Text>().text = Spawner.ReamingZombies.ToString();
    }

    private void UpdateWave()
    {
        WaveCounter.GetComponent<Text>().text = Spawner.CurrentWave.ToString();
    }

    private void Spawner_OnWaveChange(object sender, EventArgs e)
        => UpdateWave();

    private void Spawner_OnZombieCountChange(object sender, EventArgs e)
        => UpdateCount();
}
