using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;

public abstract class Data_Base<T> : MonoBehaviour where T : Data_Base<T>
{
    #region 单例模板(暂时用不到)

    /*private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject($"[{typeof(T).Name} Singleton]");
                    instance = obj.AddComponent<T>();
                }
            }
            return instance;
        }
    }*/

    #endregion
     
    /// <summary>
    /// 文件路径  
    /// </summary>
    public abstract string path { get; }

    public IEnumerator LoadCFG<U>(int Length, Dictionary<string, U> configDict) where U : new()
    {
        using (FileStream fs = new FileStream(path, FileMode.Open))
        using (ExcelPackage package = new ExcelPackage(fs))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            int h = sheet.Dimension.End.Row; // 获取行数

            for (int i = 2; i <= h; i++)
            {
                U tempCfg = new U();
                var properties = typeof(U).GetFields();

                for (int col = 1; col <= Length; col++)
                {
                    if (col - 1 < properties.Length)
                    {
                        var prop = properties[col - 1];
                        prop.SetValue(tempCfg, sheet.GetValue(i, col)?.ToString());
                    }
                    else
                    {
                        Debug.LogWarning($"Excel 列 {col} 超出 {typeof(U).Name} 字段数量");
                    }
                }

                string id = sheet.GetValue(i, 1)?.ToString();
                if (!string.IsNullOrEmpty(id) && !configDict.ContainsKey(id))
                {
                    configDict.Add(id, tempCfg);
                }
                else
                {
                    Debug.LogWarning($"ID {id} 为空或已存在");
                }
            }
        }
        yield break;
    }
}
