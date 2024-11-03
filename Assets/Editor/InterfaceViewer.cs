using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class InterfaceViewer : EditorWindow
{
    private string interfaceName = "IMyInterface";
    private string searchTerm = "";
    private bool showFields = true;
    private bool showMethods = true;

    [MenuItem("Window/Interface Viewer")]
    public static void ShowWindow()
    {
        GetWindow<InterfaceViewer>("Interface Viewer");
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("当前接口查找有错误,目前还不能用");
        
        EditorGUILayout.LabelField("输入接口名:");
        interfaceName = EditorGUILayout.TextField(interfaceName);

        EditorGUILayout.LabelField("输入要查询的字段或方法名:");
        searchTerm = EditorGUILayout.TextField(searchTerm);

        showFields = EditorGUILayout.Toggle("显示字段", showFields);
        showMethods = EditorGUILayout.Toggle("显示方法", showMethods);

        if (GUILayout.Button("查找实现"))
        {
            FindImplementations();
        }
    }

    private void FindImplementations()
    {
        if (string.IsNullOrWhiteSpace(interfaceName))
        {
            EditorUtility.DisplayDialog("错误", "接口名不能为空", "OK");
            return;
        }

        // 尝试获取接口类型
        var interfaceType = Type.GetType(interfaceName);

        // 调试输出所有加载的类型
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                //Debug.Log(type.FullName); // 输出所有类型名称
            }
        }

        if (string.IsNullOrWhiteSpace(interfaceName))
        {
            EditorUtility.DisplayDialog("错误", "接口名不能为空", "OK");
            return;
        }

        interfaceType = Type.GetType(interfaceName);
        if (interfaceType == null)
        {
            EditorUtility.DisplayDialog("错误", "无法找到指定的接口，请确保输入正确的名称（包括命名空间）", "OK");
            return;
        }

        List<Type> implementations = new List<Type>();
        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (interfaceType.IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
                {
                    implementations.Add(type);
                }
            }
        }

        if (implementations.Count == 0)
        {
            EditorUtility.DisplayDialog("结果", "未找到实现该接口的类", "OK");
            return;
        }

        string results = "实现了 " + interfaceName + " 的类:\n";
        foreach (var type in implementations)
        {
            results += "\n" + type.FullName;

            if (showFields)
            {
                // 查找字段
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (string.IsNullOrEmpty(searchTerm) || field.Name.Contains(searchTerm))
                    {
                        results += "\n - 字段: " + field.Name;
                    }
                }
            }

            if (showMethods)
            {
                // 查找方法
                MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
                foreach (var method in methods)
                {
                    if (string.IsNullOrEmpty(searchTerm) || method.Name.Contains(searchTerm))
                    {
                        results += "\n - 方法: " + method.Name;
                    }
                }
            }
        }

        EditorUtility.DisplayDialog("查找结果", results, "OK");
    }
}