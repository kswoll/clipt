using System.Text;
using Clipt.WinApis;

namespace Clipt.Utils
{
    public class ProcessUtils
    {
        public string GetProcessImageFileName(uint processId)
        {
            var processHandle = WinApi.OpenProcess(WinApi.PROCESS_ALL_ACCESS, 0, processId);
            var buffer = new StringBuilder(2048);
            WinApi.GetProcessImageFileName(processHandle, buffer, (uint)buffer.Capacity);
            WinApi.CloseHandle(processHandle);
            return buffer.ToString();
        }
    }
}