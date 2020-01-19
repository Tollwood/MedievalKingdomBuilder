
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializationManager
{

    public static bool Save(string savePath, object data)
    {
        BinaryFormatter binaryFormatter = GetBinaryFormatter();

        FileStream file = File.Create(savePath);
        binaryFormatter.Serialize(file, data);

        file.Close();
        return true;
    }

    public static object Load(string savePath)
    {
        if (!File.Exists(savePath))
        {
            return null;
        }
        FileStream file = File.Open(savePath,FileMode.Open);

        try
        {
            return GetBinaryFormatter().Deserialize(file);
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}", savePath);
            return null;
        }
        finally
        {
            file.Close();
        }
    }

    private static BinaryFormatter GetBinaryFormatter()
    {
        BinaryFormatter bf = new BinaryFormatter();
        SurrogateSelector selector = new SurrogateSelector();
        StreamingContext streamingContext = new StreamingContext(StreamingContextStates.All);
        selector.AddSurrogate(typeof(Vector3), streamingContext, new Vector3SerializationSurrogate());
        selector.AddSurrogate(typeof(Quaternion), streamingContext, new QuaternionSerializationSurrogate());

        bf.SurrogateSelector = selector;
        return bf;
    }
}
