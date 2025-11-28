using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/ExpressionSet")]
public class CharacterExpressionSet : ScriptableObject
{
    public string characterName;
    public List<ExpressionData> expressions;
}

[System.Serializable]
public class ExpressionData
{
    public string expressionName;
    public Sprite sprite;
}

