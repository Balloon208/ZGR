using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private Object scriptfile;
    protected virtual IEnumerator Fullshow()
    {
        yield return null;
    }

    private void Start()
    {
        Debug.Log(scriptfile.GetType());
    }
    protected void StartScript()
    {
        StartCoroutine(Fullshow());
    }
}
