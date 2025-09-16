// Decompiled with JetBrains decompiler
// Type: PX.Data.Maintenance.GI.XMLTablesRestrictor
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using PX.Common;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

#nullable enable
namespace PX.Data.Maintenance.GI;

internal class XMLTablesRestrictor : IPXSchemaTableRestrictor, IDisposable
{
  private readonly 
  #nullable disable
  IFileProvider _fileProvider;
  private readonly ILogger _logger;
  private readonly string _relativeFileName;
  private readonly IDisposable _fileWatcher;
  private XMLTablesRestrictor.TableList _restricted;
  private XMLTablesRestrictor.TableList _allowed;
  private const string _xmlSchema = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n\t\t\t<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\">\r\n\t\t\t\t<xs:simpleType name=\"string1\">\r\n\t\t\t\t\t<xs:restriction base=\"xs:string\">\r\n\t\t\t\t\t\t<xs:minLength value=\"1\" />\r\n\t\t\t\t\t</xs:restriction>\r\n\t\t\t\t</xs:simpleType>\r\n\t\t\t\t<xs:element name=\"GITables\">\r\n\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t<xs:element name=\"Hidden\" minOccurs=\"0\" maxOccurs=\"1\">\r\n\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t\t\t\t<xs:element name=\"Table\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n\t\t\t\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:extension base=\"xs:string\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:attribute type=\"string1\" name=\"FullName\" use=\"required\" />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</xs:extension>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t<xs:element name=\"Allowed\" minOccurs=\"0\" maxOccurs=\"1\">\r\n\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t\t\t\t<xs:element name=\"Table\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n\t\t\t\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:extension base=\"xs:string\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:attribute type=\"string1\" name=\"FullName\" use=\"required\" />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</xs:extension>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t</xs:element>\r\n\t\t\t</xs:schema>\r\n\t\t";

  public XMLTablesRestrictor(IFileProvider fileProvider, ILogger logger, string relativeFileName)
  {
    this._fileProvider = fileProvider ?? throw new ArgumentNullException(nameof (fileProvider));
    this._logger = logger ?? throw new ArgumentNullException(nameof (logger));
    this._relativeFileName = !string.IsNullOrEmpty(relativeFileName) ? relativeFileName : throw new PXArgumentException(nameof (relativeFileName), "The name of the XML file for GI table configuration cannot be empty.");
    this.Reload();
    this._fileWatcher = ChangeToken.OnChange((Func<IChangeToken>) (() => fileProvider.Watch(relativeFileName)), new System.Action(this.Reload));
  }

  private void Reload()
  {
    this._allowed = (XMLTablesRestrictor.TableList) null;
    this._restricted = (XMLTablesRestrictor.TableList) null;
    IFileInfo fileInfo = this._fileProvider.GetFileInfo(this._relativeFileName);
    if (!fileInfo.Exists)
      return;
    try
    {
      using (Stream readStream = fileInfo.CreateReadStream())
      {
        XDocument xml = XDocument.Load(readStream);
        this.ValidateXmlSchema(xml);
        this._restricted = this.ReadSection(xml.Root, "Hidden");
        this._allowed = this.ReadSection(xml.Root, "Allowed");
      }
    }
    catch (FileNotFoundException ex)
    {
    }
    catch (Exception ex)
    {
      this._logger.Error<string>(ex, "The XML file {FileName} for GI table configuration has an invalid format. Please check the file and try again.", this._relativeFileName);
    }
  }

  private void ValidateXmlSchema(XDocument xml)
  {
    XmlSchemaSet schemas = new XmlSchemaSet();
    schemas.Add("", XmlReader.Create((TextReader) new StringReader("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n\t\t\t<xs:schema xmlns:xs=\"http://www.w3.org/2001/XMLSchema\" attributeFormDefault=\"unqualified\" elementFormDefault=\"qualified\">\r\n\t\t\t\t<xs:simpleType name=\"string1\">\r\n\t\t\t\t\t<xs:restriction base=\"xs:string\">\r\n\t\t\t\t\t\t<xs:minLength value=\"1\" />\r\n\t\t\t\t\t</xs:restriction>\r\n\t\t\t\t</xs:simpleType>\r\n\t\t\t\t<xs:element name=\"GITables\">\r\n\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t<xs:element name=\"Hidden\" minOccurs=\"0\" maxOccurs=\"1\">\r\n\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t\t\t\t<xs:element name=\"Table\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n\t\t\t\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:extension base=\"xs:string\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:attribute type=\"string1\" name=\"FullName\" use=\"required\" />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</xs:extension>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t<xs:element name=\"Allowed\" minOccurs=\"0\" maxOccurs=\"1\">\r\n\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t<xs:sequence>\r\n\t\t\t\t\t\t\t\t\t\t<xs:element name=\"Table\" minOccurs=\"0\" maxOccurs=\"unbounded\">\r\n\t\t\t\t\t\t\t\t\t\t\t<xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t\t\t<xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:extension base=\"xs:string\">\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t<xs:attribute type=\"string1\" name=\"FullName\" use=\"required\" />\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t</xs:extension>\r\n\t\t\t\t\t\t\t\t\t\t\t\t</xs:simpleContent>\r\n\t\t\t\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t\t\t\t</xs:element>\r\n\t\t\t\t\t\t</xs:sequence>\r\n\t\t\t\t\t</xs:complexType>\r\n\t\t\t\t</xs:element>\r\n\t\t\t</xs:schema>\r\n\t\t")));
    string errorMessage = (string) null;
    Exception exception = (Exception) null;
    xml.Validate(schemas, (ValidationEventHandler) ((sender, args) =>
    {
      errorMessage = args.Message;
      exception = (Exception) args.Exception;
    }));
    if (exception != null)
      throw exception;
    if (errorMessage != null)
      throw new PXException(errorMessage);
  }

  private XMLTablesRestrictor.TableList ReadSection(XElement parent, string sectionName)
  {
    XMLTablesRestrictor.TableList tableList = new XMLTablesRestrictor.TableList();
    XElement xelement = parent.Element((XName) sectionName);
    if (xelement != null)
    {
      foreach (XElement element in xelement.Elements((XName) "Table"))
      {
        string str = element.Attribute((XName) "FullName").With<XAttribute, string>((Func<XAttribute, string>) (_ => _.Value));
        if (!string.IsNullOrEmpty(str))
          tableList.Add(str);
      }
    }
    return tableList;
  }

  public bool IsAllowed(System.Type table)
  {
    if (table == (System.Type) null)
      throw new ArgumentNullException(nameof (table));
    if (this._allowed == null || this._restricted == null)
      return true;
    string fullName = table.FullName;
    int matchLevel1 = this._allowed.GetMatchLevel(fullName);
    int matchLevel2 = this._restricted.GetMatchLevel(fullName);
    if (matchLevel1 < 0)
      return false;
    return matchLevel2 < 0 || matchLevel1 > matchLevel2;
  }

  public void Dispose() => this._fileWatcher.Dispose();

  /// <summary>
  /// Represents a wildcard running on the
  /// <see cref="N:System.Text.RegularExpressions" /> engine.
  /// </summary>
  private class Wildcard : Regex
  {
    private const string WildcardMulti = "*";
    private const string WildcardSingle = "?";
    private readonly string _pattern;

    public string Pattern => this._pattern;

    /// <summary>Initializes a wildcard with the given search pattern.</summary>
    /// <param name="pattern">The wildcard pattern to match.</param>
    public Wildcard(string pattern)
      : base(XMLTablesRestrictor.Wildcard.WildcardToRegex(pattern))
    {
      this._pattern = pattern;
    }

    /// <summary>
    /// Initializes a wildcard with the given search pattern and options.
    /// </summary>
    /// <param name="pattern">The wildcard pattern to match.</param>
    /// <param name="options">A combination of one or more
    /// <see cref="T:System.Text.RegularExpressions.RegexOptions" />.</param>
    public Wildcard(string pattern, RegexOptions options)
      : base(XMLTablesRestrictor.Wildcard.WildcardToRegex(pattern), options)
    {
      this._pattern = pattern;
    }

    public static bool HasWildcards(string str) => str.Contains("*") || str.Contains("?");

    /// <summary>Converts a wildcard to a regex.</summary>
    /// <param name="pattern">The wildcard pattern to convert.</param>
    /// <returns>A regex equivalent of the given wildcard.</returns>
    private static string WildcardToRegex(string pattern)
    {
      return $"^{Regex.Escape(pattern).Replace("\\*", ".*").Replace("\\?", ".")}$";
    }
  }

  private class TableList
  {
    private const char Delimiter = '.';
    private readonly HashSet<string> _usual = new HashSet<string>();
    private readonly List<XMLTablesRestrictor.Wildcard> _wildcarded = new List<XMLTablesRestrictor.Wildcard>();

    public TableList()
    {
    }

    public TableList(IEnumerable<string> collection)
    {
      foreach (string str in collection)
        this.Add(str);
    }

    public void Add(string value)
    {
      if (XMLTablesRestrictor.Wildcard.HasWildcards(value))
        this._wildcarded.Add(new XMLTablesRestrictor.Wildcard(value, RegexOptions.Compiled));
      else
        this._usual.Add(value);
    }

    public bool Contains(string value)
    {
      return this._usual.Contains(value) || this._wildcarded.Any<XMLTablesRestrictor.Wildcard>((Func<XMLTablesRestrictor.Wildcard, bool>) (wildcarded => wildcarded.IsMatch(value)));
    }

    /// <summary>
    /// Returns relative measure that determines specificity level of match.
    /// </summary>
    /// <remarks>
    /// Example of return values: PX.SM.Users = 3, PX.SM.* = 2, PX.* = 1, * = 0
    /// </remarks>
    public int GetMatchLevel(string value)
    {
      if (this._usual.Contains(value))
        return value.Count<char>((Func<char, bool>) (c => c == '.')) + 1;
      string[] array = this._wildcarded.Where<XMLTablesRestrictor.Wildcard>((Func<XMLTablesRestrictor.Wildcard, bool>) (w => w.IsMatch(value))).Select<XMLTablesRestrictor.Wildcard, string>((Func<XMLTablesRestrictor.Wildcard, string>) (w => w.Pattern)).ToArray<string>();
      return array.Length != 0 ? ((IEnumerable<string>) array).Max<string>((Func<string, int>) (p => p.Count<char>((Func<char, bool>) (c => c == '.')))) : -1;
    }

    public int Count => this._usual.Count + this._wildcarded.Count;
  }
}
