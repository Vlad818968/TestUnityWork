using AxGrid.FSM;
using AxGrid.Model;

[State("Rolling")]
public class RollingState : FSMState
{
    [Enter]
    private void StartScroll()
    {
        Model.Set("StartBtnEnable", false);
        Model.Set("IsScrolling", true);
    }

    [One(3)]
    private void ActivateStopBtn()
    {
        Model.Set("StopBtnEnable", true);
    }

    [Bind("OnBtn")]
    private void StopRollHandler(string btnName)
    {
        if (btnName != "Stop")
        {
            return;
        }

        Parent.Change("End");
    }
}
