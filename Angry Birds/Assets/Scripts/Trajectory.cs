
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Trajectory : MonoBehaviour
{
    [SerializeField] private float _trajectoryWidth;
    [SerializeField] private Transform _dotsParent;
    [SerializeField] private int _countTrajectoryDots;
    private int _currIndex;
    private LineRenderer _lineRenderer;
    private GameObject[] _dots;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _dots = Slingshot.instance.TrailDots;
        _currIndex = 0;
        //_lineRenderer.SetWidth(_trajectoryWidth, _trajectoryWidth/2f);
    }

    public void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        for (int i = _dots.Length - 1; i > _dots.Length - _countTrajectoryDots - 1; i--)
        {
            //while(!_dots[_currIndex].activeSelf)
            //{
            //    _currIndex++;
            //    if (_currIndex > _dots.Length - 1) _currIndex = 0;
            //}
            float time = (_dots.Length - 1 - i) * 0.1f;
            _dots[i].transform.position = origin + speed * time + Physics.gravity * time * time / 2f;
            _dots[i].SetActive(true);
        }
    }

    public void Show()
    {
        _lineRenderer.enabled = true;
    }

    public void Hide()
    {
        _lineRenderer.enabled = false;
    }
}
