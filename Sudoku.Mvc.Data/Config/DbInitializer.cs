using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sudoku.Mvc.Common.Configuration.Options.Abstract;
using Sudoku.Mvc.Common.Extensions;
using Sudoku.Mvc.Data.Entity;

namespace Sudoku.Mvc.Data.Config
{
    public static class DbInitializer
    {
        public static void Initialize(SudokuDbContext context)
        {
            context.Database.EnsureCreated();

            InitializeDbOptions(context);

            context.SaveChanges();
        }

        private static void InitializeDbOptions(SudokuDbContext context)
        {
            var optionsInMainApp = GetOptions().Invoke(Assembly.GetEntryAssembly()).ToList();
            var optionsInCommon = GetOptions().Invoke(Assembly.GetAssembly(typeof(DbOptions))).ToList();

            optionsInMainApp.AddRange(optionsInCommon);

            var dbProperties = context.AppConfig.Select(x => x.Key).ToList();

            foreach (var option in optionsInMainApp)
            {
                var instance = (DbOptions)Activator.CreateInstance(option);

                var properties = option.GetProperties(BindingFlags.Public | BindingFlags.Instance).Select(x => instance.NameOfSection + ':' + x.Name).ToList();

                var notInitializedProperties = properties.Except(dbProperties).Select(x => x.RemoveAllBeforeChar(':')).ToList();

                if (notInitializedProperties.Count == 0)
                {
                    continue;
                }

                foreach (var property in notInitializedProperties)
                {
                    var prop = option.GetProperty(property);
                    var rawValue = prop.GetValue(instance, null);
                    var val = rawValue == null ? string.Empty : rawValue.ToString();
                    val = prop.PropertyType == typeof(Boolean) ? val.ToLowerInvariant() : val; //True и False не парсятся взад

                    var entity = new AppConfig()
                    {
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = DateTime.UtcNow,
                        Key = $"{instance.NameOfSection}:{property}",
                        Value = val
                    };

                    context.Add(entity);
                }
            }
        }

        private static Func<Assembly, IEnumerable<Type>> GetOptions()
        {
            return assembly => assembly.GetTypes().Where(t => t != typeof(DbOptions) &&
                                                              typeof(DbOptions).IsAssignableFrom(t));
        }
    }
}
