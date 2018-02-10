using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Overworld
{

    public class ChaserSpawner : MonoBehaviour
    {
        //TODO spawner config

        public ZoneController Zone;

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

        }
    }
}