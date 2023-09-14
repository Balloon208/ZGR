using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FunctionList : MonoBehaviour
{
    public static void Hi(string s1, string s2)
    {
        Debug.Log(s1 + " is " + s2);
    }

    public static void Select2(string s1, string s2)
    {
        GameObject.Find("Barlist").transform.Find("2_Bar").gameObject.SetActive(true);

        GameObject obj1text = GameObject.FindWithTag("Select1");
        GameObject obj2text = GameObject.FindWithTag("Select2");

        obj1text.GetComponent<Text>().text = s1;
        obj2text.GetComponent<Text>().text = s2;
    }

    public static void Select4(string s1, string s2, string s3, string s4)
    {
        GameObject.Find("Barlist").transform.Find("4_Bar").gameObject.SetActive(true);

        GameObject obj1text = GameObject.FindWithTag("Select1");
        GameObject obj2text = GameObject.FindWithTag("Select2");
        GameObject obj3text = GameObject.FindWithTag("Select3");
        GameObject obj4text = GameObject.FindWithTag("Select4");

        obj1text.GetComponent<Text>().text = s1;
        obj2text.GetComponent<Text>().text = s2;
        obj3text.GetComponent<Text>().text = s3;
        obj4text.GetComponent<Text>().text = s4;
    }
}
