using System.Collections.Generic;
using CsvFiles;
using System.IO;
using UnityEngine;

public class CsvControll
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static IEnumerable<T> LoadCSVDataFromBytes<T>(byte[] bytes) where T : new()
    {
        if (bytes == null || bytes.Length == 0)
            return null;
        Stream stream = new MemoryStream(bytes);
        StreamReader streamReader = new StreamReader(stream);
        return CsvFile.Read<T>(streamReader);
    }

    public static IEnumerable<T> LoadCSVDataFromFile<T>(string resourcePath) where T : new()
    {
        TextAsset txtData = (TextAsset)Resources.Load(resourcePath);
        if (txtData == null)
            return null;

        Stream stream = new MemoryStream(txtData.bytes);
        StreamReader streamReader = new StreamReader(stream);
        return CsvFile.Read<T>(streamReader);
    }
}
