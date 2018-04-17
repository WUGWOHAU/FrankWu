using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue {

    //名前
    public string name;

    //スクリプト
    [TextArea( 3, 10 ) ]
    public string[] sentences;

    [ TextArea( 3, 10 ) ]
    public string[] Healsentences;

    [ TextArea( 3, 10 ) ]
    public string[] Attacksentences;

    [TextArea(3, 10)]
    public string[] Endsentences;

}