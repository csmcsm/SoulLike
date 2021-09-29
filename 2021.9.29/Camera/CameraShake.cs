using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 _currentPosition;        //记录抖动前的位置
    private float _shakeCD = 0.05f;        //抖动的频率
    private int _shakeCount = -1;            //设置抖动次数
    private float _shakeTime;
    public int level = 1;
    public int Count=5;
    float radio = 0f;
    void Start()
    {
        if (cameraTransform == null) cameraTransform = transform;

        _currentPosition = cameraTransform.position;    //记录抖动前的位置         //设置抖动次数
    }
    void Update()
    {
        if (_shakeTime + _shakeCD < Time.time && _shakeCount > 0)
        {
            _shakeCount--;
            float _radio = Random.Range(-1*_shakeCount*radio/ Count,
                _shakeCount * radio / Count);
            float _radio2 = Random.Range(-1 * _shakeCount * radio / Count,
                _shakeCount * radio / Count);
            if (_shakeCount == 1)
            {    //抖动最后一次时设置为都动前记录的位置
                radio = 0;
                this.enabled=false;
            }
            _shakeTime = Time.time;
            cameraTransform.position = _currentPosition + transform.up
                * _radio * level+transform.right
                * _radio2 * level;
        }
    }
    private void OnEnable()
    {
        _currentPosition = cameraTransform.position;
        radio = 1f;
        _shakeCount = Count;
    }
}