using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AText : AUI
{
    private AUIProperty<string> mText = new AUIProperty<string>("");

    protected override void Awake(){
        base.Awake();
    }
    public override void Start(){
        base.Start();
    }
    public string MText { 
        get => mText.Value; 
        set
        {
            mText.Value = value;
            OnChanaged();
        }
    }

    public override void OnChanaged()
    {
        GetComponent<UnityEngine.UI.Text>().text = MText;
    }
}


