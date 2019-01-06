using System;
using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    //GameController that is used by Unity
    public class GameController : MonoBehaviour
    {

        /// <summary>
        /// Amount of days past since game start (including time skips)
        /// </summary>
        public ulong DaysPastSinceStart => (ulong)(galacticDay + (30 * galacticMonth - 1) + (30 * (galacticMonth - 1) * (galacticYear - 1)));

        /// <summary>
        /// How many seconds past since the start
        /// </summary>
        private ulong galacticTime;
        private int galacticDay = 1;
        private int galacticMonth = 1;
        private int galacticYear = 1;

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
            AdvanceTimeDay(1);
            //AdvanceTimeMonth(6);
            //AdvanceTime(1);
            _prevSecond = (int)Time.time;
        }

        public void AdvanceTimeDay(int numDays)
        {
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear);
            //Debug.Log("Current galacticTime: " + galacticTime);

            ulong daysInSeconds = (24 * 60 * 60) * (uint)numDays;

            //galacticTime = galacticTime + (uint)daysInSeconds;

            //Galaxy.Update(galacticTime);
            AdvanceTime(daysInSeconds);

            galacticDay += numDays;

            if (galacticMonth > 12)
            {
                galacticDay = 1;
                galacticMonth = 1;
                galacticYear++;
                return;
            }

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

        /// <summary>
        /// Really, in seconds? Maybe for RTS on planets or something
        /// </summary>
        /// <param name="numSeconds"></param>
        public void AdvanceTime(ulong numSeconds)
        {
            galacticTime = galacticTime + numSeconds;

            Galaxy.Update(galacticTime);
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }

    }
}