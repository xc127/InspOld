using MahApps.Metro;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Main
{
    public class CimHelper
    {
        static object _locker_cim = new object();
        static object _locker_ftp = new object();

        static List<string> PathsForCim = new List<string>();
        static List<string> PathsForFtp = new List<string>();

        public static int Count = 0;

        public static string GetPathsForCim(int index)
        {
            lock(_locker_cim)
            {
                return PathsForCim[index];
            }
        }

        public static string GetPathsForFtp(int index)
        {
            lock (_locker_ftp)
            {
                return PathsForFtp[index];
            }
        }

        public static List<string> GetFtpPaths() => PathsForFtp.ToList();

        static void AddPathForCim(string path)
        {
            lock (_locker_cim)
            {
                PathsForCim.Add(path);
            }
        }

        static void AddPathForFtp(string path)
        {
            lock (_locker_ftp)
            {
                PathsForFtp.Add(path);
            }
        }

        public static void Add(string path1,string path2)
        {
            AddPathForCim(path1);
            AddPathForFtp(path2);
            Count++;
        }

        static void ResetPathForCim()
        {
            lock (_locker_cim)
            {
                PathsForCim.Clear();
            }                
        }

        static void ResetPathForFtp()
        {
            lock (_locker_ftp)
            {
                PathsForFtp.Clear();
            }
        }

        public static void Reset()
        {
            ResetPathForCim();
            ResetPathForFtp();
            Count = 0;
        }
    }

    public class StringPair
    {
        public string String1 { get; set; }
        public string String2 { get; set; }
    }
}
