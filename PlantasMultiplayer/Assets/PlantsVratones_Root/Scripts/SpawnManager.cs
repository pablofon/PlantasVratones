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
    [SerializeField] GameObject round1;
    [SerializeField] GameObject round2;
    [SerializeField] GameObject round3;



    // Start is called before the first frame update
    void Start()
    {
        round1.SetActive(false);
        round2.SetActive(false);
        round3.SetActive(false);

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
        round1.SetActive(true);
        AudioManager.instance.PlaySFX(1);
        yield return new WaitForSeconds(3f);
        round1.SetActive(false);
        InvokeRepeating("GenerateEnemyOne", castTime, repeatTime);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(3f);
        round2.SetActive(true);
        AudioManager.instance.PlaySFX(2);
        yield return new WaitForSeconds(5f);
        round2.SetActive(false);
        InvokeRepeating("GenerateEnemyTwo", castTime, repeatTime);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(3f);
        round3.SetActive(true);
        AudioManager.instance.PlaySFX(3);
        yield return new WaitForSeconds(5f);
        round3.SetActive(false);
        InvokeRepeating("GenerateEnemyThree", castTimeHard, repeatTimeHard);
        yield return new WaitForSeconds(RoundTime);
        CancelInvoke();
        yield return new WaitForSeconds(5f);
        GameManager.Instance.gameCompleted = true;
        yield return null;
    }
}
