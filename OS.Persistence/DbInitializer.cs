using OS.Domain;
using OS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace OS.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(OSDbContext context)
        {
            context.Database.EnsureCreated();
        }

        public async static Task RunSeed(ConfigurationManager configurationManager, IServiceProvider serviceProvider)
        {
            var admin_password = configurationManager["Passwords:Admin_Password"];
            var user_password = configurationManager["Passwords:User_Password"];
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            var context = serviceProvider.GetRequiredService<OSDbContext>();

            #region Languages
            Language uzLang;
            Language ruLang;
            if (!await context.Languages.AnyAsync())
            {
                uzLang = new Language
                {
                    Code = "UZ",
                    Flag = "uz_flag",
                    IsDefault = true,
                    Name = "O'zbekcha"
                };
                ruLang = new Language
                {
                    Code = "RU",
                    Flag = "ru_flag",
                    IsDefault = false,
                    Name = "Русский"
                };

                await context.Languages.AddAsync(uzLang);
                await context.Languages.AddAsync(ruLang);
                await context.SaveChangesAsync();
            }
            else
            {
                uzLang = await context.Languages.FirstAsync(l => l.Code == "UZ");
                ruLang = await context.Languages.FirstAsync(l => l.Code == "RU");
            }
            #endregion Languages

            #region Regions
            if (!await context.Regions.AnyAsync())
            {
                var andijonRegion = new Region { Code = "AND" };
                var namanganRegion = new Region { Code = "NAM" };
                var fargonaRegion = new Region { Code = "FAR" };
                var toshkentViloyatiRegion = new Region { Code = "TV" };
                var toshkentShaharRegion = new Region { Code = "TS" };
                var sirdaryoRegion = new Region { Code = "SIR" };
                var jizzaxRegion = new Region { Code = "JIZ" };
                var samarqandRegion = new Region { Code = "SAM" };
                var surxandaryoRegion = new Region { Code = "SUR" };
                var qashqadaryoRegion = new Region { Code = "QAR" };
                var navoiyRegion = new Region { Code = "NAV" };
                var buxoroRegion = new Region { Code = "BUX" };
                var xorazmRegion = new Region { Code = "XR" };
                var qqRegion = new Region { Code = "QQ" };

                await context.Regions.AddRangeAsync(andijonRegion, namanganRegion, fargonaRegion, toshkentViloyatiRegion,
                    toshkentShaharRegion, sirdaryoRegion, jizzaxRegion, samarqandRegion, surxandaryoRegion, qashqadaryoRegion,
                    navoiyRegion, buxoroRegion, xorazmRegion, qqRegion);
                await context.SaveChangesAsync();

                var andijonTUZ = new RegionTranslate { Language = uzLang, Region = andijonRegion, Name = "Andijon viloyati" };
                var andijonTRU = new RegionTranslate { Language = ruLang, Region = andijonRegion, Name = "Андижанская область" };

                var namanganTUZ = new RegionTranslate { Language = uzLang, Region = namanganRegion, Name = "Namangan viloyati" };
                var namanganTRU = new RegionTranslate { Language = ruLang, Region = namanganRegion, Name = "Наманганская область" };

                var fargonaTUZ = new RegionTranslate { Language = uzLang, Region = fargonaRegion, Name = "Farg'ona viloyati" };
                var fargonaTRU = new RegionTranslate { Language = ruLang, Region = fargonaRegion, Name = "Ферганская область" };

                var toshkentviloyatTUZ = new RegionTranslate { Language = uzLang, Region = toshkentViloyatiRegion, Name = "Toshkent viloyati" };
                var toshkentviloyatTRU = new RegionTranslate { Language = ruLang, Region = toshkentViloyatiRegion, Name = "Ташкентская область" };

                var toshkentShaharTUZ = new RegionTranslate { Language = uzLang, Region = toshkentShaharRegion, Name = "Toshkent shahri" };
                var toshkentShaharTRU = new RegionTranslate { Language = ruLang, Region = toshkentShaharRegion, Name = "город Ташкент" };

                var sirdaryoTUZ = new RegionTranslate { Language = uzLang, Region = sirdaryoRegion, Name = "Sirdaryo viloyati" };
                var sirdaryoTRU = new RegionTranslate { Language = ruLang, Region = sirdaryoRegion, Name = "Сырдарьинская область" };

                var jizzaxTUZ = new RegionTranslate { Language = uzLang, Region = jizzaxRegion, Name = "Jizzax viloyati" };
                var jizzaxTRU = new RegionTranslate { Language = ruLang, Region = jizzaxRegion, Name = "Джизакская область" };

                var samarqandTUZ = new RegionTranslate { Language = uzLang, Region = samarqandRegion, Name = "Samarqand viloyati" };
                var samarqandTRU = new RegionTranslate { Language = ruLang, Region = samarqandRegion, Name = "Самаркандская область" };

                var surxandaryoTUZ = new RegionTranslate { Language = uzLang, Region = surxandaryoRegion, Name = "Surxandaryo viloyati" };
                var surxandaryoTRU = new RegionTranslate { Language = ruLang, Region = surxandaryoRegion, Name = "Сурхандарьинская область" };

                var qashqadaryoTUZ = new RegionTranslate { Language = uzLang, Region = qashqadaryoRegion, Name = "Qashqadaryo viloyati" };
                var qashqadaryoTRU = new RegionTranslate { Language = ruLang, Region = qashqadaryoRegion, Name = "Кашкадарьинская область" };

                var navoiyTUZ = new RegionTranslate { Language = uzLang, Region = navoiyRegion, Name = "Navoiy viloyati" };
                var navoiyTRU = new RegionTranslate { Language = ruLang, Region = navoiyRegion, Name = "Навоийская область" };

                var buxoroTUZ = new RegionTranslate { Language = uzLang, Region = buxoroRegion, Name = "Buxoro viloyati" };
                var buxoroTRU = new RegionTranslate { Language = ruLang, Region = buxoroRegion, Name = "Бухарская область" };

                var xorazmTUZ = new RegionTranslate { Language = uzLang, Region = xorazmRegion, Name = "Xorazm viloyati" };
                var xoramzTRU = new RegionTranslate { Language = ruLang, Region = xorazmRegion, Name = "Хорезмская область" };

                var qqTUZ = new RegionTranslate { Language = uzLang, Region = qqRegion, Name = "Qoraqalpog'iston Respublikasi" };
                var qqTRU = new RegionTranslate { Language = ruLang, Region = qqRegion, Name = "Республика Каракалпакстан" };

                await context.RegionTranslates.AddRangeAsync(andijonTUZ, andijonTRU, namanganTUZ, namanganTRU, fargonaTUZ, fargonaTRU, toshkentviloyatTUZ,
                    toshkentviloyatTRU, toshkentShaharTUZ, toshkentShaharTRU, sirdaryoTUZ, sirdaryoTRU, jizzaxTUZ, jizzaxTRU, samarqandTUZ, samarqandTRU,
                    surxandaryoTUZ, surxandaryoTRU, qashqadaryoTUZ, qashqadaryoTRU, navoiyTUZ, navoiyTRU, buxoroTUZ, buxoroTRU, xorazmTUZ,
                    xoramzTRU, qqTUZ, qqTRU);
                await context.SaveChangesAsync();
            }
            #endregion Regions

            #region Roles
            Role superadminRole;
            Role adminRole;
            Role userRole;

            if (!await context.Roles.AnyAsync())
            {
                superadminRole = new Role
                {
                    Name = Roles.SuperAdministrator,
                    ConcurrencyStamp = string.Empty,
                    NormalizedName = Roles.SuperAdministrator.ToUpper()
                };
                adminRole = new Role
                {
                    Name = Roles.Administrator,
                    ConcurrencyStamp = string.Empty,
                    NormalizedName = Roles.Administrator.ToUpper()
                };
                userRole = new Role
                {
                    Name = Roles.User,
                    ConcurrencyStamp = string.Empty,
                    NormalizedName = Roles.User.ToUpper()
                };

                await roleManager.CreateAsync(superadminRole);
                await roleManager.CreateAsync(adminRole);
                await roleManager.CreateAsync(userRole);
            }
            else
            {
                superadminRole = await roleManager.FindByNameAsync(Roles.SuperAdministrator)
                    ?? throw new InvalidOperationException("SuperAdministrator role missing");
            }
            #endregion Roles

            #region Users
            var adminUserName = "martin";
            if (await userManager.FindByNameAsync(adminUserName) == null)
            {
                var james = new User
                {
                    AccessFailedCount = 0,
                    Email = "martin.iden.jack@london.com",
                    EmailConfirmed = true,
                    NormalizedUserName = adminUserName.ToUpper(),
                    NormalizedEmail = "martin.iden.jack@london.com".ToUpper(),
                    PhoneNumberConfirmed = true,
                    PhoneNumber = "",
                    TwoFactorEnabled = false,
                    UserName = adminUserName,
                    DefaultRole = superadminRole,
                    IsActive = true,
                };

                var effectivePassword = string.IsNullOrWhiteSpace(admin_password) || admin_password == "Password"
                    ? "Admin123!"
                    : admin_password;

                var result = await userManager.CreateAsync(james, effectivePassword);
                if (!result.Succeeded)
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    throw new Exception($"SuperAdmin foydalanuvchisini yaratib bo'lmadi: {errors}");
                }

                await userManager.AddToRoleAsync(james, superadminRole.Name!);
            }
            #endregion Users
        }
    }
}
