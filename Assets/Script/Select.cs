using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    public static bool selected = false;

    private static int k = 1;
    // Update is called once per frame
    public static IEnumerator Selecting(int max)
    {
        GameObject[] Bar = new GameObject[5];

        for (int i = 1; i <= max; i++)
        {
            string name = "Select" + i.ToString();

            Bar[i] = GameObject.Find(name);
        }

        while (true)
        {
            for (int i = 1; i <= max; i++)
            {
                if (i == k) Bar[i].GetComponent<Animator>().Play("Highlight");
                else Bar[i].GetComponent<Animator>().Play("ResetColor");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (k > 1) k--;
                Debug.Log("Cursor : " + k);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (k < max) k++;
                Debug.Log("Cursor : " + k);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                selected = true;
                break;
            }
            yield return null;
        }
        Debug.Log("Selected");
    }
}
