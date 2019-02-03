using System;
using System.Collections.Generic;
using System.IO;
using Assets.Data.Models;
using UnityEngine;
using Object = UnityEngine.Object;

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

            daysPastSinceStart = result;
            return result;
        }

        internal static Object[] Stars()
        {
            var test = Resources.LoadAll("Stars/", typeof(Sprite));





            return test;
        }

        private ulong daysPastSinceStart = 0;

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
        private int galacticYear = 1; // Could set this to 4000? Or over 9000!!!

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

        private int prevSecond = 0;
        private float prevTime = 0.0f;
        private void FixedUpdate()
        {
            //Setting the starting timings
            StartupGalaxy();

            //Redraw the sprites to ensure a smooth transition
            UpdateGalaxy(prevTime);


            prevTime = Time.time;
            //Update the galactic calender to pass 1 day every second
            if (prevSecond < (int)Time.time)
            {
                AdvanceGalaxyTime(1);
                prevSecond = (int)Time.time;
                prevTime = prevSecond;
            }

        }


        private void StartupGalaxy()
        {
            if (!prevSecond.Equals(0)) return;
            galacticTime = daysPastSinceStart * (ulong)(HoursInDay * MinutesInHours * SecondsInMinutes);
            UpdateGalaxy(galacticTime);
            prevSecond = (int)Time.time;
            prevTime = prevSecond;
        }



        /// <summary>
        /// Advance the time of the entire galaxy by number of days.
        /// </summary>
        /// <param name="numDays"></param>
        public void AdvanceGalaxyTime(int numDays)
        {
            ulong daysInSeconds = (ulong)(SecondsInMinutes * MinutesInHours * HoursInDay) * (ulong)numDays;
            SetGalaxyDate(daysInSeconds);
            galacticDay += numDays;

            if (galacticDay > DaysInMonth)
            {
                // Dirty fix to ensure the current month doesn't have more than 30 days, should do something with months and years...
                // Don't do this to the seconds just yet since they determine the current place of the orbital
                galacticDay = 1;
                galacticMonth++;
            }

            if (galacticMonth > MonthsInYear)
            {
                // Did a dirty fix to ensure the current year has no more than 12 months, should do something with the days as wel...
                galacticDay = 1;
                galacticMonth = 1;
                galacticYear++;
            }
        }

        public void AdvanceTimeMonth(int numMonths)
        {
            AdvanceGalaxyTime(numMonths * DaysInMonth);
        }

        /// <summary>
        /// Really, in seconds? Maybe for RTS on planets or something
        /// </summary>
        /// <param name="numSeconds"></param>
        public void SetGalaxyDate(ulong numSeconds)
        {
            galacticTime = galacticTime + numSeconds;
            Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear);
            //Debug.Log("Current galacticTime: " + galacticTime);
        }

        private void UpdateGalaxy(float timelapsed)
        {
            //Debug.Log("===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear + " (= timelapsed " + timelapsed + " / galacticTime " + galacticTime + ")");


            Galaxy.Update(timelapsed);

            //var timeInDays = (float)timelapsed * SecondsInMinutes * MinutesInHours * HoursInDay; // I think?

            //Debug.Log(timelapsed + "===== Current day: " + galacticDay + " of month: " + galacticMonth + " of year: " + galacticYear + " (= total seconds " + timeInDays + ")");

            //timeInDays = timeInDays / 100000; // Gameplay speed?
            //timeInDays = timeInDays / 1000; // I don't like to wait while I'm testing orbital speeds, ratios, and the like.
            //Galaxy.Update(timeInDays); // Way way to fast

            // Advance time by 1 day every 0.x frames?
            //Galaxy.Update(SecondsInMinutes * MinutesInHours * HoursInDay);
        }

    }
}