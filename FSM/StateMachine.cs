using UnityEngine;

public class StateMachine
{
	IState currentState;
	IState previousState;

	public void ChangeState(IState newState)
	{
		if (this.currentState != null)
			this.currentState.Exit();

		this.previousState = this.currentState;
		this.currentState = newState;
		this.currentState.Enter();
	}

	public void ExecuteState() 
	{
		var runningState = this.currentState;
		if (runningState != null)
		{
			runningState.Execute();
		}
	}

	public void SwitchToPreviousState()
	{
		if (previousState != null)
		{
			this.currentState.Exit();
			this.currentState = this.previousState;
			this.currentState.Enter();
		}
	}

    public IState GetCurrentState()
    {
        return currentState != null ? currentState : null;
    }
}