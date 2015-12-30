using System;
using Gamepad.Service.Services;

namespace Gamepad.Service.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class EventsAttr : Attribute
    {
        public EventsAttr()
        {
            EventManager.Laod();
        }
    }
}
