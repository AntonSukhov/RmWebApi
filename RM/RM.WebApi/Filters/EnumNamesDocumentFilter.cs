using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Nodes;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RM.WebApi.Filters
{
    /// <summary>
    /// Добавляет расширение 'x-enumNames' в схемы перечислений.
    /// <para>
    /// По умолчанию Swashbuckle сериализует enum только числовыми значениями
    /// ("enum": [0, 1]), из-за чего NSwag генерирует члены с именами '_0', '_1'.
    /// 'x-enumNames' передаёт имена значений (Male, Female), и NSwag использует их
    /// при генерации клиента.
    /// </para>
    /// <remarks>
    /// В Swashbuckle 10 (генератор схем на Microsoft.OpenApi 2.x) schema-фильтры
    /// не вызываются для enum-типов, поэтому используется document-фильтр:
    /// схемы сопоставляются с CLR-перечислениями по имени схемы.
    /// </remarks>
    /// </summary>
    public class EnumNamesDocumentFilter : IDocumentFilter
    {
        private const string EnumNamesExtensionKey = "x-enumNames";

        /// <inheritdoc />
        public void Apply(OpenApiDocument document, DocumentFilterContext context)
        {
            ArgumentNullException.ThrowIfNull(document);
            ArgumentNullException.ThrowIfNull(context);

            var enumTypesByName = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(GetAssemblyTypes)
                .Where(type => type.IsEnum)
                .GroupBy(type => type.Name)
                .ToDictionary(group => group.Key, group => group.First(), StringComparer.Ordinal);

            if (document.Components?.Schemas is not { Count: > 0 } schemas)
            {
                return;
            }

            foreach (var (schemaName, schema) in schemas)
            {
                if (schema.Enum is not { Count: > 0 } enumValues)
                {
                    continue;
                }

                if (!enumTypesByName.TryGetValue(schemaName, out var enumType))
                {
                    continue;
                }

                var names = new JsonArray();

                foreach (var value in enumValues)
                {
                    if (value is JsonValue jsonValue && jsonValue.TryGetValue<int>(out var intValue))
                    {
                        names.Add(Enum.GetName(enumType, intValue) ?? intValue.ToString());
                    }
                    else
                    {
                        names.Add(value?.ToString());
                    }
                }

                if (schema is not OpenApiSchema concreteSchema)
                {
                    continue;
                }

                concreteSchema.Extensions ??= new Dictionary<string, IOpenApiExtension>();
                concreteSchema.Extensions[EnumNamesExtensionKey] = new JsonNodeExtension(names);
            }
        }

        private static IEnumerable<Type> GetAssemblyTypes(System.Reflection.Assembly assembly)
        {
            try
            {
                return assembly.GetTypes();
            }
            catch (Exception)
            {
                // Некоторые сборки могут быть не полностью загружены - пропускаем их.
                return Type.EmptyTypes;
            }
        }
    }
}
