using APIsDeCondomino.model;

var builder = WebApplication.CreateBuilder(args);
//Logger.Info("Iniciando a aplicação...1");
// Add services to the container.
StrCaminho conDBF = new StrCaminho();
//Logger.Info("Iniciando a aplicação...2");
//builder.Configuration.GetSection("ConnectionStrings").Bind(conDBF);
builder.Configuration.GetSection("Caminhos").Bind(conDBF);
//Logger.Info($"Caminho do DBF: {conDBF.CaminhoDBF}");
builder.Services.AddSingleton(conDBF);
//Logger.Info("Caminho do DBF configurado com sucesso.");

builder.Services.AddControllers();
//Logger.Info("Serviços de controle adicionados com sucesso.");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//Logger.Info("Explorador de pontos finais adicionado com sucesso.");
builder.Services.AddSwaggerGen();
//Logger.Info("Swagger configurado com sucesso.");
var app = builder.Build();
//Logger.Info("Aplicação construída com sucesso.");
//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
//Logger.Info("Swagger ativado com sucesso.");
app.UseSwaggerUI();
//Logger.Info("Interface do Swagger UI ativada com sucesso.");
//}

app.UseHttpsRedirection();
//Logger.Info("Redirecionamento HTTPS configurado com sucesso.");
app.UseAuthorization();
//Logger.Info("Autorização configurada com sucesso.");
app.MapControllers();
//Logger.Info("Mapeamento de controladores concluído com sucesso.");
app.Run();
//Logger.Info("Aplicação em execução com sucesso.");