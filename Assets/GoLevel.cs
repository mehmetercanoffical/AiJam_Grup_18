using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoLevel : MonoBehaviour
{
    public string LevelName;
    public void GoLevelLobi()
    {
        LevelManager.Instance.SceneLoader(LevelName);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            GoLevelLobi();
    }



}
