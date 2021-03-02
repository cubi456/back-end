using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System.Collections.Generic;  
using System;
namespace back_end{
public class CustomBootstrapper : DefaultNancyBootstrapper
{
    protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context)
    {
        // Antes del request calculo el tiempo y lo agrego al contexto.   
        pipelines.BeforeRequest += ctx =>{
            ctx.Items.Add("Tiempo_inicio",DateTime.UtcNow);
            return null;
        };
        //Tomo el tiempo del contexto y calculo el tiempo transcurrido
         pipelines.AfterRequest += ctx =>{
            var tiempoDeEjec = (DateTime.UtcNow - (DateTime)ctx.Items["Tiempo_inicio"]).TotalMilliseconds;
            Console.WriteLine("Tiempo de ejecucion: "+tiempoDeEjec.ToString()+"ms");
        };

        activarCors(pipelines);
    }

    /*
    *   Inyecto la lista de clientes en el modulo.
    */
    protected override void ConfigureApplicationContainer(TinyIoCContainer container)
    {
        base.ConfigureApplicationContainer(container);
        container.Register<List<Cliente>>(new List<Cliente>());
    }

    /**
    *   Metodo para permitir conexiones.
    */
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
}