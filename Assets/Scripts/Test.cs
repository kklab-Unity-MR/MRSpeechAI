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

        await ProcessChatAndSpeak("“ú–{‚Ìñ“s‚ÍH");
    }

    public async System.Threading.Tasks.Task ProcessChatAndSpeak(string text)
    {
        try
        {
            // ChatGPT‚Ì•Ô“š‚ğæ“¾
            var chatGptResponse = await chatGpt.RequestAsync(text);
            var responseText = chatGptResponse.choices[0].message.content;

            // TextToSpeech‚Å‰¹º‡¬‚µ‚ÄÄ¶
            await textToSpeech.SynthesizeAudioAsync(responseText);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error processing chat and speech: " + ex.Message);
        }
    }
}

