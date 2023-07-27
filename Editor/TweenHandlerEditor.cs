using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Exile
{
    //[CustomEditor(typeof(TweenHandler))]
    public class TweenHandlerEditor : Editor
    {
    //    private ReorderableList reorderableList;
    //    private List<TweenContainer> containerList;
    //    private List<TweenContainer> CreateList()
    //    {
    //        SerializedProperty sp = serializedObject.FindProperty("containers");
    //        List<TweenContainer> containers = new List<TweenContainer>();
            
    //        if (sp.isArray)
    //        {
    //            for (int i = 0; i < sp.arraySize; i++)
    //            {
    //                containers.Add(CreateContainer(sp.GetArrayElementAtIndex(0)));
    //            }
    //        }
    //        return containers;
    //    }
    //    private TweenContainer CreateContainer(SerializedProperty sp)
    //    {
    //        TweenContainer container = new TweenContainer();
    //        container.GameObject = sp.FindPropertyRelative("gameObject").objectReferenceValue as GameObject;
    //        container.Order = sp.FindPropertyRelative("order").intValue;
    //        container.TweenData = sp.FindPropertyRelative("tweenData").objectReferenceValue as TweenData;
    //        return container;
    //    }
    //    public override void OnInspectorGUI()
    //    {           
    //        serializedObject.Update();
    //        reorderableList.DoLayoutList();
    //        //this.DrawDefaultInspector();
    //        //myTarget.test = EditorGUILayout.IntField("Experience", myTarget.test);
    //        //EditorGUILayout.LabelField("Level", myTarget.test.ToString());
    //    }
    //    private void OnEnable()
    //    {
    //        containerList = CreateList();
    //        reorderableList = new ReorderableList(containerList, typeof(TweenContainer), true, true, true, true);
    //        reorderableList.elementHeight = 90;
    //        reorderableList.drawHeaderCallback += DrawHeader;
    //        reorderableList.drawElementCallback += DrawElement;
    //        reorderableList.onAddCallback += AddItem;
    //        reorderableList.onRemoveCallback += RemoveItem;
    //    }
    //    private void OnDisable()
    //    {
    //        SerializedProperty sp = serializedObject.FindProperty("containers");
    //        sp.ClearArray();
    //        for (int i = 0; i < containerList.Count; i++)
    //        {
    //            sp.InsertArrayElementAtIndex(i);
    //            SerializedProperty elementSP = sp.GetArrayElementAtIndex(i);
    //            elementSP.FindPropertyRelative("gameObject").objectReferenceValue = containerList[i].GameObject;
    //            elementSP.FindPropertyRelative("order").intValue = containerList[i].Order;
    //            elementSP.FindPropertyRelative("tweenData").objectReferenceValue = containerList[i].TweenData;
    //        }
    //        serializedObject.ApplyModifiedProperties();
    //        EditorUtility.SetDirty(target);
    //    }
    //    private void DrawHeader(Rect rect)
    //    {
    //        GUI.Label(rect, "Tweens");
    //    }

    //    private Rect GetPositionRect(Rect rect, int index, int element, int width)
    //    {
    //        float spacingY = 22;
    //        float spacingX = 80;
    //        float height = 18;
    //        return new Rect(rect.x + spacingX * element, rect.y + spacingY * index, width, height);
    //    }

    //    /// <summary>
    //    /// Draws one element of the list
    //    /// </summary>
    //    /// <param name="rect"></param>
    //    /// <param name="index"></param>
    //    /// <param name="active"></param>
    //    /// <param name="focused"></param>
    //    private void DrawElement(Rect rect, int index, bool active, bool focused)
    //    {
    //        EditorGUI.BeginChangeCheck();
    //        //int numberOfElements = 4;
    //        TweenContainer item = containerList[index];
    //        EditorGUI.LabelField(GetPositionRect(rect, 0, 0, 80), "Order");
    //        item.Order = EditorGUI.IntField(GetPositionRect(rect, index, 1, 60), item.Order);

    //        EditorGUI.LabelField(GetPositionRect(rect, 1, 0, 80), "GameObject");
    //        item.GameObject = (GameObject)EditorGUI.ObjectField(GetPositionRect(rect, 1, 1, 140), item.GameObject, typeof(GameObject), true);

    //        EditorGUI.LabelField(GetPositionRect(rect, 2, 0, 80), "Data");
    //        item.TweenData = (TweenData)EditorGUI.ObjectField(GetPositionRect(rect, 2, 1, 140), item.TweenData, typeof(TweenData), true);
    //        if (item.TweenData is TextColorData colorData)
    //        {
    //            EditorGUI.LabelField(GetPositionRect(rect, 3, 0, 80), "Color");
    //            colorData.color = EditorGUI.ColorField(GetPositionRect(rect, 3, 1, 140), Color.white);
    //            EditorUtility.SetDirty(colorData);
    //        }
            
    //        //item.boolValue = EditorGUI.Toggle(new Rect(rect.x, rect.y, 18, rect.height), item.boolValue);
    //        //item.stringvalue = EditorGUI.TextField(new Rect(rect.x + 18, rect.y, rect.width - 18, rect.height), item.stringvalue);
    //        if (EditorGUI.EndChangeCheck())
    //        {
    //            EditorUtility.SetDirty(target);
    //        }

    //        // If you are using a custom PropertyDrawer, this is probably better
    //        // EditorGUI.PropertyField(rect, serializedObject.FindProperty("list").GetArrayElementAtIndex(index));
    //        // Although it is probably smart to cach the list as a private variable ;)
    //    }

    //    private void AddItem(ReorderableList list)
    //    {
    //        Debug.Log("Add");
    //        containerList.Add(new TweenContainer());
    //        //SerializedProperty sp = serializedObject.FindProperty("containers");
    //        //sp.InsertArrayElementAtIndex(sp.arraySize - 1);
    //        //reorderableList = new ReorderableList(CreateList(), typeof(TweenContainer), true, true, true, true);
    //        //Debug.Log(sp.arraySize);
    //        EditorUtility.SetDirty(target);
    //    }

    //    private void RemoveItem(ReorderableList list)
    //    {
    //        //SerializedProperty sp = serializedObject.FindProperty("containers");
    //        //sp.DeleteArrayElementAtIndex(sp.arraySize - 1);
    //        //serializedObject.ApplyModifiedProperties();
    //        //serializedObject.Update();
    //        containerList.RemoveAt(list.selectedIndices.First());
    //        EditorUtility.SetDirty(target);
    //    }
    }
}
