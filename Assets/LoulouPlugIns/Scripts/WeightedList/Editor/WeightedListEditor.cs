using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace LouLouStarterContent.Editor
{

    [CustomPropertyDrawer(typeof(SerializedWeightedListParent), true)]
    public class WeightedListEditor : BasePropertyDrawer
    {

        private SerializedWeightedListParent _targetWeightedList;
        private ReorderableList _reorderableWeightedList;
        private SerializedProperty _p_weightedElementsList;
        [SerializeField]
        private bool _foldout;
        private int _arraySize;
        private GUIStyle _style;
        private void InitializeReordarableList(SerializedProperty property)
        {
            if (_reorderableWeightedList == null)
            {
                _reorderableWeightedList = new ReorderableList(property.serializedObject, _p_weightedElementsList, true, false, true, true);
                _reorderableWeightedList.drawElementCallback = DrawElement;
            }
        }
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + (_foldout ? _reorderableWeightedList.GetHeight() : 0);
        }
        internal override void AtStartOfGUI(SerializedProperty property)
        {
            numberOfLine = 1;

            _targetWeightedList = target as SerializedWeightedListParent;
            _targetWeightedList.InitializeIfNull();

            _p_weightedElementsList = property.FindPropertyRelative("_weightedElementsList");
            _arraySize = _p_weightedElementsList.arraySize;

            _style = new GUIStyle(EditorStyles.label);
            _style.alignment = TextAnchor.MiddleCenter;

            InitializeReordarableList(property);
        }

        internal override void OnGUIEffect(Rect position, SerializedProperty property)
        {
            _p_weightedElementsList = property.FindPropertyRelative("_weightedElementsList");

            var _rectFoldout = MakeRectForDrawer(0, .8f, 1, .0f);
            var _rectArraySize = MakeRectForDrawer(0, .2f, 1, .0f);

            _foldout = EditorGUI.Foldout(_rectFoldout, _foldout, property.displayName, true);
            _arraySize = EditorGUI.IntField(_rectArraySize, _arraySize);
            _p_weightedElementsList.arraySize = _arraySize;

            if (_foldout)
            {
                DrawList(property, _rectFoldout);
            }
        }

        private void DrawList(SerializedProperty property, Rect rectFoldout)
        {
            var _rect = new Rect(rectFoldout.x, rectFoldout.y + rectFoldout.height + 5, usableSpace, _reorderableWeightedList.GetHeight());
            _reorderableWeightedList.DoList(_rect);
            property.serializedObject.ApplyModifiedProperties();
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var _currentSerializedElement = _reorderableWeightedList.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("Element");
            var _currentSerializedWeight = _reorderableWeightedList.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("Weight");

            var _ElementRect = new Rect(rect.x, rect.y, rect.width * 7 / 10, rect.height);
            var _WeightLabelRect = new Rect(rect.x + rect.width * 7 / 10, rect.y, rect.width * 1 / 10, rect.height);
            var _WeightRect = new Rect(rect.x + rect.width * 8 / 10, rect.y, rect.width * 2 / 10, rect.height);

            CustomEditorGUILayout.PropertyDrawerWithEditButton(_ElementRect, _currentSerializedElement, new GUIContent("Element n°" + index));
            EditorGUI.LabelField(_WeightLabelRect, "Weight", _style);
            EditorGUI.PropertyField(_WeightRect, _currentSerializedWeight, GUIContent.none);

        }
    }

}