using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    
    private ChatGPT chatGpt;
    private TextToSpeech textToSpeech;

    private async void Start()
    {
        chatGpt = GetComponent<ChatGPT>();
        textToSpeech = GetComponent<TextToSpeech>();

        await ProcessChatAndSpeak("日本の首都は？");
    }

    public async System.Threading.Tasks.Task ProcessChatAndSpeak(string text)
    {
        try
        {
            // ChatGPTの返答を取得
            var chatGptResponse = await chatGpt.RequestAsync(text);
            var responseText = chatGptResponse.choices[0].message.content;

            // TextToSpeechで音声合成して再生
            await textToSpeech.SynthesizeAudioAsync(responseText);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error processing chat and speech: " + ex.Message);
        }
    }
}

