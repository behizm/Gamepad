namespace Gamepad.Service.Interfaces
{
    public interface IBaseService<out TInterface>
    {
        TInterface Clone();
    }
}
