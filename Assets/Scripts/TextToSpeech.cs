using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.CognitiveServices.Speech;
using System;
using System.Threading.Tasks;

public class TextToSpeech : MonoBehaviour
{

    SpeechConfig config;
    string speechSynthesisLanguage = "ja-JP";
    string speakText = "Ç±ÇÒÇ…ÇøÇÕÅBÇ∆ÇÊÇ≥Ç´Ç≈Ç∑ÅB";
    string subscriptionKey = "";
    string region = "eastus";


    // Start is called before the first frame update
    void Start()
    {
        //Text To SpeechÇÃê›íË
        config = SpeechConfig.FromSubscription(subscriptionKey, region);
        config.SpeechSynthesisLanguage = speechSynthesisLanguage; 
        config.SetSpeechSynthesisOutputFormat(SpeechSynthesisOutputFormat.Raw16Khz16BitMonoPcm); 

        Debug.Log(config.AuthorizationToken);
    }

    public async void PCtest()
    {
        using var synthesizer = new SpeechSynthesizer(config);
        await synthesizer.SpeakTextAsync("Ç±ÇÒÇ…ÇøÇÕ");
    }

    public async void SynthesizeAudioAsyncDefo()
    {
        try
        {
            using var synthesizer = new SpeechSynthesizer(config, null);
            var result = await synthesizer.SpeakTextAsync(speakText);

            var audioSource = gameObject.AddComponent<AudioSource>();
            var sampleCount = result.AudioData.Length / 2;
            var audioData = new float[sampleCount];
            for (var i = 0; i < sampleCount; ++i)
            {
                audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
            }
            var audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
            audioClip.SetData(audioData, 0);
            audioSource.clip = audioClip;
            audioSource.Play();

            Debug.Log("Success");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }

    public async Task SynthesizeAudioAsync(string speakText_test)
    {
        try
        {
            using var synthesizer = new SpeechSynthesizer(config, null);
            var result = await synthesizer.SpeakTextAsync(speakText_test);

            var audioSource = gameObject.AddComponent<AudioSource>();
            var sampleCount = result.AudioData.Length / 2;
            var audioData = new float[sampleCount];
            for (var i = 0; i < sampleCount; ++i)
            {
                audioData[i] = (short)(result.AudioData[i * 2 + 1] << 8 | result.AudioData[i * 2]) / 32768.0F;
            }
            var audioClip = AudioClip.Create("SynthesizedAudio", sampleCount, 1, 16000, false);
            audioClip.SetData(audioData, 0);
            audioSource.clip = audioClip;
            audioSource.Play();

            Debug.Log("Success");
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }

    }
}

