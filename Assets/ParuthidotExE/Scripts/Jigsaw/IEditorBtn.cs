using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ParuthidotExE
{
    public interface IEditorApply
    {
        void Apply();
    }


    //Target any MonoBehaviour, set second parameter to true so that any class that inherits Monobhaviour is evaluated.
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class IEditorBtn : Editor
    {
        IEditorApply applyBehaviour;
        public void OnEnable()
        {
            //Check if the monobehaviour has the required interface
            if (target is IEditorApply)
            {
                applyBehaviour = (IEditorApply)target;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //only add the button if the IEditorApply interface was found.
            if (applyBehaviour == null)
            {
                return;
            }
            //Add the actual button and make sure MyBehaviour.Apply is called on click
            if (GUILayout.Button("Apply"))
            {
                applyBehaviour.Apply();
            }
        }
    }


}

