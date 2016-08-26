using UnityEngine;
using System.Collections;

public class TorusBehaviour : MonoBehaviour 
{
    [SerializeField] private int _segmentCounterSmall = 5;    
    [SerializeField] private int _segmentCounterBig = 5;

    [SerializeField] private float _smallRadius = 5f;
    [SerializeField] private float _bigRadius = 15f;

    [SerializeField] private GameObject _patternPoint;

    [SerializeField] private Color[] _colors;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CreateAndInitAllPoints()
    {
    }

    private void Rotation()
    {
    }
}
