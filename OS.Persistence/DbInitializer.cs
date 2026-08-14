using OS.Domain;
using OS.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


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
            var uzLang = new Language
            {
                Code = "UZ",
                Flag = "uz_flag",
                IsDefault = true,
                Name = "O'zbekcha"
            };
            var ruLang = new Language
            {
                Code = "RU",
                Flag = "ru_flag",
                IsDefault = false,
                Name = "Русский"
            };

            await context.Languages.AddAsync(uzLang);
            await context.Languages.AddAsync(ruLang);
            await context.SaveChangesAsync();

            #endregion Languages

          
        
         
            #region Regions
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

            #endregion Regions

            //#region ProductCategories
            //var maishiyCategory = new ProductCategory() { IsDeleted = false, CreatedAt = DateTime.UtcNow, IsNew = false };
            //var smartfonCategory = new ProductCategory() { IsDeleted = false, CreatedAt = DateTime.UtcNow, IsNew = false };
            //var televizorCategory = new ProductCategory() { IsDeleted = false, CreatedAt = DateTime.UtcNow, IsNew = false };
            //var muzlatgichCategory = new ProductCategory() { IsDeleted = false, CreatedAt = DateTime.UtcNow, IsNew = false };
            //var tuplamCategory = new ProductCategory() { IsDeleted = false, CreatedAt = DateTime.UtcNow, IsNew = false };
            //await context.ProductCategories.AddRangeAsync(maishiyCategory, smartfonCategory, televizorCategory, muzlatgichCategory, tuplamCategory);
            //await context.SaveChangesAsync();

            //var maishiyTUZ = new ProductCategoryTranslate()
            //{
            //    Name = "Maishiy texnika",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Maishiy texnikalar bo'limi",
            //    ProductCategory = maishiyCategory,
            //    Language = uzLang,
            //};
            //var maishiyTRU = new ProductCategoryTranslate()
            //{
            //    Name = "Бытовая техника",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Отдел бытовой техники",
            //    ProductCategory = maishiyCategory,
            //    Language = ruLang,
            //};

            //var smartfonTUZ = new ProductCategoryTranslate()
            //{
            //    Name = "Smartfon",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Smartfonlar bo'limi",
            //    ProductCategory = smartfonCategory,
            //    Language = uzLang,
            //};
            //var smartfonTRU = new ProductCategoryTranslate()
            //{
            //    Name = "Смартфон",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Раздел «Смартфоны»",
            //    ProductCategory = smartfonCategory,
            //    Language = ruLang,
            //};

            //var televizorTUZ = new ProductCategoryTranslate()
            //{
            //    Name = "Televizor",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Televizorlar bo'limi",
            //    ProductCategory = televizorCategory,
            //    Language = uzLang,
            //};
            //var televizorTRU = new ProductCategoryTranslate()
            //{
            //    Name = "ТВ",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Отдел телевизоров",
            //    ProductCategory = televizorCategory,
            //    Language = ruLang,
            //};
            //var muzlatgichTUZ = new ProductCategoryTranslate()
            //{
            //    Name = "Muzlatgich",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Muzlatgichlar bo'limi",
            //    ProductCategory = muzlatgichCategory,
            //    Language = uzLang,
            //};
            //var muzlatgichTRU = new ProductCategoryTranslate()
            //{
            //    Name = "Холодильник",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Отдел холодильников",
            //    ProductCategory = muzlatgichCategory,
            //    Language = ruLang,
            //};
            //var tuplamlarTUZ = new ProductCategoryTranslate()
            //{
            //    Name = "To'plam",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "To'plamlar bo'limi",
            //    ProductCategory = tuplamCategory,
            //    Language = uzLang,
            //};
            //var tuplamlarTRU = new ProductCategoryTranslate()
            //{
            //    Name = "Коллекция",
            //    CreatedAt = DateTime.UtcNow,
            //    Description = "Раздел Коллекции",
            //    ProductCategory = tuplamCategory,
            //    Language = ruLang,
            //};
            //await context.ProductCategoryTranslates.AddRangeAsync(maishiyTUZ, maishiyTRU, smartfonTUZ, smartfonTRU, televizorTUZ, televizorTRU,
            //muzlatgichTUZ, muzlatgichTRU, tuplamlarTUZ, tuplamlarTRU);
            //await context.SaveChangesAsync();

            //#endregion ProductCategories

          
            #region Roles

            var superadminRole = new Role
            {
                Name = Roles.SuperAdministrator,
                ConcurrencyStamp = string.Empty,
                NormalizedName = Roles.SuperAdministrator.ToUpper()
            };
            var adminRole = new Role
            {
                Name = Roles.Administrator,
                ConcurrencyStamp = string.Empty,
                NormalizedName = Roles.Administrator.ToUpper()
            };
            var userRole = new Role
            {
                Name = Roles.User,
                ConcurrencyStamp = string.Empty,
                NormalizedName = Roles.User.ToUpper()
            };
        

            await context.Roles.AddAsync(superadminRole);
            await context.Roles.AddAsync(adminRole);
            await context.Roles.AddAsync(userRole);

            await context.SaveChangesAsync();
            #endregion Roles

            #region Users

         
            var james = new User
            {
                AccessFailedCount = 0,
                Email = "martin.iden.jack@london.com",
                EmailConfirmed = true,
                NormalizedUserName = "martin".ToUpper(),
                NormalizedEmail = "martin.iden.jack@london.com".ToUpper(),
                PhoneNumberConfirmed = true,
                PhoneNumber = "",
                TwoFactorEnabled = false,
                UserName = "martin",
                DefaultRole = superadminRole,
                IsActive = true,
            };
            var result = await userManager.CreateAsync(james, admin_password ?? "AdminPass123!");
            if (!result.Succeeded)
            {
                james.Age = 18;
            }

            await context.SaveChangesAsync();

            #endregion Users

        

        }
    }
}
