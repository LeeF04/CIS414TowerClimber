//Michael Anglemier

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_JumpTypeManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI jumpTypeText;

    void Start()
    {
        jumpTypeText.text = "Move Type\n" + "None";
    }

    public void UpdateMoveTypeText(string MoveTypeName)
    {
        if (MoveTypeName != null)
        {
            jumpTypeText.text = "Move Type\n" + MoveTypeName;
        }
        else
        {
            jumpTypeText.text = "None";
        }
    }


}
