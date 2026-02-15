using AxGrid.FSM;
using AxGrid.Model;

[State("End")]
public class EndState : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.Set("IsScrolling", false);
        Model.Set("StopBtnEnable", false);
    }

    [Bind("OnScrollEnded")]
    private void EndedRollHandler()
    {
        Parent.Change("Entry");
    }
}
