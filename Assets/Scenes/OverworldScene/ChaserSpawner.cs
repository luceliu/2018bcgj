using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class ChaserSpawner : MonoBehaviour
    {
        //TODO spawner config
        public GameObject ChaserPrefab;
        public float SpawnInterval;
        public int SpawnMaximum;
        public float DestroyInterval;

        public ZoneController Zone;

        private float elapsed;

        // Use this for initialization
        void Start()
        {
            if (Zone == null)
            {
                Debug.LogError("ChaserSpawner on " + gameObject.name + " has no Zone!");
            }
        }

        // Update is called once per frame
        void Update()
        {
            int numChasers = transform.childCount;

            if (Zone.PlayerInZone)
            {             
                if(elapsed >= SpawnInterval && numChasers < SpawnMaximum)
                {
                    var go = Instantiate<GameObject>(ChaserPrefab, transform, false);
                    go.transform.position = transform.position;
                    elapsed = 0;
                }
            }
            else
            {
                //destroy chasers
                if(elapsed >= DestroyInterval && numChasers > 0)
                {
                    Destroy(transform.GetChild(0).gameObject);
                    elapsed = 0;
                }
                
            }

            elapsed += Time.deltaTime;
        }
    }
}