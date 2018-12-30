using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        Galaxy = new Galaxy();
        Galaxy.Generate(2);
	}

    public Galaxy Galaxy;
	
	// Update is called once per frame
	void Update () {
	
	}

    ulong galacticTime = 0;

    public void AdvanceTime(int numSeconds)
    {
        galacticTime = galacticTime + (uint)numSeconds;

        Galaxy.Update(galacticTime);
    }

}
