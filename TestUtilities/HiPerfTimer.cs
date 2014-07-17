using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading;

namespace TestUtilities
{
    /// <summary>
    /// High Performance Timer that calculates runtime of operations.
    /// Use Start to begin timing and Stop to stop the timer.
    /// PrintDuration outputs timer runtime to console in seconds.
    /// Use Reset to clear the time before timing another operation.
    /// </summary>
    public class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
            out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
            out long lpFrequency);

        private long startTime, stopTime;
        private long freq;

        // Constructor
        public HiPerfTimer()
        {
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {
                // high-performance counter not supported
                throw new Win32Exception();
            }
        }

        // Start the timer
        public void Start()
        {
            // lets do the waiting threads there work
            Thread.Sleep(0);

            QueryPerformanceCounter(out startTime);
        }

        // Stop the timer
        public void Stop()
        {
            QueryPerformanceCounter(out stopTime);
        }

        // Returns the duration of the timer (in seconds)
        public double Duration
        {
            get
            {
                return (double)(stopTime - startTime) / (double)freq;
            }
        }

        public void Reset()
        {
            startTime = 0;
            stopTime = 0;
        }

        public void PrintDuration()
        {
            Console.WriteLine("Duration: {0} sec\n", this.Duration);
        }

    }
}
