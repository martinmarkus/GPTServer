namespace GPTServer.Common.Core.Utils.GeneralUtils.Assembly
{
    public static class AssemblyUtils
    {
        public static System.Reflection.Assembly GetByName(string name) =>
            AppDomain.CurrentDomain
                .GetAssemblies()
                .SingleOrDefault(assembly => assembly
                .GetName().Name
                .Equals(name, StringComparison.OrdinalIgnoreCase));
    }
}
