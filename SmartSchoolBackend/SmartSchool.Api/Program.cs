
////using JWT.Context;
//using JWT.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using SmartSchool.Bl.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Repository;
using SmartSchool.BL.Services;
using SmartSchool.DAL;
using SmartSchool.DAL.Context;
using Stripe;
using System.Net.NetworkInformation;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace SmartSchool.Api
{
    
    public class Program
    {
       
        public static void Main(string[] args)
        {

            string txt = "hi";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<JWTC>(builder.Configuration.GetSection("JWT"));

            

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //adding files service
            builder.Services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            //services of using conection string
            builder.Services.AddDbContext<SmartSchoolContext>(opt =>
            opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
			   .AddEntityFrameworkStores<SmartSchoolContext>();

            builder.Services.AddScoped<IRequestRepo, RequestRepo>();
            builder.Services.AddScoped<IGradeYearRepo, GradeYearRepo>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IClassRoomRepo, ClassRoomRepo>();
            builder.Services.AddScoped<IScheduleRepo, ScheduleRepo>();
            builder.Services.AddScoped<ISessionRepo, SessionRepo>();
            builder.Services.AddScoped<ISubjectRepo, SubjectRepo>();
            builder.Services.AddScoped<IParentRepo, ParentRepo>();
            builder.Services.AddScoped<IStudentRepo, StudentRepo>();
            builder.Services.AddScoped<IStudentAttendanceRepo, StudentAttendanceRepo>();
            builder.Services.AddScoped<ITeacherAttendanceRepo, TeacherAttendanceRepo>();
            builder.Services.AddScoped<IComplaintRepo, ComplaintRepo>();
            builder.Services.AddScoped<IExamResultRepo, ExamResultRepo>();
            builder.Services.AddScoped<IMaterialRepo, MaterialRepo>();
            builder.Services.AddTransient<IMailService, MailService>();
            builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();

			builder.Services.AddScoped<TokenService>();
			builder.Services.AddScoped<CustomerService>();
			builder.Services.AddScoped<ChargeService>();
			StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("StripeOptions:SecretKey");


			builder.Services.AddScoped<ITeacherRepo, TeacherRepo>();
            //service of using jwt Authentication
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.RequireHttpsMetadata = false;
                o.SaveToken = false;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = builder.Configuration["JWT:Issuer"],
                    ValidAudience = builder.Configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                };
            });
            builder.Services.AddCors(o =>
            {
                o.AddPolicy(txt, builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            app.UseCors(txt);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseStaticFiles();
//            app.UseStaticFiles(new StaticFileOptions
//            {
//                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources"))
//,
//                RequestPath = new PathString("/Resources")
//            });
            app.MapControllers();

            app.Run();
        }
    }
}