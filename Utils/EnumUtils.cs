using System;
using System.Collections.Generic;
using System.Linq;

namespace Feralnex.Networking
{
    public static class EnumUtils
    {
        private static IEnumerable<Type> _assemblyTypes;

        static EnumUtils()
        {
            _assemblyTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
        }

        public static Type GetType(string enumName) => _assemblyTypes.FirstOrDefault(type => type.Name == enumName);
    }
}
