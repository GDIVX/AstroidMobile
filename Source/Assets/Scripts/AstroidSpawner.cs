using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroidSpawner : MonoBehaviour
{
    public GameObject astroidPrefab;
    public float intervals = 5f;
    public int astroidsToSpawn = 3;
    public int astoridsAddedPerWave = 2;
    public float spread;

    public float minSpeed;
    public float maxSpeed;
    public float minScale;
    public float maxScale;

    [SerializeField]
    private int astroidsInGame = 0;

    public void SpawnAstroid(Vector3 position, Quaternion rotation, float scale, float speed)
    {
        Vector3 spawnPosition = rotation * position.normalized * -speed * spread;

        GameObject astroid = Instantiate(astroidPrefab, spawnPosition, rotation);

        astroid.transform.localScale *= scale;
        var controller = astroid.GetComponent<AstroidController>();

        controller.speed = speed;
        controller.onRemoved.AddListener(HandleOnAstroidRemoved);

        var distance = Vector3.Distance(position * speed * spread, spawnPosition) * 2.5f;
        controller.maxTravelDistance = distance;

        astroidsInGame++;
    }

    [ContextMenu("Spawn Astroid")]
    public void SpawnAtRandom()
    {
        var worldPort = Camera.main.rect;
        var min = Camera.main.ViewportToWorldPoint(new Vector3(worldPort.x, worldPort.y, 0));
        var max = Camera.main.ViewportToWorldPoint(new Vector3(worldPort.width, worldPort.height, 0));

        var randPosition = new Vector3(Random.Range(min.x, max.x), Random.Range(max.x, max.y));

        SpawnAstroid(randPosition, Random.rotation, Random.Range(minScale, maxScale), Random.Range(minSpeed, maxSpeed));
    }

    private void Start()
    {
        StartSpawningIntervals();
    }

    public void StartSpawningIntervals()
    {
        Wait(intervals);
        for (int i = 0; i < astroidsToSpawn; i++)
        {
            SpawnAtRandom();
        }

    }

    private void HandleOnAstroidRemoved(AstroidController astroid)
    {
        if (astroid != null)
        {
            astroidsInGame--;
        }

        if (GameManager.instance.state == GameState.Running && astroidsInGame <= 0)
        {
            astroidsToSpawn += astoridsAddedPerWave;
            StartSpawningIntervals();
        }
    }

    private IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
