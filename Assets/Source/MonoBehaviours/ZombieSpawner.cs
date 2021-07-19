using System;

using UnityEngine;


/// <summary>
/// Represent a script for zombie spawning.
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    public const float WaveSizeMultiplier = 1;

    private System.Random random;
    private int zombiesToSpawn;

    public GameObject ZombiePrefab;
    public MazeGenerator Maze;
    public GameObject RewardPicker;
    public Gunner Gunner;
    public float MaxSpeed;
    public float MinSpeed;

    /// <summary>
    /// Current wave number.
    /// </summary>
    public int CurrentWave { get; private set; }

    /// <summary>
    /// Number of reaming alive zombies.
    /// </summary>
    public int ReamingZombies { get; private set; }

    /// <summary>
    /// Occur when wave change.
    /// </summary>
    public event EventHandler OnWaveChange;

    /// <summary>
    /// Occur when zombie count change.
    /// </summary>
    public event EventHandler OnZombieCountChange;

    public ZombieSpawner()
    {
        random = new System.Random();
    }

    private void Start()
    {
        SpawnNextWave();
        OnZombieCountChange?.Invoke(this, new EventArgs());
    }

    private void FixedUpdate()
    {
        if (zombiesToSpawn > 0)
        {
            SpawnZombie();
            zombiesToSpawn--;
        }
    }

    /// <summary>
    /// Spawn next wave of zombies.
    /// </summary>
    private void SpawnNextWave()
    {
        CurrentWave++;
        ReamingZombies = (int)(CurrentWave * WaveSizeMultiplier);
        zombiesToSpawn = ReamingZombies;

        OnWaveChange?.Invoke(this, new EventArgs());
    }

    /// <summary>
    /// Spawn a zombie in random maze corner.
    /// </summary>
    private void SpawnZombie()
    {
        Vector2 position = new Vector2();
        Vector2 tilePos = new Vector2()
        {
            x = random.Next(1, (int)Maze.TileSize - 1) + (float)random.NextDouble(),
            y = random.Next(1, (int)Maze.TileSize - 1) + (float)random.NextDouble(),
        };
        int tile = random.Next(4);


        switch (tile)
        {
            case 0:
                position = new Vector2()
                {
                    x = tilePos.x - Maze.TotalWidth / 2,
                    y = -tilePos.y + Maze.TotalHeight / 2,
                };
                break;
            case 1:
                position = new Vector2()
                {
                    x = tilePos.x - Maze.TotalWidth / 2,
                    y = tilePos.y - Maze.TotalHeight / 2,
                };
                break;
            case 2:
                position = new Vector2()
                {
                    x = -tilePos.x + Maze.TotalWidth / 2,
                    y = -tilePos.y + Maze.TotalHeight / 2,
                };
                break;
            case 3:
                position = new Vector2()
                {
                    x = -tilePos.x + Maze.TotalWidth / 2,
                    y = tilePos.y - Maze.TotalHeight / 2,
                };
                break;
        }

        SpawnZombie(position);
    }

    /// <summary>
    /// Spawn a zombie at specific position.
    /// </summary>
    private void SpawnZombie(Vector2 position)
    {
        var zombie = Instantiate(ZombiePrefab);
        zombie.transform.position = position;

        var pathFinder = zombie.GetComponent<MazePathFinder>();
        pathFinder.Target = GameObject.Find("Player").transform;
        pathFinder.Maze = GameObject.Find("Maze").GetComponent<MazeGenerator>();

        zombie.GetComponent<MazePathFinder>().Speed = random.Next((int)MinSpeed, (int)MaxSpeed) + (float)random.NextDouble();
        zombie.GetComponent<Hitable>().OnDead += Zombie_OnDead;
    }

    private void Zombie_OnDead(object sender, EventArgs e)
    {
        ReamingZombies--;
        if (ReamingZombies <= 0)
        {
            Gunner.enabled = false;
            RewardPicker.SetActive(true);
            SpawnNextWave();
        }

        OnZombieCountChange?.Invoke(this, new EventArgs());
    }
}
