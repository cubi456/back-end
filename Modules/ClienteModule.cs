using Nancy;
using Nancy.ModelBinding;
using System.Collections.Generic;  
using System;


namespace back_end
{
    public class ClienteModule : NancyModule
    {

        
            public ClienteModule(List<Cliente> clientes)
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