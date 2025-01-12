using SimpleInjector;
using TagsCloudVisualization.Abstraction;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization.Extensions;

public static class ExtensionDi
{
    // по сути, можно было бы добавить дополнительный интерйс, чтоб работать не с Continer на прямую, но так как создавать мне лень. я его и не сделал. (иначе говоря, это overhead)
    public static Container RegisterSettingsCloud(this Container container)
    {
        container.Register<AppSettings>(Lifestyle.Singleton);
        container.Register<TagCloudSettings>(Lifestyle.Singleton);
        container.Register<WordLoaderSettings>(Lifestyle.Singleton);
        return container;
    }

    public static Container RegisterLayouter(this Container container)
    {
        container.Register<ICloudLayouter, CircularCloudLayouter>(Lifestyle.Singleton);
        return container;
    }


    public static Container RegisterTextModule(this Container container)
    {
        container.Register<IWordLoader, FileWordLoader>(Lifestyle.Singleton);
        container.Register<FactoryStem>(Lifestyle.Singleton);
        return container;
    }

    public static Container RegisterImageModule(this Container container)
    {
        container.Register<AbstractFactoryBitMap, FactoryBitMap>(Lifestyle.Singleton);
        container.Register<ISizeWord, MeasureString>(Lifestyle.Singleton);
        return container;
    }

    public static Container RegisterTagCloud(this Container container)
    {
        container.Register<TagCloud>(Lifestyle.Singleton);

        return container;
    }
}