using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Threading.Tasks;

public class IntegratedService : MonoBehaviour
{
    [SerializeField] private GameObject dicationHandler;
    private ChatGPT chatGpt;
    private TextToSpeech textToSpeech;

    [SerializeField] private GameObject TMPgameObject;

    void Start()
    {
        chatGpt = GetComponent<ChatGPT>();
        textToSpeech = GetComponent<TextToSpeech>();
    }

    public void StartRecordingDH()
    {
        dicationHandler.GetComponent<DictationHandler>().StartRecording();
    }

    public void StopRecordingDH()
    {
        dicationHandler.GetComponent<DictationHandler>().StopRecording();

    }

    public async void service()
    {
        string question = TMPgameObject.GetComponent<TextMeshProUGUI>().text;
        await ProcessChatAndSpeak(question);
    }

    public async Task ProcessChatAndSpeak(string text)
    {
        try
        {
            var chatGptResponse = await chatGpt.RequestAsync(text);
            var responseText = chatGptResponse.choices[0].message.content;

            await textToSpeech.SynthesizeAudioAsync(responseText);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error processing chat and speech: " + ex.Message);
        }
    }

    public void DeleteTMP()
    {
        TMPgameObject.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void CallService()
    {
        StartCoroutine(ServiceCoroutine());
    }

    private IEnumerator ServiceCoroutine()
    {
        StopRecordingDH();

        service();
        yield return null;
    }

}