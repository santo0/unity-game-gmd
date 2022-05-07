

public class BossStateLoader : StateLoader
{
    public BossIdleState initialState;
    public override State LoadInitialState()
    {
        return initialState;
    }
}