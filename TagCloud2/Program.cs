// See https://aka.ms/new-console-template for more information

using SimpleInjector;
using TagCloud2;
using TagCloud2.Abstract;
using TagCloud2.Infrastructure;
using TagsCloudVisualization.Extensions;

var serviceCollection = new Container(); // можно было бы для еще более жесткой красоты добавить интерейс под создания все сущностей для реализаций облака для di. типо Container.RegTagCloud.[OnlyMethodsForRegisterTagCLoud].Completed. и дальше отсальное что нужно для di. кароче сделать паттерн строитель. но это overhead.

serviceCollection.RegisterTagCloud()
    .RegisterLayouter()
    .RegisterTextModule()
    .RegisterImageModule()
    .RegisterSettingsCloud();


serviceCollection.Register<ITagCloudController, TagCloudCli>(Lifestyle.Singleton);
serviceCollection.Register<IInputData>(() => new InputData(args), Lifestyle.Singleton);
serviceCollection.Register<ILogger, ConsoleLogger>(Lifestyle.Singleton);

var application = serviceCollection.GetInstance<ITagCloudController>();

application.Run();