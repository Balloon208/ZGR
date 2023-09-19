using System.Collections;
using System.Collections.Generic;
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

    [System.Serializable]
    public class Scripts
    {
        public Mode mode;
        public string name;
        [Multiline(3)]
        public string text;
    }

    public Text nameline;
    public Text textline;
    [Tooltip("Text - Name 과 Text 작성\nFunction - Name에 함수 이름 작성, Text에 매개변수 입력\n* 매개변수는 ',' 로 파싱")]
    public Scripts[] scripts;
    private bool coroutine_lock = false;

    // Start is called before the first frame update

    public IEnumerator ShowText(string text)
    {
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
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        coroutine_lock = false;
    }

    IEnumerator Fullshow()
    {
        for (int i = 0; i < scripts.Length; i++)
        {
            Debug.Log(i);
            if (scripts[i].mode == Mode.Text)
            {
                nameline.text = scripts[i].name;

                string textp = scripts[i].text;

                StartCoroutine(ShowText(textp));
                yield return new WaitUntil(() => coroutine_lock==false);
            }
            if (scripts[i].mode == Mode.Select2)
            {
                GameObject.Find("Barlist").transform.Find("2_Bar").gameObject.SetActive(true);

                Text t1 = GameObject.FindWithTag("Select1").GetComponent<Text>();
                Text t2 = GameObject.FindWithTag("Select2").GetComponent<Text>();

                StartCoroutine(Select.Selecting(2));
                yield return new WaitUntil(() => Select.selected == true);
            }
            if (scripts[i].mode == Mode.Select4)
            {
                GameObject.Find("Barlist").transform.Find("4_Bar").gameObject.SetActive(true);

                Text t1 = GameObject.FindWithTag("Select1").GetComponent<Text>();
                Text t2 = GameObject.FindWithTag("Select2").GetComponent<Text>();
                Text t3 = GameObject.FindWithTag("Select3").GetComponent<Text>();
                Text t4 = GameObject.FindWithTag("Select4").GetComponent<Text>();

                StartCoroutine(Select.Selecting(4));
                yield return new WaitUntil(() => Select.selected == true);
            }
        }

        Debug.Log("End");
    }

    public void StartScript()
    {
        StartCoroutine(Fullshow());
    }
}
