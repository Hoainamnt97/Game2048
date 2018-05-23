using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class TilePlayer2 : MonoBehaviour {
    
    private int number;
    public int Row;
    public int Column;
    private Text TileTextNumber;
    private Text TileTextColor;
    private Image TileImage;

    void Awake()
    {
        TileTextNumber = transform.Find("TileNum").GetComponentInChildren<Text>();
        TileTextColor = transform.Find("TileNum").GetComponentInChildren<Text>();
        TileImage = transform.Find("TileNum").GetComponent<Image>();

    }

    public void ApplyTileStyle(int index) //method để chỉnh màu theo số
    {
        TileTextNumber.text = Style.Instance.StyleData[index].number.ToString();
        TileTextColor.color = Style.Instance.StyleData[index].TextColor;
        TileImage.color = Style.Instance.StyleData[index].BGColor;
    }

    public void Invisible()
    {
        TileTextNumber.enabled = false;
        TileTextColor.enabled = false;
        TileImage.enabled = false;
    }

    public void Visible(int num)
    {
        TileTextNumber.enabled = true;
        TileTextColor.enabled = true;
        TileImage.enabled = true;
        ApplyTileStyle(num-1);
    }

    //Its like OOP but in the C# way
    public int Appear
    {
        get
        {
            return number;
        }
        set
        {
            //value means the value due to each cases, In java, it's like an integer when we have to call it in the set method, in C#, it's just "value"
            number = value;
            if (number == 0)
            {
                Invisible();
            }
            else
            {
                Visible(number);

            }
        }

    }

  
}
