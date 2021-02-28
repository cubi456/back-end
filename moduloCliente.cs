using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;  

namespace back_end
{
    public class moduloCLiente : NancyModule
    {
            //Collecion de clientes que servira como almacenamiento
            private static List<Cliente> clientes {get; set;} = new List<Cliente>();
            public moduloCLiente()
            {
                //Solicitud para obtener los clientes
                Get("/clientes",retornarClientes=> {
                    
                    if(clientes.Count != 0)
                        return Response.AsJson<Cliente[]>(clientes.ToArray());
                    else
                        return 500;
                });
                //Solicitud para almacenar clientes
                Post("/clientes", guardarCliente =>{
                   var cliente = this.Bind<Cliente>();
                   if(cliente!=null)
                   {
                       clientes.Add(cliente);
                       return 200;
                   }
                   else
                        return 500;
                });
            }
    }
}