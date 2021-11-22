using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace AppLib
{
    public class MemoryManager
    {
        private static object _lock = new object();

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minimumWorkingSetSize, int maximumWorkingSetSize);

        static MemoryManager() { }

        public static void ReleaseUnusedMemory(bool runGC = false)
        {
            if (runGC)
            {
                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();
            }
            try
            {
                lock (MemoryManager._lock)
                    MemoryManager.SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1);
            }
            catch
            {
            }
        }
    }
}
