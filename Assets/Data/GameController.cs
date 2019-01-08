using System;
using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    //GameController that is used by Unity
    public class GameController : MonoBehaviour
    {

        ///// <summary>
        ///// Amount of days past since game start (including time skips)
        ///// </summary>
        //public ulong DaysPastSinceStart => (ulong)(galacticDay + (DaysInMonth * galacticMonth - 1) + (DaysInMonth * (galacticMonth - 1) * (galacticYear - 1)));


        /// <summary>
        /// Amount of days past since game start (including time skips)
        /// </summary>
        public ulong DaysPastSinceStart()
        {
            // Start with the current day of the current month.
            var result = (ulong)galacticDay;

            // Than add days for all the months of the current year
            if (galacticMonth > 1) result += (ulong)(DaysInMonth * (galacticMonth - 1)); // don't count all the days of the current month, this is already done

            // Than add days for all the years that have past
            if (galacticYear > 1)
            {
                // don't count all the days of the current year, this is already done
                result += (ulong)((DaysInMonth * MonthsInYear) * (galacticYear - 1));
            }
            return result;
        }


        public int DaysInMonth = 30;
        public int MonthsInYear = 12;
        public int HoursInDay = 24;
        public int MinutesInHours = 60;
        public int SecondsInMinutes = 60;

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
        private float _prevTime = 0.0f;
        private void FixedUpdate()
        {
            AdvanceTimeForSprites(_prevTime);
            _prevTime = Time.time;

            if (_prevSecond >= (int)Time.time) return;

            AdvanceTimeDay(1);
            //AdvanceTimeMonth(6);
            //AdvanceTime(1);
            _prevSecond = (int)Time.time;
            _prevTime = _prevSecond;
        }

        /// <summary>
        /// Advance the time of the entire galaxy by number of days.
        /// </summary>
        /// <param name="numDays"></param>
        public void AdvanceTimeDay(int numDays)
        {
            ulong daysInSeconds = (24 * 60 * 60) * (uint)numDays;

            AdvanceTime(daysInSeconds);

            galacticDay += numDays;

            if (galacticMonth > 12)
            {
                // Did a dirty fix to ensure the current year has no more than 12 months, should do something with the days as wel...
                galacticDay = 1;
                galacticMonth = 1;
                galacticYear++;
                return;
            }

            if (galacticDay > DaysInMonth)
            {
                // Dirty fix to ensure the current month doesn't have more than 30 days, should do something with months and years...
                // Don't do this to the seconds just yet since they determine the current place of the orbital
                galacticDay = 1;
                galacticMonth++;
                return;
            }
        }

        public void AdvanceTimeMonth(int numMonths)
        {
            AdvanceTimeDay(numMonths * DaysInMonth);
        }

        /// <summary>
        /// Really, in seconds? Maybe for RTS on planets or something
        /// </summary>
        /// <param name="numSeconds"></param>
        public void AdvanceTime(ulong numSeconds)
        {
            galacticTime = galacticTime + numSeconds;

            // Is now handled by AdvanceTimeForSprites()
            //Galaxy.Update(galacticTime);
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }

        private void AdvanceTimeForSprites(float timelasped)
        {
            var timeInDays = (float)timelasped * SecondsInMinutes * MinutesInHours * HoursInDay; // I think?
            //timeInDays = timeInDays / 100000; // Gameplay speed?
            timeInDays = timeInDays / 1000; // I don't like to wait while I'm testing orbital speeds, ratios, and the like.


            Galaxy.Update(timeInDays);
        }

    }
}