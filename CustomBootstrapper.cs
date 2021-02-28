using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

public class CustomBootstrapper : DefaultNancyBootstrapper
{
    protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
    {
        activarCors(pipelines);
    }

    public void activarCors(IPipelines pipelines)    
    {
        pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
        {
            ctx.Response.WithHeader("Access-Control-Allow-Origin", "*")
                            .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                            .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
        });
    }
}