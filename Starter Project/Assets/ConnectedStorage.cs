using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectedStorage : MonoBehaviour
{
    public Text StoredValueText;
    public Text RandomValueText;
    
    int randomValue;
    
    public void Init()
    {
    }

    public void GenerateRandomValue()
    {
        randomValue = Random.Range(0, 100);
        RandomValueText.text = randomValue.ToString();
    }

    public void Save()
    {
    }

    public void Load()
    {
    }
}
