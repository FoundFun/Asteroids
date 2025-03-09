using UnityEngine;

namespace InputBehaviour
{
    public static class InputDevice
    {
        public static DeviceType GetDeviceType()
        {
            DeviceType deviceType = SystemInfo.deviceType;
            return deviceType;
        }
    }
}
