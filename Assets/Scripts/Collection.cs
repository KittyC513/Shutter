using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Collection : MonoBehaviour
{

    public int collection;
    public int numOfC;

    public Image[] collections;
    public Sprite found;
    public Sprite unfound;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (collection <= 0)
        {
            wins();
        }

        if (collection > numOfC)
        {
            collection = numOfC;
        }
        for (int i = 0; i < collections.Length; i++)
        {
            if (i < collection)
            {
                collections[i].sprite = unfound;
            }
            else
            {
                collections[i].sprite = found;
            }
            if (i < numOfC)
            {
                collections[i].enabled = true;
            }
            else
            {
                collections[i].enabled = false;
            }
        }
    }

    void wins()
    {
        SceneManager.LoadScene(2);
    }
}
