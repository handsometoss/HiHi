using System.Reflection;
using System.Windows;

namespace BackGroundWorkerExample
{
    static class LogInfo
    {
        public static void Logging()
        {
            var className = MethodBase.GetCurrentMethod().DeclaringType.ToString();
            var methodName = MethodBase.GetCurrentMethod().Name;
            MessageBox.Show(className + " - " + methodName);
        }
    }
}
