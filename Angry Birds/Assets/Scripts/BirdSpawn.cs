using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawn : MonoBehaviour
{
    [SerializeField] private GameObject[] _birds;
    [SerializeField] private Transform[] _birdsPos;
    [SerializeField] private Transform _birdParent;

    public GameObject[] _queue;

    private int _firstBirdIndex;

    public GameObject NextBird() 
    {
        GameObject res = null;
        if (_firstBirdIndex < _queue.Length && _queue[_firstBirdIndex])
        { 
            res = _queue[_firstBirdIndex++];
            res.GetComponent<Rigidbody2D>().isKinematic = false;
        }

        if (res)
        { 
            _birdParent.position += new Vector3(10f, 0, 0);
                    
        }

        return res;
    }

    public void DeactivateBirds()
    {
        foreach (GameObject bird in _queue)
        {
            if(bird)
                bird.GetComponent<Bird>()._isInteractible = false;
        }
    }

    private void Awake()
    {
        _queue = new GameObject[_birds.Length];
        _firstBirdIndex = 0;

        for (int i = 0; i < _birds.Length; i++)
        {
            GameObject newBird = Instantiate(_birds[i], _birdsPos[i].position, Quaternion.identity);
            newBird.transform.parent = _birdParent; 
            newBird.GetComponent<SpringJoint2D>().enabled = false;
            _queue[i] = newBird;
        }
    }

    private void Start()
    {
            PlayerScores.instance.BirdCount = _birds.Length;
    }
}
