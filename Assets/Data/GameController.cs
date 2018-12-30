using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    //GameController that is user by Unity
    public class GameController : MonoBehaviour
    {
        private ulong galacticTime;

        public Galaxy Galaxy;

        // Use this for initialization
        private void OnEnable()
        {
            Galaxy = new Galaxy();
            Galaxy.Generate(2);
        }

        // Update is called once per frame
        private void Update()
        {
        }

        public void AdvanceTime(int numSeconds)
        {
            galacticTime = galacticTime + (uint) numSeconds;

            Galaxy.Update(galacticTime);
        }
    }
}