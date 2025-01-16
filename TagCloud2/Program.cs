// See https://aka.ms/new-console-template for more information

using SimpleInjector;
using TagCloud2;
using TagCloud2.Abstract;
using TagCloud2.Infrastructure;
using TagsCloudVisualization.Extensions;

var serviceCollection = new Container(); 
serviceCollection.RegisterTagCloud()
    .RegisterLayouter()
    .RegisterTextModule()
    .RegisterImageModule()
    .RegisterSettingsCloud();

serviceCollection.Register<ITagCloudController, TagCloudCli>(Lifestyle.Singleton);
serviceCollection.Register(() => new InputData(args), Lifestyle.Singleton);
serviceCollection.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);

var application = serviceCollection.GetInstance<ITagCloudController>();

application.Run();