using TMPro;
using UnityEngine;

public class GridPanel : MonoBehaviour
{
    [Header("UI elements")]
    [SerializeField] GameObject _stepPanel;
    [SerializeField] TMP_InputField _heightInputField;
    [SerializeField] TMP_InputField _widthInputField;

    [Header("Behaviours")]
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private Grid _grid;


    private int _height = 5, _widht = 5;

    private const int MIN_VALUE = 4;
    private const int MAX_VALUE = 200;

    public void OnStepSettingsClick()
    {
        gameObject.SetActive(false);
        _stepPanel.gameObject.SetActive(true);
    }

    public void OnHeightInputFieldEndEdit(string str)
    {
        OnInputFieldEndEdit(str, _heightInputField, true);
    }

	public void OnWidthInputFieldEndEdit(string str)
	{
		OnInputFieldEndEdit(str, _widthInputField, false);
	}

	private void OnInputFieldEndEdit(string str, TMP_InputField field, bool isHeight)
    {
        int value = int.Parse(str);
        if (value > MAX_VALUE || value < MIN_VALUE)
        {
            value = Mathf.Clamp(value, MIN_VALUE, MAX_VALUE);
            field.text = value.ToString();
        }

        if (isHeight)
            _height = value;
        else
            _widht = value;
    }

    public void OnResetClick()
    {
		_grid.ResetGrid(_widht, _height);
		_camera.ResetCamera();       
    }
}
