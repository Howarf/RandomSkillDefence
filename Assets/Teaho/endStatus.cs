using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endStatus : MonoBehaviour
{
    public GameObject win, lose;
    public void Update()
    {
        if (GameManager.manager.gameOver)
        {
            win.SetActive(true);
            lose.SetActive(false);
        }
        else
        {
            win.SetActive(false);
            lose.SetActive(true);
        }
    }
}
