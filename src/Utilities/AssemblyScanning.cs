using System.Reflection;

namespace AOC2022.Utilities;

public static class AssemblyScanning
{
    public static IEnumerable<Type> RetrieveSubclassesOf<T>()
    {
        return Assembly
            .GetEntryAssembly()! // won't call from unmanaged code
            .GetExportedTypes()
            .Where(
                type =>
                    type.IsSubclassOf(
                        typeof(T)));
    }
}