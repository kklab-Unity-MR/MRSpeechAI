using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;
using UnityEngine.UI;

public class CreatScrollViewMessage : MonoBehaviour
{

    private TextMeshProUGUI text;

    [SerializeField] private List<string> allLogs;
    private int maxTextData = 20;
    private StringBuilder textStringBuilder;

    //public Scrollbar verticalScrollbar;
    //public ScrollRect scrollRect;

    public void AddText(string textData)
    {
        allLogs.Add(textData);
        if (allLogs.Count > maxTextData)
        {
            allLogs.RemoveRange(0, allLogs.Count - maxTextData);
        }

        textStringBuilder.Clear();

        foreach (var tx in allLogs)
        {
            textStringBuilder.Append(Environment.NewLine + tx);
        }
        text.text = textStringBuilder.ToString().TrimStart();

        //verticalScrollbar.value = 1.0f;
        //scrollRect.verticalNormalizedPosition = 0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        //verticalScrollbar.value = 1.0f;
        allLogs = new List<string>();
        textStringBuilder = new StringBuilder();

        text = gameObject.GetComponent<TextMeshProUGUI>();

    }
}
