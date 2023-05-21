using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ClothChanger : MonoBehaviour
{
    //[SerializeField] private Material[] _shirtMaterials;
    //[SerializeField] private Material[] _pantsMaterials;
    //[SerializeField] private SkinnedMeshRenderer _shirt;
    //[SerializeField] private SkinnedMeshRenderer _pants;

    //private int _pantsIndex, _shirtIndex;

    //private void ChangeCloth(Material[] mat, ref int index, SkinnedMeshRenderer mesh)
    //{
    //    if (index >= mat.Length)
    //        index = 0;
    //    else if (index < 0)
    //        index = mat.Length - 1;
    //    mesh.material = mat[index];
    //}

    //public void ChangeShirt(int delta)
    //{
    //    _shirtIndex += delta;
    //    ChangeCloth(_shirtMaterials, ref _shirtIndex, _shirt);
    //}

    //public void ChangePants(int delta)
    //{
    //    _pantsIndex += delta;
    //    ChangeCloth(_pantsMaterials, ref _pantsIndex, _pants);
    //}

    [SerializeField] private List<PlayableDirector> _shirt;
    [SerializeField] private List<PlayableDirector> _pants;
}
