using UnityEngine;


/// <summary>
/// Represent a script for zombie spawning.
/// </summary>
public class ZombieSpawner : MonoBehaviour
{
    public GameObject ZombiePrefab;

    void Start()
    {
        SpawnZombie();
    }

    private void SpawnZombie()
    {
        var zombie = Instantiate(ZombiePrefab);
        zombie.transform.position = Vector3.zero;

        var pathFinder = zombie.GetComponent<MazePathFinder>();
        pathFinder.Target = GameObject.Find("Player").transform;
        pathFinder.Maze = GameObject.Find("Maze").GetComponent<MazeGenerator>();
    }

}
