using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text starsText;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        UpdateStarsUI();//TODO Not put inside the Update method later
        AudioManager.Instance.Play("Music");
    }

    public void UpdateStarsUI()
    {
        int sum = 0;

        for(int i = 1; i < 14; i++)
        {
            sum += PlayerPrefs.GetInt("Lv" + i.ToString());//Add the level 1 stars number, level 2 stars number.....
        }

        starsText.text = sum + "/" + 39;
    }
}
