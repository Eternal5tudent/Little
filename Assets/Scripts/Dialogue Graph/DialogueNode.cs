using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace Dialogue_Udemy
{
    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayerSpeaking = false;
        [SerializeField] string text;
        [SerializeField] List<string> children = new List<string>();
        [HideInInspector] [SerializeField] Rect rect = new Rect(0, 0, 200, 125);
        [HideInInspector] public Vector2 scrollPos;
        public int ChildCount { get { return children.Count; } }
        public bool IsEndingNode { get { return children.Count == 0; } }

#if UNITY_EDITOR
        public Rect GetRect()
        {
            return rect;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialogue Node");
            rect.position = newPosition;
        }

        public void SetText(string newText)
        {
            if (newText != text)
            {
                Undo.RecordObject(this, "Update dialogue text");
                text = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public string GetText()
        {
            return text;
        }

        public string GetFirstChild()
        {
            return children[0];
        }

        public List<string> GetChildren()
        {
            return children;
        }

        public void AddChild(string childID)
        {
            Undo.RecordObject(this, "Add Dialogue Link");
            children.Add(childID);
            EditorUtility.SetDirty(this);

        }

        public void RemoveChild(string childID)
        {
            Undo.RecordObject(this, "Remove Dialogue Link");
            children.Remove(childID);
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(this);

        }

        public bool IsPlayerSpeaking()
        {
            return isPlayerSpeaking;
        }

        internal void SetPlayerSpeaking(bool v)
        {
            Undo.RecordObject(this, "Change Dialogue Speaker");
            isPlayerSpeaking = v;
        }
    }
#endif
}
