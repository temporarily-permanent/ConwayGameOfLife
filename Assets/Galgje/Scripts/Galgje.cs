using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Galgje : MonoBehaviour
{
    
    
    private string _answer;
    private string _progress;
    private List<char> _guessedLetters;
    private List<char> _correctLetters;
    private List<char> _incorrectLetters;
    
    private void RecieveInput(string input)
    {
        foreach (char c in input)
        {
            TryLetter(c);
        }
    }

    private void TryLetter(char c)
    {
        if (!_guessedLetters.Contains(c))
        {
            _guessedLetters.Add(c);
            TryNewLetter(c);
            
        }
        else
        {
            //todo show error in ui
        }
    }

    // TODO fix naming
    private void TryNewLetter(char c)
    {
        
    }

    public void SetNewAnswer(string newAnswer)
    {
        _answer = newAnswer.ToLower();
        _progress = _answer; 
        for (int i = 0; i < newAnswer.Length; i++)
        {
            Char.IsLetter(_progress[i]);
        }                                     
    }
        
    // Start is called before the first frame update
    void Start()
    {
        SetNewAnswer("The cat sprinted across the lawn like it was late for a very important meeting.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
