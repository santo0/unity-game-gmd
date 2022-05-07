

public class MushroomStateLoader: StateLoader{
    public MushroomIdleState initialState;
    public override State LoadInitialState()
    {
        return initialState;
    }
}