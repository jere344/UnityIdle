using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace LouLouStarterContent.Editor
{

    [CustomPropertyDrawer(typeof(BaseDictionary),true)]
    public class BaseDictionaryDrawer : BasePropertyDrawer
    {
        private ReorderableList _reorderableDictionary;
        private BaseDictionary _dictionaryTarget;
        private SerializedProperty _p_dictionaryEntries;
        private const int _warningLine = 2;
        private bool _isFoldout,_isWarning;
        private GUIStyle _style;

        private float DictionaryHeight => (_isFoldout ? _reorderableDictionary.GetHeight() + 10 : 0);

        internal override void AtStartOfGUI(SerializedProperty property)
        {
            _dictionaryTarget = target as BaseDictionary;
            _dictionaryTarget.InititializeIfNull();
            _p_dictionaryEntries = property.FindPropertyRelative("_dictionaryEntries");
            _style = new GUIStyle(EditorStyles.label);
            _style.alignment = TextAnchor.MiddleCenter;
            numberOfLine = _isWarning ? _warningLine+1: 1 ;

            InitializeReordarableList(property);

        }


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + DictionaryHeight; ;
        }

        private void InitializeReordarableList(SerializedProperty property)
        {
            if (_reorderableDictionary == null)
            {
                _reorderableDictionary = new ReorderableList(property.serializedObject, _p_dictionaryEntries, true, false, true, true);
                _reorderableDictionary.drawElementCallback = DrawElement;
            }
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var currentSerializedKey = _reorderableDictionary.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("key");
            var currentSerializedValue = _reorderableDictionary.serializedProperty.GetArrayElementAtIndex(index).FindPropertyRelative("value");

            var keyRect = new Rect(rect.x, rect.y, rect.width * 7 / 10, rect.height);
            var valueLabelRect = new Rect(rect.x + rect.width * 7 / 10, rect.y, rect.width * 1 / 10, rect.height);
            var valueRect = new Rect(rect.x + rect.width * 8 / 10, rect.y, rect.width * 2 / 10, rect.height);

            //if the element has subsection (List/pure class etc...) it will not be displayed. Maybe you can check if the property has subproperty and add an Edit button that would open a window to edit the said property

            EditorGUI.PropertyField(keyRect, currentSerializedKey, GUIContent.none);
            EditorGUI.LabelField(valueLabelRect, "Value", _style);
            CustomEditorGUILayout.PropertyDrawerWithEditButton(valueRect, currentSerializedValue,new GUIContent( "Value n°" + index));
            //EditorGUI.PropertyField(valueRect, currentSerializedValue, GUIContent.none);
        }
        private void DrawList(SerializedProperty property, Rect rectFoldout)
        {
            var _rect = new Rect(rectFoldout.x, rectFoldout.y + rectFoldout.height + 5, usableSpace, _reorderableDictionary.GetHeight());
            _reorderableDictionary.DoList(_rect);
            property.serializedObject.ApplyModifiedProperties();
        }


        internal override void OnGUIEffect(Rect position, SerializedProperty property)
        {
            //set rects
            var rectLabel = MakeRectForDrawer(0, 1, 1, 0);
        
            //Get keys and values properties
            
            //Draw the foldout
            var tempFoldout = EditorGUI.Foldout(rectLabel,_isFoldout, property.name,true);
           
            if(tempFoldout)
            {
                DrawList(property, rectLabel);
            }
            //Warning stuffs
            _isWarning = GetTargetAs<BaseDictionary>().HasTheSameKeyTwice;

            if(_isWarning)
            {
                var _rectWarning = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight+DictionaryHeight, usableSpace, EditorGUIUtility.singleLineHeight * _warningLine);
                EditorGUI.HelpBox(_rectWarning, "You have more than one key with the same value, you might have some trouble for calling this key.", MessageType.Warning);
            }


            //End of GUI stuffs

            _isFoldout = tempFoldout;
        }
       
    }

}