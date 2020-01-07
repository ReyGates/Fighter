using UnityEngine;
using System.Collections;

public class BaseShip<T, U> : Singleton<U> where T : BaseShipData where U : MonoBehaviour
{
    public T Data;

    private Vector3 _currentEuler;

    private void Start()
    {
        _currentEuler = transform.rotation.eulerAngles;
    }

    protected void Move(Vector3 newPos)
    {
        _currentEuler.x = 0;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(_currentEuler), Time.deltaTime * 5);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * Data.Speed);
    }
}
