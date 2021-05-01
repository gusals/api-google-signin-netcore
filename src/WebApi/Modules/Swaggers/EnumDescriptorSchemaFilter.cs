using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Modules
{
    /// <inheritdoc/>
    public sealed class EnumDescriptorSchemaFilter : ISchemaFilter
    {
        /// <inheritdoc/>
        public void Apply(OpenApiSchema openApiSchema, SchemaFilterContext schemaFilterContext)
        {
            var typeInfo = schemaFilterContext.Type.GetTypeInfo();
            if (typeInfo.IsEnum)
                openApiSchema.Description = BuildDescription(typeInfo: typeInfo);
        }

        private static string BuildDescription(TypeInfo typeInfo)
        {
            var docMembers = LoadXmlMembers(typeInfo: typeInfo);
            var stringBuilder = new StringBuilder();
            if (docMembers != null)
            {
                var docMember = XmlDocHelper.GetTypeMember(xElement: docMembers, typeInfo: typeInfo);
                stringBuilder.AppendLine(value: docMember.Value.Trim()).AppendLine();
                BuildMembersDescription(typeInfo: typeInfo, stringBuilder: stringBuilder, docMembers: docMembers);
            }
            return stringBuilder.ToString();
        }

        private static void BuildMembersDescription(TypeInfo typeInfo, StringBuilder stringBuilder, XElement docMembers)
        {
            var enumValues = Enum.GetValues(enumType: typeInfo);

            for (var i = 0; i < enumValues.Length; i++)
            {
                var enumValue = enumValues.GetValue(i);
                if (enumValue != null)
                {
                    var name = enumValue.ToString();
                    var member = typeInfo.GetMember(name: name ?? string.Empty).Single();
                    var docMember = XmlDocHelper.GetDocMember(xElement: docMembers, memberInfo: member);
                    var description = docMember.Value.Trim();
                    stringBuilder.AppendFormat(format: $@"* `{{0}}` - {{1}}", arg0: name, arg1: description).AppendLine();
                }
            }
        }

        private static XElement? LoadXmlMembers(TypeInfo typeInfo)
        {
            var file = XmlDocHelper.GetXmlDocFile(assembly: typeInfo.Assembly);
            var docXml = XDocument.Load(uri: file.FullName);
            var xmlRoot = docXml.Root;
            return xmlRoot != null ? xmlRoot.Element(name: "members") : throw new ArgumentNullException(paramName: $"{nameof(xmlRoot)}, for {typeInfo.FullName}");
        }
    }
}