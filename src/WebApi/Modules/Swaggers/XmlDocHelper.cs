using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace WebApi.Modules
{
    /// <summary>
    /// XmlDocHelper.
    /// </summary>
    public static class XmlDocHelper
    {
        /// <summary>
        /// Get the xml element representing the member.
        /// </summary>
        /// <param name="xElement">Represents an XML element.  See XElement Class Overview and the Remarks section on this page for usage information and examples.</param>
        /// <param name="memberInfo">Obtains information about the attributes of a member and provides access to member metadata.</param>
        /// <returns><see cref="XElement"/></returns>
        public static XElement GetDocMember(XElement xElement, MemberInfo memberInfo) =>
            xElement.Elements(name: "member").First(xElement => xElement.Attribute(name: "name")?.Value == GetMemberId(memberInfo: memberInfo));

        /// <summary>
        /// Get the xml element representing the member.
        /// </summary>
        /// <param name="xElement">Represents an XML element.  See XElement Class Overview and the Remarks section on this page for usage information and examples.</param>
        /// <param name="typeInfo">Represents type declarations for class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</param>
        /// <returns><see cref="XElement"/></returns>
        public static XElement GetTypeMember(XElement xElement, TypeInfo typeInfo) =>
            xElement.Elements("member").First(xElement => xElement.Attribute(name: "name")?.Value == GetMemberId(memberInfo: typeInfo));

        /// <summary>
        /// Find the XML documentation files for a given assembly.
        /// </summary>
        /// <param name="assembly">Represents an assembly, which is a reusable, versionable, and self-describing building block of a common language runtime application.</param>
        /// <param name="cultureInfo">Provides information about a specific culture (called a locale for unmanaged code development). The information includes the names for the culture, the writing system, the calendar used, the sort order of strings, and formatting for dates and numbers.</param>
        /// <returns><see cref="FileInfo"/></returns>
        public static FileInfo GetXmlDocFile(Assembly assembly, CultureInfo? cultureInfo = null) =>
            EnumeratePossibleXmlDocumentationLocation(assembly: assembly, cultureInfo: cultureInfo ?? CultureInfo.CurrentCulture)
                    .Select(selector: directory => Path.Combine(path1: directory, path2: $"{Path.GetFileNameWithoutExtension(path: assembly.Location)}.xml"))
                    .Select(selector: filePath => new FileInfo(fileName: filePath))
                    .FirstOrDefault(predicate: fileInfo => fileInfo.Exists)
                ?? throw new ArgumentException(message: $"No XML doc file found for assembly '{assembly.FullName}'");

        private static IEnumerable<string> EnumeratePossibleXmlDocumentationLocation(Assembly assembly, CultureInfo cultureInfo)
        {
            var locations = new[] { new FileInfo(fileName: assembly.Location)?.Directory?.FullName, AppContext.BaseDirectory };

            if (locations != null)
                foreach (var location in locations)
                    if (location != null)
                    {
                        while (cultureInfo.Name != CultureInfo.InvariantCulture.Name)
                        {
                            yield return Path.Combine(path1: location, path2: cultureInfo.Name);
                            cultureInfo = cultureInfo.Parent;
                        }
                        yield return Path.Combine(path1: location, path2: string.Empty);
                    }
        }

        private static string GetMemberFullName(MemberInfo memberInfo)
        {
            var memberScope = string.Empty;
            if (memberInfo.DeclaringType != null)
                memberScope = GetMemberFullName(memberInfo: memberInfo.DeclaringType);
            else if (memberInfo is Type type)
                memberScope = type.Namespace;
            return $"{memberScope}.{memberInfo.Name}";
        }

        private static string GetMemberId(MemberInfo memberInfo) => $"{GetMemberPrefix(memberInfo: memberInfo)}:{GetMemberFullName(memberInfo: memberInfo)}";

        private static char GetMemberPrefix(MemberInfo memberInfo)
        {
            var typeName = memberInfo.GetType().Name;
            return typeName switch
            {
                "MdFieldInfo" => 'F',
                _ => typeName.Replace(oldValue: "Runtime", newValue: string.Empty)[index: 0],
            };
        }
    }
}