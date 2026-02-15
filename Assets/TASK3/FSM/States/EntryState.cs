using AxGrid.FSM;
using AxGrid.Model;

[State("Entry")]
public class EntryState : FSMState
{
    [Enter]
    private void Enter()
    {
        Model.Set("StartBtnEnable", true);
        Model.Set("StopBtnEnable", false);
    }

    [Bind("OnBtn")]
    private void StartRollHandler(string btnName)
    {
        if (btnName != "Start")
        {
            return;
        }

        Parent.Change("Rolling");
    }
}
