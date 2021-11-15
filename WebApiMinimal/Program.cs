using Microsoft.EntityFrameworkCore;
using WebApiMinimal.Contexto;
using WebApiMinimal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<Contexto>
    (options => options.UseSqlServer(
       // "Data Source=localhost;Initial Catalog=Clientes_Catalog;Integrated Security=False;User ID=se; Password=1234;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False"));
       "Server = localhost; Database = Clientes_Catalog; Trusted_Connection = True; MultipleActiveResultSets = true"));

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.MapPost("Adicionar-EditarCliente", async (Clientes cliente, Contexto contexto) =>
{
    contexto.cliente.Add(Clientes);
    await contexto.SaveChangesAsync();

});


app.MapPost("ExcluirCliente/{id}", async (int id, Contexto contexto) =>
{
    var excluirCliente = await contexto.cliente.FirstOrDefaultAsync(p => p.Id == id);
    if (excluirCliente != null)
    {
        contexto.cliente.Remove(excluirCliente);
        await contexto.SaveChangesAsync();
    }
});

app.MapPost("ListarClienteAtivo", async (int id, Contexto contexto) =>
{
    return await contexto.cliente.ToListAsync();

});

app.UseSwaggerUI();

app.Run();
