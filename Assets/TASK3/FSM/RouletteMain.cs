using AxGrid;
using AxGrid.FSM;
using AxGrid.Base;
using UnityEngine;

public class RouletteMain : MonoBehaviourExt
{
    [OnStart]
    private void Init()
    {
        Settings.Fsm = new FSM();
        Settings.Fsm.Add(new EntryState());
        Settings.Fsm.Add(new RollingState());
        Settings.Fsm.Add(new EndState());

        Settings.Fsm.Start("Entry");
    }

    [OnUpdate]
    private void Logic()
    {
        Settings.Fsm.Update(Time.deltaTime);
    }
}
