using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;

//AUIRoot

public class AUI : MonoBehaviour
{
    [SerializeField] protected string mValue;
    [SerializeField] string rootPath;
    protected string EventPath;
    [SerializeField] EventPath<string> OnSliderChange;
    protected GameObject root;
    protected System.Action EvAct;

    protected virtual void Awake() 
    {
        root = GameObject.Find(rootPath);
        EvAct = () => {  root.GetComponent<AUIRoot>().Invoke(EventPath, 0); };
        
        ExamWrite<GameUIMgr> w = new ExamWrite<GameUIMgr>();
        w.Perfect(root.GetComponent<GameUIMgr>(), mValue, this);
    }
    public virtual void Start() 
    {
    }

    public virtual void OnChanaged()
    {
        
    }
}

public class ExamWrite<T>
{
    public void Perfect(T exm, string subject, object value)
    {
        if(subject == "") return;
        Type tp = typeof(T);
        FieldInfo fld = tp.GetField(    subject, 
                                        BindingFlags.Instance |
                                        BindingFlags.Static |
                                        BindingFlags.Public |
                                        BindingFlags.NonPublic);
        object point = fld.GetValue(exm);
        fld.SetValue(exm, value);
    }
}