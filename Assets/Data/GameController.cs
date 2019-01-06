using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    //GameController that is used by Unity
    public class GameController : MonoBehaviour
    {
        private ulong galacticTime;
        private int galacticDay = 1;
        private int galacticMonth = 1;

        public Galaxy Galaxy;

        // Use this for initialization
        private void OnEnable()
        {
            Galaxy = new Galaxy();

            // For now, generate a galaxy with 1 star
            Galaxy.Generate(1);
        }

        // Update is called once per frame
        private void Update()
        {
        }


        private int _prevSecond = 0;
        private void FixedUpdate()
        {
            if (_prevSecond >= (int)Time.time) return;
            AdvanceTimeDay(10);
            //AdvanceTimeMonth(6);
            //AdvanceTime(1);
            _prevSecond = (int)Time.time;
        }

        public void AdvanceTimeDay(int numDays)
        {
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth);
            //Debug.Log("Current galacticTime: " + galacticTime);

            ulong daysInSeconds = (24 * 60 * 60) * (uint)numDays;

            galacticTime = galacticTime + (uint)daysInSeconds;

            Galaxy.Update(galacticTime);
            galacticDay += numDays;

            if (galacticDay > 30)
            {
                // Dirty fix to ensure the current day is no more than 30, should do something with months and years ad stuff...
                // Don't do this to the seconds just yet since they determine the current place of the orbital
                galacticDay = 1;
                galacticMonth++;
                return;
            }
        }

        public void AdvanceTimeMonth(int numMonths)
        {
            AdvanceTimeDay(numMonths * 30);
        }


        public void AdvanceTime(int numSeconds)
        {
            galacticTime = galacticTime + (uint)numSeconds;

            Galaxy.Update(galacticTime);
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }

    }
}