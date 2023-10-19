using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class ChatGPT : MonoBehaviour
{
    private string API_KEY = "";
    [SerializeField] private List<ChatGPTMessageModel> messageList = new List<ChatGPTMessageModel>();

    [Serializable]
    public class ChatGPTMessageModel
    {
        public string role;
        public string content;
    }

    [Serializable]
    public class ChatGPTCompletionRequestModel
    {
        public string model;
        public List<ChatGPTMessageModel> messages;
    }

    [System.Serializable]
    public class ChatGPTResponseModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public ChatGPTMessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        messageList.Add(
            new ChatGPTMessageModel() { role = "system", content = "あなたの名前はトムクルーズです。質問に対して分かりやすく且つ優しく答えてください。" }
            );
    }



    public async UniTask<ChatGPTResponseModel> RequestAsync(string userMessage)
    {

        var apiUrl = "https://api.openai.com/v1/chat/completions";

        messageList.Add(new ChatGPTMessageModel { role = "user", content = userMessage });

        var headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + API_KEY},
                {"Content-type", "application/json"},
            };

        var options = new ChatGPTCompletionRequestModel()
        {
            model = "gpt-3.5-turbo",
            messages = messageList
        };
        var jsonOptions = JsonUtility.ToJson(options);

        Debug.Log("自分:" + userMessage);

        using var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            throw new Exception();
        }
        else
        {
            var responseString = request.downloadHandler.text;
            var responseObject = JsonUtility.FromJson<ChatGPTResponseModel>(responseString);
            Debug.Log("ChatGPT:" + responseObject.choices[0].message.content);
            messageList.Add(responseObject.choices[0].message);
            return responseObject;
        }
    }

    public void RemoveList()
    {
        messageList.RemoveRange(1, messageList.Count - 1);

    }

}

