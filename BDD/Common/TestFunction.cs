using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BDD.Common
{
    public class TestFunction
    {
        public static void EnsureTestMethodPresent(Type type, string testMethodName)
        {
            Assert.IsTrue(IsValidTestMethod(type, testMethodName));
        }
        public static bool IsValidTestMethod(Type type, string testMethodName)
        {
            if (!type.GetCustomAttributes(false).OfType<TestClassAttribute>().Any()) return false;
            var mInfo = type.GetMethod(testMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);
            return mInfo != null && mInfo.GetCustomAttributes(false).OfType<TestMethodAttribute>().Any();
        }
    }
}
