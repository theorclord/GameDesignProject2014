using UnityEngine;
using System.Collections;

public class TutorialAI : MonoBehaviour {

    private GameObject AICastle;

    private Player ai;

    private int nextUnit;
    private int nextPath;
    private bool isWaiting;

    // Use this for initialization
    void Start()
    {
        AICastle = GameObject.Find("CastleEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (isWaiting)
        {
            if (ai.Technology.Contains(ai.unitTypeList[nextUnit].Tech))
            {
                if (ai.Resources >= ai.unitTypeList[nextUnit].Price)
                {
                    AICastle.GetComponent<TutorialSpawnPoint>().addState(new SpawnPair(nextPath, ai.unitTypeList[nextUnit], 1, ai));
                    ai.Resources -= ai.unitTypeList[nextUnit].Price;
                    isWaiting = false;
                }
            }
            else
            {
                isWaiting = false;
            }
        }
        else
        {
            isWaiting = true;
            nextUnit = Random.Range(0, ai.unitTypeList.Count);
            nextPath = Random.Range(0, 2);
        }


    }

    public void SetAI(Player ai)
    {
        this.ai = ai;

    }
}
