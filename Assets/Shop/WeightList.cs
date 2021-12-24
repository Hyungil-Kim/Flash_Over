using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightList<T>
{
    Dictionary<T, double> gradeDic = new Dictionary<T, double>();
    double totalWeight = 0d;
    //
    public void WeightInit()
    {
        totalWeight = 0d;
        foreach (var gradeWeight in gradeDic)
        {
            totalWeight += gradeWeight.Value;
        }
    }

    public T GetRandomGrade()
    {
        var catchValue = totalWeight * Random.value;
        double currentWeight = 0d;
        foreach (var gradeWeight in gradeDic)
        {
            currentWeight += gradeWeight.Value;
            if(catchValue <= currentWeight)
            {
                return gradeWeight.Key;
            }
        }
        return default;
    }
    public void SetGradeWeight(T grade, double weight)
    {
        if(gradeDic.ContainsKey(grade))
        {
            gradeDic[grade] = weight;
        }
        WeightInit();
    }
    public void AddGrade(T grade, double weight)
    {
        if (weight > 0)
        {
            gradeDic.Add(grade, weight);
            WeightInit();
        }
    }
    public void RemoveGrade(T grade)
    {
        if (gradeDic.ContainsKey(grade))
        {
            gradeDic.Remove(grade);
        }
        WeightInit();
    }
}
