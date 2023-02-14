using UnityEngine;

public class Stepper : MonoBehaviour
{
    public bool Paused { get; set; } = true;

    public float StepsPerSecond { get; set; } = 1;

	[SerializeField] private Grid _grid;

    private float _pause;

	private void Awake()
	{
        ResetPause();
	}

    private void Update()
    {
        if (Paused)
            return;

        if (_pause > 0)
        {
            _pause -= Time.deltaTime;
            return;
        }

        TakeStep();
	}

    public void TakeStep()
    {
		_grid.TakeStep();
		ResetPause();
	}

    private void ResetPause()
    {
		_pause = 1f / StepsPerSecond;
	}
}
