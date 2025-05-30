using System;
using _Project.Scripts.Cups;
using UnityEngine;

public class CupHandler : MonoBehaviour
{
    private static int currentLevel = 1;
    [SerializeField] private PlasticCup[] plasticCup;

    private void Start()
    {
        plasticCup = new PlasticCup[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            plasticCup[i] = transform.GetChild(i).gameObject.GetComponent<PlasticCup>();
        }
    }

    private void Update()
    {
        if (AllCupDropped())
        {
            currentLevel++;
            SceneTransitionManager.singleton.GoToSceneAsync(currentLevel);
        }
    }

    private bool AllCupDropped()
    {
        foreach (var cup in plasticCup)
        {
            if (!cup.IsDrop)
                return false;
        }

        return true;
    }
}
