using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] enemyPrefabsOne;
    [SerializeField] GameObject[] enemyPrefabsTwo;
    [SerializeField] GameObject[] enemyPrefabsThree;
    [SerializeField] Transform[] spawners;
    [SerializeField] float castTime;
    [SerializeField] float repeatTime;
    [SerializeField] float castTimeHard;
    [SerializeField] float repeatTimeHard;
    [SerializeField] float RoundTime;
    private int enemyIndex;
    private int spawnerIndex;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("GenerateEnemyOne", castTime, repeatTime);
        StartCoroutine(nameof(ProgressManager));
    }

    // Update is called once per frame
    void Update()
    {
       if (GameManager.Instance.gameOver == true)
        {
            StopCoroutine(nameof(ProgressManager));
        }
    }

    void GenerateEnemyOne()
    {
        if (GameManager.Instance.gameOver == false)
        {
            enemyIndex = Random.Range(0, enemyPrefabsOne.Length - 1);
            spawnerIndex = Random.Range(0, spawners.Length);
            Instantiate(enemyPrefabsOne[enemyIndex], spawners[spawnerIndex].position, Quaternion.identity);
            //Instantiate(prefab, posicion a generar, rotación con la que se genera)
        }

    }

    void GenerateEnemyTwo()
    {
        if (GameManager.Instance.gameOver == false)
        {
            enemyIndex = Random.Range(0, enemyPrefabsTwo.Length - 1);
            spawnerIndex = Random.Range(0, spawners.Length - 1);
            Instantiate(enemyPrefabsTwo[enemyIndex], spawners[spawnerIndex].position, Quaternion.identity);
            //Instantiate(prefab, posicion a generar, rotación con la que se genera)
        }

    }

    void GenerateEnemyThree()
    {
        if (GameManager.Instance.gameOver == false)
        {
            enemyIndex = Random.Range(0, enemyPrefabsThree.Length - 1);
            spawnerIndex = Random.Range(0, spawners.Length - 1);
            Instantiate(enemyPrefabsThree[enemyIndex], spawners[spawnerIndex].position, Quaternion.identity);
            //Instantiate(prefab, posicion a generar, rotación con la que se genera)
        }

    }

    IEnumerator ProgressManager()
    {
        yield return new WaitForSeconds(3f);
        InvokeRepeating("GenerateEnemyOne", castTime, repeatTime);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        InvokeRepeating("GenerateEnemyTwo", castTime, repeatTime);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        InvokeRepeating("GenerateEnemyThree", castTimeHard, repeatTimeHard);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        GameManager.Instance.gameCompleted = true;
        yield return null;
    }
}
