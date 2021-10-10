public interface ITowerState
{
    public void Handle(AtackTower tower);
    public void Handle(FarmTower tower);
    public void DisHandle();
}