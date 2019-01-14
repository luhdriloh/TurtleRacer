using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool _anyoneCrossedYet;

    private void Start()
    {
        _anyoneCrossedYet = false;
        transform.position = new Vector2(0f, LevelSelectInformation._levelSelectInformationInstance.GetLevelSelected()._raceDistance);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            LevelData data = LevelSelectInformation._levelSelectInformationInstance.GetLevelSelected();

            if (_anyoneCrossedYet == false)
            {
                PlayerPrefs.SetInt(data._difficulty.ToString() + "win", 1);
                GameController._gamecontroller.RaceResult("You are the winner");
            }
            else
            {
                GameController._gamecontroller.RaceResult("You lost");
            }

            float timeToFinish = Time.time - GameController._gamecontroller.RaceStartTime();

            float previousBestTime = PlayerPrefs.GetFloat(data._difficulty.ToString(), 0);
            Debug.Log("previous best time: " + previousBestTime);
            if (previousBestTime <= 0 || timeToFinish < previousBestTime)
            {
                PlayerPrefs.SetFloat(data._difficulty.ToString(), timeToFinish);
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Opponent"))
        {
            if (_anyoneCrossedYet == false)
            {
                _anyoneCrossedYet = true;
            }
        }
    }
}
