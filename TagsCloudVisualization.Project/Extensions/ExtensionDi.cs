using SimpleInjector;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Extensions;

public static class ExtensionDi
{
    // по сути, можно было бы добавить дополнительный интерйс, чтоб работать не с Continer на прямую, но так как создавать мне лень. я его и не сделал.
    public static void RegisterSettingsCloud(this Container container)
    {
        container.Register<AppSettings>(Lifestyle.Singleton);
        container.Register<TagCloudSettings>(Lifestyle.Singleton);
        container.Register<WordLoaderSettings>(Lifestyle.Singleton);
    }

    public static void RegisterLayouter(this Container container) 
    {
        container.Register<ICloudLayouter, CircularCloudLayouter>(Lifestyle.Singleton);
    }
}