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

        await ProcessChatAndSpeak("���{�̎�s�́H");
    }

    public async System.Threading.Tasks.Task ProcessChatAndSpeak(string text)
    {
        try
        {
            // ChatGPT�̕ԓ����擾
            var chatGptResponse = await chatGpt.RequestAsync(text);
            var responseText = chatGptResponse.choices[0].message.content;

            // TextToSpeech�ŉ����������čĐ�
            await textToSpeech.SynthesizeAudioAsync(responseText);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error processing chat and speech: " + ex.Message);
        }
    }
}

