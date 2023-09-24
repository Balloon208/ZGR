using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    public enum Mode
    {
        Text,
        Select2,
        Select4
    };
    public Text nameline;
    public Text textline;
    [HideInInspector]
    public bool coroutine_lock = false;
    [HideInInspector]
    public int cursor;

    // Start is called before the first frame update

    public IEnumerator ShowText(string name, string text, bool textlock)
    {
        nameline.text = name;
        coroutine_lock = true;
        int maxlength = text.Length;
        float speed = 0.1f;

        string temptext = "";

        for (int j = 0; j < maxlength; j++)
        {
            temptext += text[j];

            textline.text = temptext;

            Debug.Log(textline.text);

            if (j >= 3 && Input.GetKey(KeyCode.Space)) speed = 0.02f;

            yield return new WaitForSeconds(speed);
        }
        if(textlock) yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        coroutine_lock = false;
    }

    // Update is called once per frame
    public IEnumerator Selecting(int max, params string[] s)
    {
        cursor = 1;
        coroutine_lock = true;

        if(max == 2) GameObject.Find("Barlist").transform.Find("2_Bar").gameObject.SetActive(true);
        if(max == 4) GameObject.Find("Barlist").transform.Find("4_Bar").gameObject.SetActive(true);

        GameObject[] Bar = new GameObject[5];

        for (int i = 1; i <= max; i++)
        {
            string name = "Select" + i.ToString();
            Bar[i] = GameObject.Find(name);

            Bar[i].transform.GetChild(0).GetComponent<Text>().text = s[i-1];
        }

        while (true)
        {
            for (int i = 1; i <= max; i++)
            {
                if (i == cursor) Bar[i].GetComponent<Animator>().Play("Highlight");
                else Bar[i].GetComponent<Animator>().Play("ResetColor");
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (cursor > 1) cursor--;
                Debug.Log("Cursor : " + cursor);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (cursor < max) cursor++;
                Debug.Log("Cursor : " + cursor);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                coroutine_lock = false;
                break;
            }
            yield return null;
        }

        Bar[cursor].GetComponent<Animator>().Play("ResetColor");
        Debug.Log("Selected");

        if (max == 2)GameObject.Find("2_Bar").gameObject.SetActive(false);
        if (max == 4) GameObject.Find("4_Bar").gameObject.SetActive(false);
    }
}
