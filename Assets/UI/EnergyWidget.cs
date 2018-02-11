using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Overworld
{

    public class EnergyWidget : MonoBehaviour
    {

        public Slider EnergyBar;
        public Image EnergyIndicator;

        void Start()
        {
            //set indicator position
            float totalWidth = 256; //fuckit
            float newCenter = (GameData.PlayerSleepThresholdFrac - 0.5f) * totalWidth;
            EnergyIndicator.transform.localPosition = new Vector3(newCenter, 0, 0);

        }


        void Update()
        {
            //set health value
            float energyFrac = GameData.Instance.PlayerEnergy / GameData.PlayerMaxEnergy;
            EnergyBar.value = energyFrac;
        }
    }
}