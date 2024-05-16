using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Data;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    public string[] sentences1;
    public string[] sentences2;

    public string nextScene;
    public int index = 0;
    public float typingSpeed;
    public Animator textDisplayAnim;
    //private AudioSource textAudioSource;


    private void Start()
    {
        //textAudioSource = GetComponent<AudioSource>();
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Dialogue")
        {
            PrintSentences1();
            nextScene = "Island";
        }
        else if (currentScene == "EndGame")
        {
            PrintSentences2();
            nextScene = "StartGame";
        }
    }
    public void Update()
    {
        if(sentences != null && index >= 0 && index < sentences.Length)
        {
        if (Input.anyKeyDown)
            {
                if (textDisplay.text == sentences[index])
                {

                    NextSentence();
                }
            }
            
        
        }

    }
    IEnumerator Type(int index)
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);

        }

    }

    public void NextSentence()
    {
        //textAudioSource.Play();
        textDisplayAnim.SetTrigger("Change");
        if (index < sentences.Length -1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type(index));
            
        }
        else
        {
            textDisplay.text = "";
            EndSentence();
        }
    }

    public void PrintSentences1()
    {
        sentences = sentences1;
        index = 0;
        textDisplayAnim.SetTrigger("Change");
        StartCoroutine(Type(index));


    }

    public void PrintSentences2()
    {
        sentences = sentences2;
        index = 0;
        textDisplayAnim.SetTrigger("Change");
        StartCoroutine(Type(index));
    }

    public void EndSentence()
    {
        SceneManager.LoadScene(nextScene);
    }

}
