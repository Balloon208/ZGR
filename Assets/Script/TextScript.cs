using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TextScript : MonoBehaviour
{
    public enum Mode
    {
        Text,
        Function
    };

    [System.Serializable]
    public class Scripts
    {
        public Mode mode;
        public string name;
        public string text;
    }

    public Text nameline;
    public Text textline;
    [Tooltip ("Text - Name �� Text �ۼ�\nFunction - Name�� �Լ� �̸� �ۼ�, Text�� �Ű����� �Է�\n* �Ű������� ',' �� �Ľ�")]
    public Scripts[] scripts = new Scripts[5];
    

    // Start is called before the first frame update

    public void StartFunction(string functionname, string inspector)
    {
        string[] splitlist = inspector.Split(',');

        FunctionList functionlist = new FunctionList();
        Type type = functionlist.GetType();

        type.GetMethod(functionname)?.Invoke(functionlist, splitlist);
    }

    IEnumerator fullshow()
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            if (scripts[i].mode == Mode.Text)
            {
                nameline.text = scripts[i].name;

                string textp = scripts[i].text;
                int maxlength = textp.Length;
                float speed = 0.1f;

                string temptext = "";

                for (int j = 0; j < maxlength; j++)
                {
                    temptext += textp[j];

                    textline.text = temptext;

                    Debug.Log(textline.text);

                    if (j>=3 && Input.GetKey(KeyCode.Space)) speed = 0.02f;

                    yield return new WaitForSeconds(speed);
                }
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            else if (scripts[i].mode == Mode.Function)
            {
                StartFunction(scripts[i].name, scripts[i].text);
            }
        } 
    }

    public void StartScript()
    {
        StartCoroutine(fullshow());
    }
}
