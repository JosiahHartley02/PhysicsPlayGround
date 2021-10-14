using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTextBehavior : MonoBehaviour
{
    //Reference to the textMesh for character size
    private TextMesh textReference;
    //Reference to the size of the size of the text
    private int _currentTextSize;
    [SerializeField]
    private int _visibleTextSize = 1;
    //Float decrementing as delta time elapses
    [SerializeField]
    private float _timeTillToggle;
    //Bool determining whether this instance should toggle
    private bool _shouldToggle;

    private void Start()
    {
        //Set the time till toggle to default to zero
        _timeTillToggle = 0;
        //Set should toggle to be false, this will be changed by other functions solely
        _shouldToggle = false;
        //Get a reference to the text mesh used to display text
        textReference = GetComponent<TextMesh>();
        //Determine size of text currently being displayed, 0 acts as not visible
        _currentTextSize = (int)textReference.characterSize;
        //Get the size of the text being displayed for toggling
        if (_currentTextSize != 0)
            _visibleTextSize = (int)textReference.characterSize;
    }
    private void Update()
    {
        if (!_shouldToggle)
            return;
        _timeTillToggle -= Time.deltaTime;
        if (_timeTillToggle <= 0 && _shouldToggle)
        {
            ToggleText();
            _shouldToggle = false;
        }
        
    }

    public void ToggleText()
    {
        if (!textReference)
            return;

        switch(_currentTextSize)
        {
            case 0:
                textReference.characterSize = _visibleTextSize;
                break;
            default:
                textReference.characterSize = 0;
                break;
        }

        //Determine size of text currently being displayed, 0 acts as not visible
        _currentTextSize = (int)textReference.characterSize;
    }

    public void ToggleAfter(float time)
    {
        _shouldToggle = true;
        _timeTillToggle = time;
    }
}
