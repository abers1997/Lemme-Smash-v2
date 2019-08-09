using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : MonoBehaviour
{
    [Header("Spawning Objects")]

    [SerializeField]
    private GameObject leftArrow;

    [SerializeField]
    private GameObject downArrow;

    [SerializeField]
    private GameObject upArrow;

    [SerializeField]
    private GameObject rightArrow;

    // Allows for spawning the same generated arrow multiple times in different places
    [Header("Spawning Position")]

    [SerializeField]
    private Vector3[] spawnPositionsLeftArrow;

    [SerializeField]
    private Vector3[] spawnPositionsDownArrow;

    [SerializeField]
    private Vector3[] spawnPositionsUpArrow;

    [SerializeField]
    private Vector3[] spawnPositionsRightArrow;

    private List<GameObject> arrows;
    private Dictionary<GameObject, Vector3[]> arrowToPosMapping;

    private bool beckyHintLargeDelay;
    private float timeToNextSpawn;

    // Start is called before the first frame update
    void Awake()
    {
        arrowToPosMapping = new Dictionary<GameObject, Vector3[]>();
        arrowToPosMapping.Add(leftArrow, spawnPositionsLeftArrow);
        arrowToPosMapping.Add(downArrow, spawnPositionsDownArrow);
        arrowToPosMapping.Add(upArrow, spawnPositionsUpArrow);
        arrowToPosMapping.Add(rightArrow, spawnPositionsRightArrow);

        arrows = new List<GameObject>(arrowToPosMapping.Keys);

        timeToNextSpawn = 1f;
        beckyHintLargeDelay = false;

        StartCoroutine(SpawnArrows());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnArrows(GameObject arrow)
    {
        foreach (var pos in arrowToPosMapping[arrow])
        {
            Instantiate(arrow, pos, Quaternion.identity);
        }
    }

    private IEnumerator SpawnArrows()
    {
        // Initial wait
        yield return new WaitForSeconds(timeToNextSpawn);

        while (true)
        {
            int indexToSpawn = Random.Range(0, arrows.Count);
            var randArrow = arrows[indexToSpawn];
            SpawnArrows(randArrow);

            timeToNextSpawn = Random.Range(0.5f, 1.5f);

            yield return new WaitForSeconds(timeToNextSpawn);
        }
    }
}
