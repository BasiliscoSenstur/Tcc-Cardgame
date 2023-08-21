using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BattleAbstract
{
    public abstract void EnterState(BattleController battle);
    public abstract void LogicsUpdate(BattleController battle);
    public abstract void ExitState(BattleController battle);
}
