using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

public class AuthorityBase : MonoBehaviour
{
    protected virtual void Initialize()
    {
        EntityManager.Instance.RegisterEntity(this);
    }

    protected virtual void OnDestroy()
    {
        EntityManager.Instance.UnregisterEntity(this);
    }
}

public class EntityData
{
    public Type AuthorityType;
    public HashSet<Type> ComponentTypes = new HashSet<Type>(); // Переименовано с SystemTypes
}

public class EntityManager : MonoBehaviour
{
    private static EntityManager _instance;
    public static EntityManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<EntityManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("EntityManager");
                    _instance = go.AddComponent<EntityManager>();
                }
            }
            return _instance;
        }
    }


    private Dictionary<AuthorityBase, EntityData> entities = new Dictionary<AuthorityBase, EntityData>();

    public void RegisterEntity(AuthorityBase authority)
    {
        var data = new EntityData
        {
            AuthorityType = authority.GetType(),
            ComponentTypes = new HashSet<Type>(
                authority.GetComponents<Component>()
                    .Where(c => c != authority) // Убрали исключение Transform
                    .Select(c => c.GetType())
            )
        };

        entities[authority] = data;
    }

    public void UnregisterEntity(AuthorityBase authority)
    {
        entities.Remove(authority);
    }
    
    public T Get<T>(bool required = false) where T : AuthorityBase
    {
        var result = entities.Keys.OfType<T>().FirstOrDefault();
    
        if (required && result == null)
        {
            throw new Exception($"Entity of type {typeof(T).Name} not found!");
        }
    
        return result;
    }

    public List<T> GetAll<T>() where T : AuthorityBase
    {
        return entities.Keys.OfType<T>().ToList();
    }

    public T GetSingleton<T>() where T : AuthorityBase
    {
        var results = GetAll<T>();
    
        if (results.Count > 1)
        {
            Debug.LogWarning($"Multiple entities of type {typeof(T).Name} found! Returning first.");
        }
    
        return results.FirstOrDefault();
    }

    public QueryBuilder Find()
    {
        return new QueryBuilder(this);
    }

    public class QueryBuilder
    {
        private EntityManager manager;
        private List<Type> requiredSystems = new List<Type>();
        private List<Type> excludedSystems = new List<Type>();
        private Type requiredAuthority;
        private Type excludedAuthority;

        internal QueryBuilder(EntityManager manager)
        {
            this.manager = manager;
        }
        
        public QueryBuilder WithComponent<T>() where T : Component
        {
            requiredSystems.Add(typeof(T));
            return this;
        }

        public QueryBuilder WithoutComponent<T>() where T : Component
        {
            excludedSystems.Add(typeof(T));
            return this;
        }

        public QueryBuilder WithAllComponents<T>() where T : Component => WithComponent<T>();
        public QueryBuilder WithNoneComponents<T>() where T : Component => WithoutComponent<T>();

        public QueryBuilder WithAuthority<T>() where T : AuthorityBase
        {
            requiredAuthority = typeof(T);
            return this;
        }

        public QueryBuilder WithoutAuthority<T>() where T : AuthorityBase
        {
            excludedAuthority = typeof(T);
            return this;
        }

        public List<AuthorityBase> Execute()
        {
            return manager.entities
                .Where(kvp =>
                {
                    var data = kvp.Value;
                    
                    bool hasAllSystems = requiredSystems.All(t => data.ComponentTypes.Contains(t));
                    bool hasNoneSystems = !excludedSystems.Any(t => data.ComponentTypes.Contains(t));
                    bool hasAuthority = requiredAuthority == null || data.AuthorityType == requiredAuthority;
                    bool excludeAuthority = excludedAuthority == null || data.AuthorityType != excludedAuthority;

                    return hasAllSystems && hasNoneSystems && hasAuthority && excludeAuthority;
                })
                .Select(kvp => kvp.Key)
                .ToList();
        }
    }
    
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}