using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zappar;

public class StartPlace : MonoBehaviour
{
    [SerializeField] private List<GameObject> _houses;
    [SerializeField] private ZapparInstantTrackingTarget _instantTracking;
    [SerializeField] private GameObject _drawPanel;
    [SerializeField] private GameObject _tracker;

    public int IndexHouse { get; set; }

    public void PlaceHouse()
    {
        var position = new Vector3(_tracker.transform.position.x, _tracker.transform.position.y,
            _tracker.transform.position.z + 2f);
        Instantiate(_houses[IndexHouse], position, Quaternion.Euler(-90f, 0, 0), this.transform);
        _instantTracking.PlaceTrackerAnchor();
        _drawPanel.SetActive(true);
        
        _tracker.SetActive(false);
    }
}
