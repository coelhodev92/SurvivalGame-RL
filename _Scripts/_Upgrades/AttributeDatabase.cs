using UnityEngine;
using System.Collections.Generic;

public class AttributeDatabase : MonoBehaviour
{
    public List<AttributeData> allAttributes = new List<AttributeData>();

    public List<AttributeData> GetRandomAttributes(int count)
    {
        List<AttributeData> pool = new List<AttributeData>(allAttributes);
        List<AttributeData> result = new List<AttributeData>();

        count = Mathf.Min(count, pool.Count);

        for (int i = 0; i < count; i++)
        {
            int randomIndex = Random.Range(0, pool.Count);
            result.Add(pool[randomIndex]);
            pool.RemoveAt(randomIndex);
        }

        return result;
    }
}