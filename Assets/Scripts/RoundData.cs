using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData {
    public string name; //Round Name
    public int pointsAddedForCorrectAnswer;
    public QuestionData[] questions;
}
