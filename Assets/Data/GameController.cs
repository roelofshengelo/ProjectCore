using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    //GameController that is user by Unity
    public class GameController : MonoBehaviour
    {
        private ulong galacticTime;
        private int galacticDay;

        public Galaxy Galaxy;

        // Use this for initialization
        private void OnEnable()
        {
            Galaxy = new Galaxy();
            Galaxy.Generate(1);
        }

        // Update is called once per frame
        private void Update()
        {
        }


        private int _prevSecond = 0;
        private void FixedUpdate()
        {
            if (_prevSecond < (int)Time.time)
            {
                AdvanceTimeDay(10);
                //AdvanceTime(1);
                _prevSecond = (int)Time.time;
            }
        }

        public void AdvanceTimeDay(int numDays)
        {

            ulong daysInSeconds = (24 * 60 * 60) * (uint)numDays;

            galacticTime = galacticTime + (uint)daysInSeconds;

            Galaxy.Update(galacticTime);
            galacticDay += numDays;

            if (galacticDay >= 30)
            {
                // Dirty fix to ensure the current day is no more than 30, should do something with months and years ad stuff...
                // Don't do this to the seconds just yet since they determine the current place of the orbital
                galacticDay = 0;
            }

            //Debug.Log("Current day: " + galacticDay);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }
        public void AdvanceTime(int numSeconds)
        {
            galacticTime = galacticTime + (uint)numSeconds;

            Galaxy.Update(galacticTime);
            //Debug.Log("Current day: " + galacticDay);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }

    }
}