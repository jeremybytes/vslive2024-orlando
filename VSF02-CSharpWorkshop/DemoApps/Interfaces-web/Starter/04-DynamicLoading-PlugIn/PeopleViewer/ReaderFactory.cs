using PersonReader.Interface;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace PeopleViewer;

public class ReaderFactory
{
    private IConfiguration Configuration;
    public ReaderFactory(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private static IPersonReader? reader;

    public IPersonReader GetReader()
    {
        if (reader != null)
            return reader;

        string? readerAssemblyName = Configuration["DataReader:ReaderAssembly"];
        string readerLocation = AppDomain.CurrentDomain.BaseDirectory
                                + "ReaderAssemblies"
                                + Path.DirectorySeparatorChar
                                + readerAssemblyName;

        ReaderLoadContext loadContext = new ReaderLoadContext(readerLocation);
        AssemblyName assemblyName = new AssemblyName(Path.GetFileNameWithoutExtension(readerLocation));
        Assembly readerAssembly = loadContext.LoadFromAssemblyName(assemblyName);

        string? readerTypeName = Configuration["DataReader:ReaderType"];
        Type readerType = readerAssembly.ExportedTypes
                            .First(t => t.FullName == readerTypeName);
        reader = Activator.CreateInstance(readerType) as IPersonReader;
        return reader ?? throw new InvalidOperationException($"Unable to create instance of {readerType} as IPersonReader");
    }
}
