using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace CCS {

    public class ItemBuilderWindow : EditorWindow
    {

        CCSItemType itemType = CCSItemType.CCSBaseItem;
        public bool autoNumbering = true;

        public string fileName;
        public int stackSize;
        public Sprite icon;
        Type sentType;
        CCSBaseItem instance;
        SerializedObject serializedObjectInstance;

        string fileLocation = "Resources/Items";
        bool builtNewObject = false;

        int generatedNumber = 0;

        public bool weapon;
        public bool armor;



        [MenuItem("Window/CCSInventory/ItemBuilder")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(ItemBuilderWindow));
        }

        void OnGUI()
        {
            
            itemType = (CCSItemType)EditorGUILayout.EnumPopup("Item Type: ", itemType);
            if (itemType == CCSItemType.CCSEquipment)
            {
                weapon = EditorGUI.Toggle(EditorGUILayout.GetControlRect(), "Is a Weapon?", weapon);
                if (weapon)
                {
                    sentType = Type.GetType("CCSWeapon");
                }
                else
                {
                    sentType = Type.GetType("CCSArmor");
                }
            }
            else
            {
                sentType = Type.GetType(itemType.ToString());
                if(sentType == null)
                {
                    sentType = Type.GetType("CCS/" + itemType.ToString());
                }
            }

            if (sentType == null)
            {
                EditorGUILayout.LabelField("Class name was not found!");
                EditorGUILayout.LabelField("Please Review:");
                EditorGUILayout.LabelField("ENUM:CCSItemType's Spelling");
                EditorGUILayout.LabelField("Derived Class's Name");
                Debug.LogWarning("Class name was not found, please make sure the enum's name and the class name are the same.");
            }
            else
            {

                var mi = typeof(ItemBuilderWindow).GetMethod("DisplayUI");
                var method = mi.MakeGenericMethod(sentType);
                method.Invoke(this, null);

            }

        }


        public void DisplayUI<T>() where T : CCSBaseItem
        {
            if (instance == null)
            {
                instance = CreateInstance<T>();
                serializedObjectInstance = new SerializedObject(instance);
                Debug.Log("Creating New Instance");
            }
            else if (instance.GetType() != typeof(T))
            {
                instance = CreateInstance<T>();
                serializedObjectInstance = new SerializedObject(instance);
                Debug.Log("Creating New Instance");
            }
            else if (builtNewObject)
            {
                builtNewObject = false;
                instance = CreateInstance<T>();
                serializedObjectInstance = new SerializedObject(instance);
                Debug.Log("Creating New Instance");
            }



           
            FieldInfo[] fields = instance.GetType().GetFields();
            fields = ReorderFields(fields);

            foreach (FieldInfo f in fields)
            {

                if (f.IsPublic)
                {
                    if (f.Name == "itemID")
                    {
                        autoNumbering = EditorGUILayout.Toggle("Recommended: Auto Numbering", autoNumbering);
                        if (autoNumbering)
                        {
                            SerializedProperty property = serializedObjectInstance.FindProperty("itemID");
                            generatedNumber = GetItemID();
                            property.intValue = generatedNumber;
                            EditorGUILayout.LabelField("ItemID: " + generatedNumber);

                        }
                        else
                        {
                            SerializedProperty property = serializedObjectInstance.FindProperty("itemID");
                            EditorGUILayout.PropertyField(property, true);
                        }


                    }
                    else
                    {
                        DisplayObjectType<T>(f);
                    }


                   
                }
            }
            EditorGUILayout.Space();
            fileName = EditorGUILayout.TextField("File Name:", fileName);
            fileLocation = EditorGUILayout.TextField("Build Path:", fileLocation);
            if (GUI.Button(EditorGUILayout.GetControlRect(), "Build Item"))
            {
                foreach (FieldInfo field in fields)
                {
                    if (field.Name == "itemID")
                    {
                        field.SetValue(instance, generatedNumber);
                    }
                }
                
                Build(instance);
            }

        }


        void Build<T>(T instance) where T : CCSBaseItem
        {
            //Saving All Changes.
            serializedObjectInstance.ApplyModifiedProperties();

            if (fileName == "")
            {
                Debug.LogWarning("File name in Item Builder Window is not initialized.");
                return;
            }
            if (instance.name == fileName || System.IO.File.Exists(Application.dataPath + "/Assets/" + fileLocation + "/" + fileName + ".asset"))
            {
                Debug.Log("File Name - Already Exists, Please Use Another.");
                return;
            }
            Debug.Log("Building Item: " + fileName + " at: Assets/" + fileLocation + "/" + fileName);
            string assetPath = AssetDatabase.GenerateUniqueAssetPath("Assets/" + fileLocation + "/" + fileName + ".asset");
            string[] subfolders = fileLocation.Split('/');
            string directoryPath = Application.dataPath + "/";
            string currentFolder = "";
            Debug.Log(Application.dataPath);
            foreach (string s in subfolders)
            {

                if (!System.IO.Directory.Exists(directoryPath + s))
                {
                    Debug.Log("Assets" + currentFolder + s);
                    string guid = AssetDatabase.CreateFolder("Assets" + currentFolder, s);
                    AssetDatabase.GUIDToAssetPath(guid);
                }
                currentFolder += "/" + s;
                directoryPath += s + "/";

            }

            AssetDatabase.CreateAsset(instance, assetPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();


            builtNewObject = true;

        }




        public void DisplayObjectType<T>(FieldInfo field) where T : CCSBaseItem
        {

            if (field.FieldType == typeof(Sprite) || field.FieldType == typeof(Texture2D))
            {
                //LargeIcon()
                Rect spriteRect = EditorGUILayout.GetControlRect(true, 32);
                EditorGUI.LabelField(new Rect(34, spriteRect.y, spriteRect.width - 32, 16), "Item Icon:");
                if (field.FieldType == typeof(Sprite))
                {
                    field.SetValue(instance, (Sprite)EditorGUI.ObjectField(new Rect(34, spriteRect.y + 16, spriteRect.width - 34, 16), (Sprite)field.GetValue(instance), typeof(Sprite), false));

                    if ((Sprite)field.GetValue(instance) != null)
                    {

                        PropertyInfo texture = typeof(Sprite).GetProperty("texture");

                        EditorGUI.DrawTextureTransparent(new Rect(spriteRect.x, spriteRect.y, 32, 32), (Texture2D)texture.GetValue((Sprite)field.GetValue(instance), null), ScaleMode.ScaleToFit);
                    }

                }

                else
                {
                    field.SetValue(instance, (Texture2D)EditorGUI.ObjectField(new Rect(34, spriteRect.y + 16, spriteRect.width - 34, 16), (Texture2D)field.GetValue(instance), typeof(Sprite), false));
                    if (field.GetValue(instance) != null)
                    {
                        EditorGUI.DrawTextureTransparent(new Rect(spriteRect.x, spriteRect.y, 32, 32), (Texture2D)field.GetValue(instance), ScaleMode.ScaleToFit);

                    }
                }

            }
            else
            {
                SerializedProperty property = serializedObjectInstance.FindProperty(field.Name);

                EditorGUILayout.PropertyField(property, true);
            }
        }

    

        public string GetProperName(string name)
        {

            string firstLetter = name.Substring(0, 1);
            firstLetter = firstLetter.ToUpper();
            name = firstLetter + name.Remove(0, 1);
            return name.Aggregate(string.Empty, (result, next) =>
            {
                if (char.IsUpper(next) && result.Length > 0)
                {
                    result += ' ';
                }
                return result + next;
            });
           
        }

        int GetItemID()
        {
            int maxID = 0;
            string[] guids = AssetDatabase.FindAssets("t:" + typeof(CCSBaseItem).Name);  //FindAssets uses tags check documentation for more info
            CCSBaseItem[] a = new CCSBaseItem[guids.Length];
            for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                a[i] = AssetDatabase.LoadAssetAtPath<CCSBaseItem>(path);

                if (a[i].itemID >= maxID)
                {
                    maxID = a[i].itemID + 1;
                }
            }

            return maxID;

        }

        public FieldInfo[] ReorderFields(FieldInfo [] fields)
        {
            List<Type> types = new List<Type>();
            List<FieldInfo> fieldList = new List<FieldInfo>();
            foreach(FieldInfo f in fields)
            {
                if(!types.Contains(f.DeclaringType))
                {
                    FieldInfo info = f;
                    types.Add(info.DeclaringType);
                    
                }
            }

            for(int i = types.Count -1;i>=0;i--)
            {
                foreach (FieldInfo f in fields)
                {
                    if(f.DeclaringType == types[i])
                    {
                        FieldInfo info = f;
                        fieldList.Add(info);
                    }
                }
            }
           
            return fieldList.ToArray();
        }
    }
}