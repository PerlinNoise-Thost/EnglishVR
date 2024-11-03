using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using UnityEngine;

public abstract class Data_Base : MonoBehaviour,IInitialization
{
    public abstract IEnumerator _data_Set();
    public abstract string GetData(string key);

    public BaiDu_LLM baiDuLlm = null;
    public BaiDu_STT baiDuStt = null;
    public BaiDu_TTS baiDuTts = null;

    //实现初始化接口排序属性
    public virtual string DataSetSequence =>GetType().Name;

    public IEnumerator Data_Set()
    {
        baiDuLlm = GameObject.FindFirstObjectByType<BaiDu_LLM>();
        baiDuStt = GameObject.FindFirstObjectByType<BaiDu_STT>();
        baiDuTts = GameObject.FindFirstObjectByType<BaiDu_TTS>();

        yield return StartCoroutine(_data_Set());
        
        yield break;
    }

    
    /// <summary>
    /// 读取表格
    /// </summary>
    /// <param name="path">路径</param>
    /// <param name="Length">有效列数</param>
    /// <param name="configDict">字典</param>
    /// <typeparam name="T">类</typeparam>
    public IEnumerator LoadCFG<T>(string path,int Length,Dictionary<string, T> configDict) where T : new()
    {
        FileStream fs = new FileStream(path, FileMode.Open);
        using (ExcelPackage package = new ExcelPackage(fs))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            int h = sheet.Dimension.End.Row;    //获取行数
        
            for (int i = 2; i <= h; i++)
            {
                T tempCfg = new T();
                var properties = typeof(T).GetFields();
                
                //Debug.Log(properties.Length);

                // 假设类的字段按顺序对应Excel的列
                for (int col = 1; col <= Length; col++)
                {
                    var prop = properties[col-1];
                
                    prop.SetValue(tempCfg, sheet.GetValue(i, col)?.ToString());
                }
            
                //第一列作为Key
                string id = sheet.GetValue(i, 1)?.ToString();
                
                configDict.Add(id, tempCfg);
            }
        }
        
        yield break;
    }

}
