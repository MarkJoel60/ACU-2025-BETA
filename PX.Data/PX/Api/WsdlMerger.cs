// Decompiled with JetBrains decompiler
// Type: PX.Api.WsdlMerger
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Soap.Screen;
using PX.Data;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace PX.Api;

public class WsdlMerger
{
  private StringBuilder bld;
  private Dictionary<string, bool[]> processed = new Dictionary<string, bool[]>();
  private int checkpoint;

  public void Start(bool includeUntyped)
  {
    this.processed = new Dictionary<string, bool[]>();
    this.bld = new StringBuilder();
    this.checkpoint = 0;
    this.bld.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
    this.bld.AppendLine("<wsdl:definitions xmlns:soap=\"http://schemas.xmlsoap.org/wsdl/soap/\" xmlns:tm=\"http://microsoft.com/wsdl/mime/textMatching/\" xmlns:soapenc=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:mime=\"http://schemas.xmlsoap.org/wsdl/mime/\" xmlns:tns=\"http://www.acumatica.com/generic/\" xmlns:s=\"http://www.w3.org/2001/XMLSchema\" xmlns:http=\"http://schemas.xmlsoap.org/wsdl/http/\" targetNamespace=\"http://www.acumatica.com/generic/\" xmlns:wsdl=\"http://schemas.xmlsoap.org/wsdl/\">");
    this.bld.AppendLine("  <wsdl:types>");
    this.bld.AppendLine("    <s:schema elementFormDefault=\"qualified\" targetNamespace=\"http://www.acumatica.com/generic/\">");
    this.bld.AppendLine("      <s:complexType name=\"ProcessResult\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Status\" type=\"tns:ProcessStatus\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Seconds\" type=\"s:int\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Message\" type=\"s:string\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:simpleType name=\"ProcessStatus\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"NotExists\" />");
    this.bld.AppendLine("          <s:enumeration value=\"InProcess\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Completed\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Aborted\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:element name=\"GetScenario\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"scenario\" type=\"s:string\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"GetScenarioResponse\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetScenarioResult\" type=\"tns:ArrayOfCommand\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:complexType name=\"ArrayOfCommand\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Command\" nillable=\"true\" type=\"tns:Command\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Command\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"FieldName\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ObjectName\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Value\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"false\" name=\"Commit\" type=\"s:boolean\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"false\" name=\"IgnoreError\" type=\"s:boolean\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"LinkedCommand\" type=\"tns:Command\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Descriptor\" type=\"tns:ElementDescriptor\" />");
    if (includeUntyped)
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Name\" type=\"s:string\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"ElementDescriptor\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"DisplayName\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"false\" name=\"IsDisabled\" type=\"s:boolean\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"false\" name=\"IsRequired\" type=\"s:boolean\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"String\" name=\"ElementType\" type=\"tns:ElementTypes\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"0\" name=\"LengthLimit\" type=\"s:int\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"InputMask\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"DisplayRules\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"AllowedValues\" type=\"tns:ArrayOfString\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:simpleType name=\"ElementTypes\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"String\" />");
    this.bld.AppendLine("          <s:enumeration value=\"AsciiString\" />");
    this.bld.AppendLine("          <s:enumeration value=\"StringSelector\" />");
    this.bld.AppendLine("          <s:enumeration value=\"ExplicitSelector\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Number\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Option\" />");
    this.bld.AppendLine("          <s:enumeration value=\"WideOption\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Calendar\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Action\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:simpleType name=\"SchemaMode\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"Basic\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Detailed\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:complexType name=\"ArrayOfString\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"string\" nillable=\"true\" type=\"s:string\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"EveryValue\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Key\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Action\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Field\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Value\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Field\" >");
    this.bld.AppendLine("            <s:sequence>");
    this.bld.AppendLine("              <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Message\" type=\"s:string\" />");
    this.bld.AppendLine("              <s:element minOccurs=\"0\" maxOccurs=\"1\" default=\"false\" name=\"IsError\" type=\"s:boolean\" />");
    this.bld.AppendLine("            </s:sequence>");
    this.bld.AppendLine("          </s:extension>");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Answer\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"RowNumber\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"NewRow\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"DeleteRow\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Parameter\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Command\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Attachment\">");
    this.bld.AppendLine("        <s:complexContent mixed=\"false\">");
    this.bld.AppendLine("          <s:extension base=\"tns:Field\" />");
    this.bld.AppendLine("        </s:complexContent>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"ArrayOfFilter\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Filter\" nillable=\"true\" type=\"tns:Filter\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:complexType name=\"Filter\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Field\" type=\"tns:Field\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Condition\" type=\"tns:FilterCondition\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Value\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Value2\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"OpenBrackets\" type=\"s:int\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"CloseBrackets\" type=\"s:int\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Operator\" type=\"tns:FilterOperator\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:simpleType name=\"FilterCondition\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"Equals\" />");
    this.bld.AppendLine("          <s:enumeration value=\"NotEqual\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Greater\" />");
    this.bld.AppendLine("          <s:enumeration value=\"GreaterOrEqual\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Less\" />");
    this.bld.AppendLine("          <s:enumeration value=\"LessOrEqual\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Contain\" />");
    this.bld.AppendLine("          <s:enumeration value=\"StartsWith\" />");
    this.bld.AppendLine("          <s:enumeration value=\"EndsWith\" />");
    this.bld.AppendLine("          <s:enumeration value=\"NotContain\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Between\" />");
    this.bld.AppendLine("          <s:enumeration value=\"IsNull\" />");
    this.bld.AppendLine("          <s:enumeration value=\"IsNotNull\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:simpleType name=\"FilterOperator\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"And\" />");
    this.bld.AppendLine("          <s:enumeration value=\"Or\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:complexType name=\"ArrayOfArrayOfString\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"ArrayOfString\" nillable=\"true\" type=\"tns:ArrayOfString\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:element name=\"Login\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"name\" type=\"s:string\" />");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"password\" type=\"s:string\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:complexType name=\"LoginResult\">");
    this.bld.AppendLine("        <s:sequence>");
    this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Code\" type=\"tns:ErrorCode\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Message\" type=\"s:string\" />");
    this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Session\" type=\"s:string\" />");
    this.bld.AppendLine("        </s:sequence>");
    this.bld.AppendLine("      </s:complexType>");
    this.bld.AppendLine("      <s:simpleType name=\"ErrorCode\">");
    this.bld.AppendLine("        <s:restriction base=\"s:string\">");
    this.bld.AppendLine("          <s:enumeration value=\"OK\" />");
    this.bld.AppendLine("          <s:enumeration value=\"INVALID_CREDENTIALS\" />");
    this.bld.AppendLine("          <s:enumeration value=\"INTERNAL_ERROR\" />");
    this.bld.AppendLine("          <s:enumeration value=\"INVALID_API_VERSION\" />");
    this.bld.AppendLine("        </s:restriction>");
    this.bld.AppendLine("      </s:simpleType>");
    this.bld.AppendLine("      <s:element name=\"LoginResponse\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"LoginResult\" type=\"tns:LoginResult\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name = \"Logout\">");
    this.bld.AppendLine("        <s:complexType/>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name = \"LogoutResponse\">");
    this.bld.AppendLine("        <s:complexType />");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetBusinessDate\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"date\" type=\"s:dateTime\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetBusinessDateResponse\">");
    this.bld.AppendLine("        <s:complexType />");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetLocaleName\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"localeName\" type=\"s:string\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetLocaleNameResponse\">");
    this.bld.AppendLine("        <s:complexType />");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetSchemaMode\">");
    this.bld.AppendLine("        <s:complexType>");
    this.bld.AppendLine("          <s:sequence>");
    this.bld.AppendLine("      \t      <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"mode\" type=\"tns:SchemaMode\" />");
    this.bld.AppendLine("          </s:sequence>");
    this.bld.AppendLine("        </s:complexType>");
    this.bld.AppendLine("      </s:element>");
    this.bld.AppendLine("      <s:element name=\"SetSchemaModeResponse\">");
    this.bld.AppendLine("        <s:complexType />");
    this.bld.AppendLine("      </s:element>");
  }

  public void Append(string screenID, bool isExport, bool isImport, bool isSubmit)
  {
    this.checkpoint = this.bld.Length;
    if (this.processed.ContainsKey(screenID))
      throw new PXException("The screen is being merged more than once. Please check that there are no duplicate screen IDs among the items you're trying to merge.");
    bool flag = false;
    string wsdl = WsdlBuilder.GetWsdl(screenID);
    this.bld.Append("      <s:complexType name=\"");
    this.bld.Append(screenID);
    this.bld.Append("Content\">");
    int num1 = wsdl.IndexOf("<s:complexType name=\"Content\">");
    int num2 = wsdl.IndexOf("</s:complexType>", num1 + 30);
    int startIndex1 = num1 + 30;
    int startIndex2 = startIndex1;
    List<string> stringList = new List<string>();
    int num3;
    while ((num3 = wsdl.IndexOf(" type=\"tns:", startIndex1, num2 - startIndex1)) != -1)
    {
      startIndex1 = num3 + 11;
      stringList.Add(wsdl.Substring(startIndex1, wsdl.IndexOf('"', startIndex1) - startIndex1));
      if (stringList.Count > 1)
        stringList.Add(stringList[stringList.Count - 1] + "ServiceCommands");
      this.bld.Append(wsdl, startIndex2, startIndex1 - startIndex2);
      this.bld.Append(screenID);
      startIndex2 = startIndex1;
    }
    this.bld.Append(wsdl, startIndex2, num2 - startIndex2 + 16 /*0x10*/);
    this.bld.AppendLine();
    for (int index = 0; index < stringList.Count; ++index)
    {
      int startIndex3 = wsdl.IndexOf($"<s:complexType name=\"{stringList[index]}\">") + 21;
      int num4 = wsdl.IndexOf("</s:complexType>", startIndex3);
      this.bld.Append("      <s:complexType name=\"");
      this.bld.Append(screenID);
      if (index % 2 == 1)
      {
        int startIndex4 = wsdl.IndexOf($" type=\"tns:{stringList[index + 1]}\"") + 11;
        this.bld.Append(wsdl, startIndex3, startIndex4 - startIndex3);
        this.bld.Append(screenID);
        this.bld.Append(wsdl, startIndex4, num4 - startIndex4 + 16 /*0x10*/);
        this.bld.AppendLine();
      }
      else
      {
        this.bld.Append(wsdl, startIndex3, num4 - startIndex3 + 16 /*0x10*/);
        this.bld.AppendLine();
      }
    }
    int startIndex5 = wsdl.IndexOf("<s:complexType name=\"PrimaryKey\">") + 33;
    if (startIndex5 >= 33)
    {
      flag = true;
      this.bld.Append("      <s:complexType name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("PrimaryKey\">");
      int num5 = wsdl.IndexOf("</s:complexType>", startIndex5);
      this.bld.Append(wsdl, startIndex5, num5 - startIndex5 + 16 /*0x10*/);
      this.bld.AppendLine();
    }
    if (isSubmit)
    {
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("Clear\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ClearResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("GetProcessStatus\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("GetProcessStatusResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetProcessStatusResult\" type=\"tns:ProcessResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    if (isExport | isImport | isSubmit)
    {
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("GetSchema\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("GetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.Append("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetSchemaResult\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("Content\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("SetSchema\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.Append("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"schema\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("Content\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("SetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
    }
    if (isExport)
    {
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("Export\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"topCount\" type=\"s:int\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includeHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ExportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ExportResult\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    if (isImport)
    {
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("Import\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"data\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includedHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnIncorrectTarget\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ImportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.Append("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ImportResult\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("ArrayOfImportResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:complexType name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ImportResult\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Processed\" type=\"s:boolean\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Error\" type=\"s:string\" />");
      if (flag)
      {
        this.bld.Append("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Keys\" type=\"tns:");
        this.bld.Append(screenID);
        this.bld.AppendLine("PrimaryKey\" />");
      }
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.Append("      <s:complexType name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ArrayOfImportResult\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.Append("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"ImportResult\" nillable=\"true\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("ImportResult\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
    }
    if (isSubmit)
    {
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("Submit\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.Append("      <s:complexType name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("ArrayOfContent\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.Append("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Content\" nillable=\"true\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("Content\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.Append("      <s:element name=\"");
      this.bld.Append(screenID);
      this.bld.AppendLine("SubmitResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.Append("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"SubmitResult\" type=\"tns:");
      this.bld.Append(screenID);
      this.bld.AppendLine("ArrayOfContent\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    this.processed[screenID] = new bool[3]
    {
      isExport,
      isImport,
      isSubmit
    };
  }

  public void Rollback()
  {
    if (this.checkpoint >= this.bld.Length)
      return;
    this.bld.Remove(this.checkpoint, this.bld.Length - this.checkpoint);
  }

  public string Finish(string serviceID, bool includeUntyped)
  {
    bool flag1 = true;
    bool flag2 = true;
    bool flag3 = true;
    bool flag4 = true;
    foreach (KeyValuePair<string, bool[]> keyValuePair in this.processed)
    {
      flag1 = flag1 && keyValuePair.Value[0];
      flag2 = flag2 && keyValuePair.Value[1];
      flag3 = flag3 && keyValuePair.Value[2];
      flag4 = flag4 && (keyValuePair.Value[0] || keyValuePair.Value[1] || keyValuePair.Value[2]);
    }
    if (!flag1 || !flag2 || !flag3 || !flag4)
    {
      this.bld.AppendLine("      <s:simpleType name=\"ScreenID\">");
      this.bld.AppendLine("        <s:restriction base=\"s:string\">");
      foreach (string key in this.processed.Keys)
      {
        this.bld.Append("          <s:enumeration value=\"");
        this.bld.Append(key);
        this.bld.AppendLine("\" />");
      }
      this.bld.AppendLine("        </s:restriction>");
      this.bld.AppendLine("      </s:simpleType>");
    }
    if (!flag3)
    {
      this.bld.AppendLine("      <s:element name=\"Clear\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"ClearResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"GetProcessStatus\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"GetProcessStatusResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetProcessStatusResult\" type=\"tns:ProcessResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    if (!flag4)
    {
      this.bld.AppendLine("      <s:element name=\"GetSchema\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"GetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetSchemaResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"SetSchema\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"schema\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"SetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
    }
    if (!flag1)
    {
      this.bld.AppendLine("      <s:element name=\"Export\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"topCount\" type=\"s:int\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includeHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"ExportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ExportResult\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    if (!flag2)
    {
      this.bld.AppendLine("      <s:element name=\"Import\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"data\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includedHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnIncorrectTarget\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"ImportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ImportResult\" type=\"tns:ArrayOfImportResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:complexType name=\"ArrayOfImportResult\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"ImportResult\" nillable=\"true\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
    }
    if (!flag3)
    {
      this.bld.AppendLine("      <s:element name=\"Submit\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"screenID\" type=\"tns:ScreenID\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:complexType name=\"ArrayOfContent\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Content\" nillable=\"true\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:element name=\"SubmitResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"SubmitResult\" type=\"tns:ArrayOfContent\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
    }
    if (includeUntyped)
    {
      this.bld.AppendLine("      <s:element name=\"UntypedClear\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedClearResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedGetProcessStatus\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedGetProcessStatusResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"GetProcessStatusResult\" type=\"tns:ProcessResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedGetSchema\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedGetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"GetSchemaResult\" type=\"tns:UntypedContent\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedSetSchema\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"schema\" type=\"s:UntypedContent\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedSetSchemaResponse\">");
      this.bld.AppendLine("        <s:complexType />");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedArrayOfAction\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Action\" nillable=\"true\" type=\"tns:Action\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedContent\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Actions\" type=\"tns:UntypedArrayOfAction\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Containers\" type=\"tns:UntypedArrayOfContainer\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedArrayOfContainer\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Container\" nillable=\"true\" type=\"tns:UntypedContainer\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedContainer\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Fields\" type=\"tns:UntypedArrayOfField\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Name\" type=\"s:string\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ServiceCommands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedArrayOfField\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Field\" nillable=\"true\" type=\"tns:Field\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:element name=\"UntypedSubmit\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedArrayOfContent\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Content\" nillable=\"true\" type=\"tns:UntypedContent\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:element name=\"UntypedSubmitResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"SubmitResult\" type=\"tns:UntypedArrayOfContent\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedExport\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"topCount\" type=\"s:int\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includeHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedExportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ExportResult\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedImport\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"screenID\" type=\"s:string\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"commands\" type=\"tns:ArrayOfCommand\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"filters\" type=\"tns:ArrayOfFilter\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"data\" type=\"tns:ArrayOfArrayOfString\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"includedHeaders\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnError\" type=\"s:boolean\" />");
      this.bld.AppendLine("            <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"breakOnIncorrectTarget\" type=\"s:boolean\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:element name=\"UntypedImportResponse\">");
      this.bld.AppendLine("        <s:complexType>");
      this.bld.AppendLine("          <s:sequence>");
      this.bld.AppendLine("            <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"ImportResult\" type=\"tns:UntypedArrayOfImportResult\" />");
      this.bld.AppendLine("          </s:sequence>");
      this.bld.AppendLine("        </s:complexType>");
      this.bld.AppendLine("      </s:element>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedImportResult\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"1\" maxOccurs=\"1\" name=\"Processed\" type=\"s:boolean\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Error\" type=\"s:string\" />");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"1\" name=\"Keys\" type=\"tns:ArrayOfValue\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"ArrayOfValue\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"Value\" nillable=\"true\" type=\"tns:Value\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
      this.bld.AppendLine("      <s:complexType name=\"UntypedArrayOfImportResult\">");
      this.bld.AppendLine("        <s:sequence>");
      this.bld.AppendLine("          <s:element minOccurs=\"0\" maxOccurs=\"unbounded\" name=\"ImportResult\" nillable=\"true\" type=\"tns:UntypedImportResult\" />");
      this.bld.AppendLine("        </s:sequence>");
      this.bld.AppendLine("      </s:complexType>");
    }
    this.bld.AppendLine("    </s:schema>");
    this.bld.AppendLine("  </wsdl:types>");
    if (!flag3)
    {
      this.bld.AppendLine("  <wsdl:message name=\"ClearSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:Clear\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"ClearSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:ClearResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"GetProcessStatusSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetProcessStatus\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"GetProcessStatusSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetProcessStatusResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    if (!flag4)
    {
      this.bld.AppendLine("  <wsdl:message name=\"GetSchemaSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetSchema\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"GetSchemaSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetSchemaResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"SetSchemaSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetSchema\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"SetSchemaSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetSchemaResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    if (!flag1)
    {
      this.bld.AppendLine("  <wsdl:message name=\"ExportSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:Export\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"ExportSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:ExportResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    if (!flag2)
    {
      this.bld.AppendLine("  <wsdl:message name=\"ImportSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:Import\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"ImportSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:ImportResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    if (!flag3)
    {
      this.bld.AppendLine("  <wsdl:message name=\"SubmitSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:Submit\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"SubmitSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SubmitResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    if (includeUntyped)
    {
      this.bld.AppendLine("  <wsdl:message name=\"UntypedClearSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedClear\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedClearSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedClearResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedGetProcessStatusSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedGetProcessStatus\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedGetProcessStatusSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedGetProcessStatusResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedGetSchemaSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedGetSchema\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedGetSchemaSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedGetSchemaResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedSetSchemaSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedSetSchema\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedSetSchemaSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedSetSchemaResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedSubmitSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedSubmit\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedSubmitSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedSubmitResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedExportSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedExport\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedExportSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedExportResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedImportSoapIn\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedImport\" />");
      this.bld.AppendLine("  </wsdl:message>");
      this.bld.AppendLine("  <wsdl:message name=\"UntypedImportSoapOut\">");
      this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:UntypedImportResponse\" />");
      this.bld.AppendLine("  </wsdl:message>");
    }
    this.bld.AppendLine("  <wsdl:message name=\"GetScenarioSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetScenario\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"GetScenarioSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:GetScenarioResponse\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"LoginSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:Login\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"LoginSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:LoginResponse\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name =\"LogoutSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name =\"parameters\" element =\"tns:Logout\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name =\"LogoutSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name =\"parameters\" element =\"tns:LogoutResponse\"/>");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetBusinessDateSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetBusinessDate\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetBusinessDateSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetBusinessDateResponse\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetLocaleNameSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetLocaleName\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetLocaleNameSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetLocaleNameResponse\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetSchemaModeSoapIn\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetSchemaMode\" />");
    this.bld.AppendLine("  </wsdl:message>");
    this.bld.AppendLine("  <wsdl:message name=\"SetSchemaModeSoapOut\">");
    this.bld.AppendLine("    <wsdl:part name=\"parameters\" element=\"tns:SetSchemaModeResponse\" />");
    this.bld.AppendLine("  </wsdl:message>");
    foreach (KeyValuePair<string, bool[]> keyValuePair in this.processed)
    {
      string key = keyValuePair.Key;
      int num = keyValuePair.Value[0] ? 1 : 0;
      bool flag5 = keyValuePair.Value[1];
      bool flag6 = keyValuePair.Value[2];
      if (flag6)
      {
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ClearSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("Clear\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ClearSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ClearResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatusSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatus\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatusSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatusResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
      }
      if ((num | (flag5 ? 1 : 0) | (flag6 ? 1 : 0)) != 0)
      {
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchemaSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchema\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchemaSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchemaResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchemaSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchema\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchemaSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchemaResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
      }
      if (num != 0)
      {
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ExportSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("Export\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ExportSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ExportResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
      }
      if (flag5)
      {
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ImportSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("Import\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("ImportSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ImportResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
      }
      if (flag6)
      {
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SubmitSoapIn\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("Submit\" />");
        this.bld.AppendLine("  </wsdl:message>");
        this.bld.Append("  <wsdl:message name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SubmitSoapOut\">");
        this.bld.Append("    <wsdl:part name=\"parameters\" element=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SubmitResponse\" />");
        this.bld.AppendLine("  </wsdl:message>");
      }
    }
    this.bld.AppendLine("  <wsdl:portType name=\"ScreenSoap\">");
    this.bld.AppendLine("    <wsdl:operation name=\"GetScenario\">");
    this.bld.AppendLine("      <wsdl:input message=\"tns:GetScenarioSoapIn\" />");
    this.bld.AppendLine("      <wsdl:output message=\"tns:GetScenarioSoapOut\" />");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"Login\">");
    this.bld.AppendLine("      <wsdl:input message=\"tns:LoginSoapIn\" />");
    this.bld.AppendLine("      <wsdl:output message=\"tns:LoginSoapOut\" />");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name =\"Logout\">");
    this.bld.AppendLine("      <wsdl:input message =\"tns:LogoutSoapIn\"/>");
    this.bld.AppendLine("      <wsdl:output message =\"tns:LogoutSoapOut\"/>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetBusinessDate\">");
    this.bld.AppendLine("      <wsdl:input message=\"tns:SetBusinessDateSoapIn\" />");
    this.bld.AppendLine("      <wsdl:output message=\"tns:SetBusinessDateSoapOut\" />");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetLocaleName\">");
    this.bld.AppendLine("      <wsdl:input message=\"tns:SetLocaleNameSoapIn\" />");
    this.bld.AppendLine("      <wsdl:output message=\"tns:SetLocaleNameSoapOut\" />");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetSchemaMode\">");
    this.bld.AppendLine("      <wsdl:input message=\"tns:SetSchemaModeSoapIn\" />");
    this.bld.AppendLine("      <wsdl:output message=\"tns:SetSchemaModeSoapOut\" />");
    this.bld.AppendLine("    </wsdl:operation>");
    if (!flag3)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Clear\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:ClearSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:ClearSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"GetProcessStatus\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:GetProcessStatusSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:GetProcessStatusSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag4)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"GetSchema\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:GetSchemaSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:GetSchemaSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"SetSchema\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:SetSchemaSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:SetSchemaSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag1)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Export\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:ExportSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:ExportSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag2)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Import\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:ImportSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:ImportSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag3)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Submit\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:SubmitSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:SubmitSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (includeUntyped)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedClear\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedClearSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedClearSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedGetProcessStatus\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedGetProcessStatusSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedGetProcessStatusSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedGetSchema\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedGetSchemaSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedGetSchemaSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedSetSchema\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedSetSchemaSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedSetSchemaSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedSubmit\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedSubmitSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedSubmitSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedExport\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedExportSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedExportSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedImport\">");
      this.bld.AppendLine("      <wsdl:input message=\"tns:UntypedImportSoapIn\" />");
      this.bld.AppendLine("      <wsdl:output message=\"tns:UntypedImportSoapOut\" />");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    foreach (KeyValuePair<string, bool[]> keyValuePair in this.processed)
    {
      string key = keyValuePair.Key;
      int num = keyValuePair.Value[0] ? 1 : 0;
      bool flag7 = keyValuePair.Value[1];
      bool flag8 = keyValuePair.Value[2];
      if (flag8)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Clear\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ClearSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ClearSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatus\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatusSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatusSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if ((num | (flag7 ? 1 : 0) | (flag8 ? 1 : 0)) != 0)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchema\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchemaSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchemaSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchema\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchemaSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchemaSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (flag7)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Import\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ImportSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ImportSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (num != 0)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Export\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ExportSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("ExportSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (flag8)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Submit\">");
        this.bld.Append("      <wsdl:input message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SubmitSoapIn\" />");
        this.bld.Append("      <wsdl:output message=\"tns:");
        this.bld.Append(key);
        this.bld.AppendLine("SubmitSoapOut\" />");
        this.bld.AppendLine("    </wsdl:operation>");
      }
    }
    this.bld.AppendLine("  </wsdl:portType>");
    this.bld.AppendLine("  <wsdl:binding name=\"ScreenSoap\" type=\"tns:ScreenSoap\">");
    this.bld.AppendLine("    <soap:binding transport=\"http://schemas.xmlsoap.org/soap/http\" />");
    this.bld.AppendLine("    <wsdl:operation name=\"GetScenario\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/GetScenario\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"Login\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Login\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"Logout\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Logout\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetBusinessDate\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/SetBusinessDate\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetLocaleName\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/SetLocaleName\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    this.bld.AppendLine("    <wsdl:operation name=\"SetSchemaMode\">");
    this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/SetSchemaMode\" style=\"document\" />");
    this.bld.AppendLine("      <wsdl:input>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:input>");
    this.bld.AppendLine("      <wsdl:output>");
    this.bld.AppendLine("        <soap:body use=\"literal\" />");
    this.bld.AppendLine("      </wsdl:output>");
    this.bld.AppendLine("    </wsdl:operation>");
    if (!flag3)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Clear\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Clear\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"GetProcessStatus\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/GetProcessStatus\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag4)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"GetSchema\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/GetSchema\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"SetSchema\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/SetSchema\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag1)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Export\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Export\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag2)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Import\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Import\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (!flag3)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"Submit\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/Submit\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    if (includeUntyped)
    {
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedClear\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedClear\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedGetProcessStatus\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedGetProcessStatus\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedGetSchema\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedGetSchema\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedSetSchema\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedSetSchema\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedSubmit\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedSubmit\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedExport\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedExport\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
      this.bld.AppendLine("    <wsdl:operation name=\"UntypedImport\">");
      this.bld.AppendLine("      <soap:operation soapAction=\"http://www.acumatica.com/generic/UntypedImport\" style=\"document\" />");
      this.bld.AppendLine("      <wsdl:input>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:input>");
      this.bld.AppendLine("      <wsdl:output>");
      this.bld.AppendLine("        <soap:body use=\"literal\" />");
      this.bld.AppendLine("      </wsdl:output>");
      this.bld.AppendLine("    </wsdl:operation>");
    }
    foreach (KeyValuePair<string, bool[]> keyValuePair in this.processed)
    {
      string key = keyValuePair.Key;
      int num = keyValuePair.Value[0] ? 1 : 0;
      bool flag9 = keyValuePair.Value[1];
      bool flag10 = keyValuePair.Value[2];
      if (flag10)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Clear\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/Clear\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetProcessStatus\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/GetProcessStatus\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if ((num | (flag9 ? 1 : 0) | (flag10 ? 1 : 0)) != 0)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("GetSchema\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/GetSchema\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("SetSchema\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/SetSchema\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (num != 0)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Export\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/Export\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (flag9)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Import\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/Import\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
      }
      if (flag10)
      {
        this.bld.Append("    <wsdl:operation name=\"");
        this.bld.Append(key);
        this.bld.AppendLine("Submit\">");
        this.bld.Append("      <soap:operation soapAction=\"http://www.acumatica.com/generic/");
        this.bld.Append(key);
        this.bld.AppendLine("/Submit\" style=\"document\" />");
        this.bld.AppendLine("      <wsdl:input>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:input>");
        this.bld.AppendLine("      <wsdl:output>");
        this.bld.AppendLine("        <soap:body use=\"literal\" />");
        this.bld.AppendLine("      </wsdl:output>");
        this.bld.AppendLine("    </wsdl:operation>");
      }
    }
    this.bld.AppendLine("  </wsdl:binding>");
    this.bld.AppendLine("  <wsdl:service name=\"Screen\">");
    this.bld.AppendLine("    <wsdl:port name=\"ScreenSoap\" binding=\"tns:ScreenSoap\">");
    this.bld.Append("      <soap:address location=\"http://localhost/Site/Soap/");
    this.bld.Append(serviceID);
    this.bld.AppendLine(".asmx\" />");
    this.bld.AppendLine("    </wsdl:port>");
    this.bld.AppendLine("  </wsdl:service>");
    this.bld.AppendLine("</wsdl:definitions>");
    return this.bld.ToString();
  }
}
