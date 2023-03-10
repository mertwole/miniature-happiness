using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Room))]
[RequireComponent(typeof(NeedCollectionModifier))]
public class NeedProvider : MonoBehaviour
{
    public enum FilterType
    {
        None,
        BlackList,
        WhiteList,
        FirstToTake,
    }

    [Serializable]
    public class Filter
    {
        public Filter(List<Employee> employees, FilterType filter_type)
        {
            Employees = employees;
            FilterType = filter_type;
        }

        public List<Employee> Employees = new List<Employee>();
        public FilterType FilterType;

        public bool IsEmployeeAllowed(Employee employee)
        {
            switch (FilterType)
            {
                case FilterType.None:
                    return true;
                case FilterType.WhiteList:
                    return Employees.Contains(employee);
                case FilterType.BlackList:
                    return !Employees.Contains(employee);
                case FilterType.FirstToTake:
                    if (Employees.Contains(employee))
                        return true;
                    else if (Employees.Count == 0)
                    {
                        Employees.Add(employee);
                        return true;
                    }
                    return false;
                default:
                    Debug.LogError("Unknown NeedProviderFilterType!");
                    return true;
            }
        }
    }

    [SerializeField] NeedCollectionModifier needCollectionModifier;

    [SerializeField] List<NeedSlot> availableSlots;
    public NeedType NeedType;

    public NeedSlot TryBook(Employee employee)
    {
        foreach (var slot in availableSlots)
            if (slot.TryBook(employee))
                return slot;

        return null;
    }

    public void ProvideNeedParameters(List<NeedParameters> parameters)
    {
        var need_parameters = needCollectionModifier.Apply(parameters);
        foreach (var slot in availableSlots)
            slot.ProvideNeedParameters(need_parameters);
    }
}
