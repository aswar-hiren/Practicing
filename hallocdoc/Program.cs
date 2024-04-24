
using DataLayer.DataContext;
using Microsoft.EntityFrameworkCore;
using LogicLayer.Interface_patient;
using LogicLayer.Repositary_patient;
using LogicLayer.Repo_admin;
using LogicLayer.Interface_Admin;
using LogicLayer.Repo_Provider;
using LogicLayer.Interface_Provider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var provider = builder.Services.BuildServiceProvider();
var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<HellodocPrjContext>(item =>   item.UseNpgsql(config.GetConnectionString("dbcs")));

builder.Services.AddHttpClient();
builder.Services.AddScoped<IPatientRequest, PatientRequest>();
builder.Services.AddScoped<IPatientLogin, PatientLogin>();
builder.Services.AddScoped<IFamilyFriend, FamilyFriend>();
builder.Services.AddScoped<IBusiness, BusinessData>();
builder.Services.AddScoped<IConcierge, ConciegeData>();
builder.Services.AddScoped<IPatientDashBoard, PatientDashBoardClass>();
builder.Services.AddScoped<IPatientDashForm, PatientDashForm>();
builder.Services.AddScoped<IViewDocument, ViewDocumentClass>();
builder.Services.AddScoped<IDownlod, DownlodClass>();
builder.Services.AddScoped<IReqWiseFiles, ReqWiseFileClass>();
builder.Services.AddScoped<IUpdatePatientProfile, UpdatePatProfile>();
builder.Services.AddScoped<IEmailsender, Emailsender>();
builder.Services.AddScoped<IResetPassword, ResetPassword>();
builder.Services.AddScoped<ICreatePatientReq, CreatePatient>();
builder.Services.AddScoped<IAdminRequest, AdminRequest>();
builder.Services.AddScoped<IProviderPanel, ProviderPanel>();
builder.Services.AddSession();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
