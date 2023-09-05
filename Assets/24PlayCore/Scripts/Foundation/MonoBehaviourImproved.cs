using System;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourImproved : MonoBehaviour
{
    private Dictionary<Type, Component> cachedComponents;

    public new T GetComponent<T>() where T : Component
    {
        if (cachedComponents == null)
        {
            cachedComponents = new Dictionary<Type, Component>();
        }

        if (cachedComponents.ContainsKey(typeof(T)))
            return cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if (component != null)
        {
            cachedComponents.Add(typeof(T), component);
        }

        return component;
    }

    public new bool TryGetComponent<T>(out T component) where T : Component
    {
        if (cachedComponents == null)
        {
            cachedComponents = new Dictionary<Type, Component>();
        }

        if (cachedComponents.ContainsKey(typeof(T)))
        {
            component = cachedComponents[typeof(T)] as T;
            return true;
        }

        if (base.TryGetComponent(out component))
        {
            cachedComponents.Add(typeof(T), component);
            return true;
        }

        component = null;
        return false;
    }
}
