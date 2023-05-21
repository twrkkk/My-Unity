using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    //[SerializeField] private List<GameObject> _prefabs;
    public List<Platform> _level = new List<Platform>(5);
    [SerializeField] private Transform _parent;
    [SerializeField] private Transform _player;
    [SerializeField] private float _deleteDistance;
    [SerializeField] private float _distBtwnLine;
    private int _skyRotationHash;
    private float _distBtwmPieces;
    private float _lastCoinLineZ = -80;
    private float _lastCoinZ = -80;
    private float _lastSkyRot;
    private Material _sky;

    public Platform _tail, _head;


    private void Start()
    {
        _sky = RenderSettings.skybox;
        _skyRotationHash = Shader.PropertyToID("_Rotation");
        _distBtwmPieces = ObjectPool.Instance.Prefabs[0].transform.localScale.z * 10;
        Debug.Log(_distBtwmPieces);

        for (int i = 0; i < 2; i++)
        {
            AddPiece(true);
        }

        SpawnLineCoin();
        
    }

    private void Update()
    {
        RotateSky();
        if ((_player.position.z - _head.transform.position.z) > _deleteDistance)
        {
            DeletePiece();
            AddPiece(false);
        }
        if(Mathf.Abs(_player.position.z - _lastCoinLineZ) < 45)
        {
            SpawnLineCoin();
        }
    }

    private void RotateSky()
    {
        _sky.SetFloat(_skyRotationHash, _lastSkyRot);
        _lastSkyRot += Time.deltaTime / 10f * PlayerController.Instance.GetSpeed();
    }

    private void DeletePiece()
    {
        _head.ToPool();
        _head = _parent.GetChild(0).GetComponent<Platform>();
    }

    private void AddPiece(bool isInit)
    {
        Platform newPlatform =  ObjectPool.Instance.Pool.GetFreeElement();
        newPlatform.gameObject.SetActive(true);
        if (_parent.childCount == 1)
        {
            newPlatform.transform.position = new Vector3(17.6f, 0, _distBtwmPieces * 3);
        }
        else
        {
            newPlatform.transform.position = _parent.GetChild(_parent.childCount - 2).position + new Vector3(0, 0, _distBtwmPieces * 12);
        }

        _tail = newPlatform;
        if(!_head)
            _head = _parent.GetChild(0).GetComponent<Platform>();

    }

    private Coin SpawnCoin(int line)
    {
        Coin coin = ObjectPool.Instance.Coins.GetFreeElement();
        coin.transform.position = new Vector3((line - 1) * _distBtwnLine, 0.5f, _lastCoinZ + 4);
        coin.gameObject.SetActive(true);
        _lastCoinZ = coin.transform.position.z;
        return coin;

    }

    private void SpawnLineCoin()
    {
        Coin coin = null;
        int line = Random.Range(0, 2);
        for (int i = 0; i < 15; i++)
        {
             coin = SpawnCoin(line);
        }
        _lastCoinLineZ = coin.transform.position.z;
    }
}