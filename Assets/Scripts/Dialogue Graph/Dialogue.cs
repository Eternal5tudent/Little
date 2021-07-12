using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Dialogue_Udemy
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField] Vector2 newNodeOffset = new Vector2(250, 0);
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

        private void Awake()
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parentNode)
        {
            foreach (string childID in parentNode.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

        public DialogueNode GetNextNode(DialogueNode parentNode)
        {
            if (nodeLookup.ContainsKey(parentNode.GetFirstChild()))
            {
                return nodeLookup[parentNode.GetFirstChild()];
            }
            else
            {
                return null;
            }
        }

        public DialogueNode GetFirstNode()
        {
            return nodes[0];
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            nodeLookup.Clear();
            if (nodes.Count == 0)
            {
                CreateNode(null);
            }
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = MakeNode(parent);
            Undo.RegisterCreatedObjectUndo(newNode, "Created Dialogue Node");
            Undo.RecordObject(this, "Added Dialogue Node");
            AddNode(newNode);
        }

        private void AddNode(DialogueNode newNode)
        {
            nodes.Add(newNode);
            OnValidate();
        }

        private DialogueNode MakeNode(DialogueNode parent)
        {
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            if (parent != null)
            {
                parent.AddChild(newNode.name);
                newNode.SetPosition(parent.GetRect().position + newNodeOffset);
                newNode.SetPlayerSpeaking(!parent.IsPlayerSpeaking());
            }
            return newNode;
        }

        public void DeleteNode(DialogueNode nodeToDelete)
        {
            nodes.Remove(nodeToDelete);
            OnValidate();
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        public void OnBeforeSerialize()
        {

            if (nodes.Count == 0)
            {
                DialogueNode newNode = MakeNode(null);
                AddNode(newNode);
            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach (DialogueNode node in GetAllNodes())
                {
                    if (AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
        }
        public void OnAfterDeserialize()
        {

        }
#else
        public void OnBeforeSerialize()
        {

        }
        public void OnAfterDeserialize()
        {

        }
#endif

    }
}