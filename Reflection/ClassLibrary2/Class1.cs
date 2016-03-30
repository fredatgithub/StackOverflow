using System;
using System.Reflection;

namespace ClassLibrary2
{
    public class Class1
    {
        private delegate void MyDelegate();

        public Class1()
        {
            MyDelegate myDelegate = () => { };

            var a = myDelegate.Method;
            var b = typeof(string).BaseType;
            var c = Type.FilterName;
            object d = typeof(string).InvokeMember(null, BindingFlags.Default, null, null, null);
            var e = typeof(string).FindMembers(MemberTypes.All, BindingFlags.Default, null, null);
        }
    }
}
