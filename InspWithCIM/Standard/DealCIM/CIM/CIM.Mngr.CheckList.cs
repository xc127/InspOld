using System.Collections.Generic;

namespace DealCIM
{
    partial class CIM
    {
        static object list_lock = new object();

        static List<string> list_id = new List<string>();

        public static void AddIDToList(string id)
        {
            list_id.Add(id);
        }

        public static bool RemoveIDFromList(string id)
        {
            lock (list_lock)
            {
                if (list_id.Contains(id))
                {
                    list_id.Remove(id);
                    return true;
                }
                else
                    return false;
            }
        }

        public static int GetListCnt()
        {
            return list_id.Count;
        }

        public static void ClearList()
        {
            list_id.Clear();
        }

        public static string GetListContent()
        {
            return string.Join(",", list_id.ToArray());
        }
    }
}
