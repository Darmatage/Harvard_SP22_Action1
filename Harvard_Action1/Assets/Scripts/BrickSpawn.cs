
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BrickSpawn : MonoBehaviour {

      //Object variables
      public GameObject brickPrefab;
      public Transform[] spawnPoints;
      private int rangeEnd;
      private Transform spawnPoint;

      //Timing variables
      public float spawnRangeStart = 0.2f;
      public float spawnRangeEnd = 0.8f;
      private float timeToSpawn;
      private float spawnTimer = 0f;

      void Start(){
              //assign the length of the array to the end of the random range
             rangeEnd = spawnPoints.Length;
       }

      void FixedUpdate(){
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                  spawnBrick();
                  spawnTimer =0f;
            }
      }

      void spawnBrick(){
            int SPnum = Random.Range(0, rangeEnd);
            spawnPoint = spawnPoints[SPnum];
            Instantiate(brickPrefab, spawnPoint.position, Quaternion.identity);
      }
}