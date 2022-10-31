namespace Application;

public class ApplicationOptions
{
    public const string ConfigurationEntry = "ApplicationModule";

    public string ConfigLocation { get; set; }

    public string GetPath(string file)
    {
        return Path.GetFullPath($"{this.ConfigLocation}/{file}");
    }
}