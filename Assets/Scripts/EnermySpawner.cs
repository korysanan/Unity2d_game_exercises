using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = { -2.2f, -1.1f, 0f, 1.1f, 2.2f };

    [SerializeField]
    private float spawnInterval = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        startEnermyRoutine();
    }

    void startEnermyRoutine() {
        StartCoroutine("EnermyRoutine");
    }

    public void stopEnermyRoutine() {
        StopCoroutine("EnermyRoutine");
    }

    IEnumerator EnermyRoutine() {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enermyIndex = 0;

        while (true) {
            foreach (float posX in arrPosX)
            {
                SpawnEnermy(posX, enermyIndex, moveSpeed);
            }

            spawnCount += 1;

            if (spawnCount % 10 == 0) {
                enermyIndex += 1;
                moveSpeed += 2;
            }

            if (enermyIndex >= enemies.Length){
                SpawnBoss();
                enermyIndex = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnermy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0) {
            index ++;
        }

        if (index >= enemies.Length) {
            index = enemies.Length - 1;
        }
        GameObject enermyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enermy enermy = enermyObject.GetComponent<Enermy>();
        enermy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
