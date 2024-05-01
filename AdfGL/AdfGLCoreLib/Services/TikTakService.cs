using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGLCoreLib.Services
{
    public static class TikTakService
    {
        static TikTak _tikTak = new TikTak();

        public static TikTak TikTak
        {
            get
            {
                return _tikTak;
            }
        }
    }

    public class TikTak
    {
        public int Ms { get; set; }

        public bool IsRunning { get; private set; } = false;

        public void Reset()
        {
            Ms = 0;
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            Ms = 0;
        }

        public int Stop()
        {
            if (false == IsRunning) return -1;
            IsRunning = false;
            return Ms;
        }
    }
}
