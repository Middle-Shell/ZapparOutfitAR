using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

public class StartDraw : MonoBehaviour
{
    [SerializeField] private GameObject _zapparTracker;
    [SerializeField] private GameObject _zapparCamera;
    [SerializeField] private GameObject _setUpBtn;
    [SerializeField] private GameObject _selectPanel;
    [SerializeField] private StartPlace _startPlace;


    public void PlaceHouse(int index)
    {
        _zapparTracker.SetActive(true);
        _zapparCamera.SetActive(true);
        _startPlace.IndexHouse = index;
        _setUpBtn.SetActive(true);
        
        _selectPanel.SetActive(false);
        this.gameObject.SetActive(false);
        //Instantiate(_houses[index], Vector3.zero, Quaternion.identity, ZapparTracker.transform);
        //_zapparInstant.PlaceTrackerAnchor();
        //_drawPanel.SetActive(true);
    }
}
