using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StepPanel : MonoBehaviour
{
	[Header("UI elements")]
    [SerializeField] GameObject _gridPanel;
	[SerializeField] TMP_Text _countText;

	[Header("Behaviours")]
	[SerializeField] private Stepper _stepper;

	public void OnStepSettingsClick()
	{
		gameObject.SetActive(false);
		_gridPanel.gameObject.SetActive(true);
	}

	public void OnPlayClick()
	{
		_stepper.Paused = false;
	}

	public void OnPauseClick()
	{
		_stepper.Paused = true;
	}

	public void OnNextClick()
	{
		_stepper.TakeStep();
	}

	public void OnSliderValueChanged(float value)
	{
		_stepper.StepsPerSecond = value;
		_countText.text = $"{value:0.00}";
	}
}
