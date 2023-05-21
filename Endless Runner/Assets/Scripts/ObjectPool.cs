using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private List<int> _poolSize = new List<int>();
    [SerializeField] private List<bool> autoExpand = new List<bool>();
    [SerializeField] private List<Platform> _platformPrefabs = new List<Platform>();
    [SerializeField] private List<Coin> _coinPrefabs = new List<Coin>();    
    [SerializeField] private List<Transform> _parent;

    public static ObjectPool Instance;

    private PoolMono<Platform> _pool;
    private PoolMono<Coin> _coinPool;
    public PoolMono<Platform> Pool { get { return _pool; } }
    public List<Platform> Prefabs { get { return _platformPrefabs; }  }

    public PoolMono<Coin> Coins { get { return _coinPool; } }

    private void Awake()
    {
        Instance = this;
        _pool = new PoolMono<Platform>(_platformPrefabs, _poolSize[0], _parent[0]);
        _pool.autoExpand = autoExpand[0];

        foreach (var item in _pool.pool)
        {
            item.ToPool();
        }

        _coinPool = new PoolMono<Coin>(_coinPrefabs, _poolSize[1], _parent[1]);
        _coinPool.autoExpand = autoExpand[1];
    }
}
