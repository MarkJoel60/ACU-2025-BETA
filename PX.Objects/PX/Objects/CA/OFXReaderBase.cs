// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.OFXReaderBase
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

#nullable disable
namespace PX.Objects.CA;

public abstract class OFXReaderBase
{
  protected readonly string[] Splitters = new string[4]
  {
    "\r\n",
    "\n",
    "\r",
    " "
  };
  protected OFXReaderBase.OFXHeader header = new OFXReaderBase.OFXHeader();
  protected OFXReaderBase.STMTTRNRS content = new OFXReaderBase.STMTTRNRS();
  protected bool useConverter = true;
  private const string OpenOfxTag = "<OFX";
  private const string UnicodeEncoding = "UNICODE";
  private const string AsciiEncoding = "USASCII";
  private const string amp = "&amp;";
  private const string apos = "&apos;";
  private const string quot = "&quot;";
  private const string gt = "&gt;";
  private const string lt = "&lt;";

  public OFXReaderBase() => this.useConverter = true;

  public virtual void Read(byte[] aInput)
  {
    Encoding encoding = Encoding.GetEncoding(1252);
    string str = encoding.GetString(aInput);
    int num = str.IndexOf("<OFX");
    if (num < 0)
      throw new PXException("Provided content is not recognized as a valid OFX format");
    Encoding aEncoding;
    this.ReadHeader(str.Substring(0, num), out aEncoding);
    if (aEncoding.CodePage != encoding.CodePage)
    {
      str = aEncoding.GetString(aInput);
      num = str.IndexOf("<OFX");
    }
    this.ReadOFXMessage(str.Substring(num));
  }

  public bool IsValidInput(byte[] aInput)
  {
    Encoding encoding = Encoding.GetEncoding(1252);
    int num = aInput.Length < 600 ? aInput.Length : 600;
    byte[] bytes = aInput;
    int count = num;
    return OFXReaderBase.IsValidInput(encoding.GetString(bytes, 0, count));
  }

  public bool AllowsMultipleAccounts() => true;

  public OFXReaderBase.STMTTRNRS Content => this.content;

  public bool HasRecords => this.content != null && this.content.Stmtrs.Count > 0;

  protected virtual void ReadHeader(string aHeader, out Encoding aEncoding)
  {
    int num = aHeader.IndexOf("OFXHEADER:");
    bool flag = false;
    if (num >= 0)
    {
      this.ReadHeader100(aHeader);
      flag = true;
    }
    else if (aHeader.IndexOf("<?OFX") >= 0)
    {
      flag = true;
      this.ReadHeader200(aHeader);
    }
    if (!flag)
      throw new PXException("Unrecognized format of the message header");
    aEncoding = this.DetectEncoding();
  }

  protected virtual void ReadHeader100(string aHeader)
  {
    string[] strArray1 = new string[9]
    {
      "OFXHEADER:",
      "VERSION:",
      "CHARSET:",
      "ENCODING:",
      "DATA:",
      "SECURITY:",
      "COMPRESSION:",
      "OLDFILEUID:",
      "NEWFILEUID:"
    };
    SortedDictionary<int, string> source = new SortedDictionary<int, string>();
    foreach (string str in strArray1)
    {
      if (aHeader.IndexOf(str) >= 0)
        source.Add(aHeader.IndexOf(str), str);
    }
    for (int index = 0; index < source.Count; ++index)
    {
      KeyValuePair<int, string> keyValuePair = source.ElementAt<KeyValuePair<int, string>>(index);
      int key = keyValuePair.Key;
      string str = aHeader;
      int startIndex = key;
      int num1;
      if (index == source.Count - 1)
      {
        num1 = aHeader.Length;
      }
      else
      {
        keyValuePair = source.ElementAt<KeyValuePair<int, string>>(index + 1);
        num1 = keyValuePair.Key;
      }
      int num2 = key;
      int length = num1 - num2;
      string[] strArray2 = str.Substring(startIndex, length).Split(':');
      switch (strArray2[0].Trim())
      {
        case "OFXHEADER":
          this.header.OFXHEADER = this.ExtractValueFromHeader(strArray2[1].Trim());
          break;
        case "VERSION":
          this.header.VERSION = this.ExtractValueFromHeader(strArray2[1].Trim());
          break;
        case "CHARSET":
          this.header.CHARSET = this.ExtractValueFromHeader(strArray2[1].Trim());
          break;
        case "ENCODING":
          this.header.ENCODING = this.ExtractValueFromHeader(strArray2[1].Trim());
          break;
        case "DATA":
          this.header.DATA = this.ExtractValueFromHeader(strArray2[1].Trim());
          break;
      }
    }
  }

  private string ExtractValueFromHeader(string input)
  {
    return input.Split(this.Splitters, StringSplitOptions.RemoveEmptyEntries)[0];
  }

  protected Encoding DetectEncoding()
  {
    if (this.header.ENCODING == "UNICODE")
      return Encoding.UTF8;
    try
    {
      return this.header.ENCODING == "USASCII" ? Encoding.GetEncoding(int.Parse(this.header.CHARSET)) : Encoding.GetEncoding(this.header.ENCODING);
    }
    catch
    {
      throw new PXException("Unsupported Encoding {0} or Charset (1) detected in the header", new object[2]
      {
        (object) this.header.ENCODING,
        (object) this.header.CHARSET
      });
    }
  }

  protected virtual void ReadHeader200(string aHeader)
  {
    XmlReader xmlReader = XmlReader.Create((TextReader) new StringReader(aHeader), new XmlReaderSettings()
    {
      ConformanceLevel = ConformanceLevel.Fragment
    });
    while (xmlReader.Read())
    {
      switch (xmlReader.NodeType)
      {
        case XmlNodeType.ProcessingInstruction:
          if (xmlReader.Name == "OFX" && xmlReader.HasValue)
          {
            Dictionary<string, string> dictionary = ((IEnumerable<string>) xmlReader.Value.Split(this.Splitters, StringSplitOptions.RemoveEmptyEntries)).Select<string, string[]>((Func<string, string[]>) (str => str.Split('='))).ToDictionary<string[], string, string>((Func<string[], string>) (pair => pair[0]), (Func<string[], string>) (pair => pair[1].Trim('"')));
            if (dictionary.ContainsKey("OFXHEADER"))
              this.header.OFXHEADER = dictionary["OFXHEADER"];
            if (dictionary.ContainsKey("VERSION"))
              this.header.VERSION = dictionary["VERSION"];
            if (dictionary.ContainsKey("encoding"))
            {
              this.header.ENCODING = dictionary["encoding"];
              continue;
            }
            continue;
          }
          continue;
        case XmlNodeType.XmlDeclaration:
          string attribute = xmlReader.GetAttribute("encoding");
          this.header.ENCODING = string.IsNullOrEmpty(attribute) ? "UNICODE" : attribute;
          continue;
        default:
          continue;
      }
    }
  }

  protected virtual void ReadOFXMessage(string aOFXMessage)
  {
    using (XmlReader reader = this.CreateReader(!this.NeedConvertionToXml() || !this.useConverter ? aOFXMessage : this.ConvertToXML(aOFXMessage)))
    {
      while (reader.Read())
      {
        if (reader.NodeType == XmlNodeType.Element && (reader.Name == "STMTTRNRS" || reader.Name == "CCSTMTTRNRS"))
          OFXReaderBase.Read(reader, this.content, reader.Name);
      }
    }
  }

  protected XmlReader CreateReader(string ofxBody)
  {
    XmlReader reader = (XmlReader) null;
    if ((this.header.MajorVersion >= 200 || this.useConverter) && (this.header.MajorVersion >= 200 || this.useConverter))
      reader = (XmlReader) new XmlTextReader((TextReader) new StringReader(ofxBody));
    return reader;
  }

  protected string ConvertToXML(string aSGMLMessage)
  {
    Dictionary<string, Stack<OFXReaderBase.TagInfo>> dictionary = new Dictionary<string, Stack<OFXReaderBase.TagInfo>>();
    int startIndex1 = 0;
    OFXReaderBase.TagInfo tagInfo1 = (OFXReaderBase.TagInfo) null;
    int num1;
    int num2;
    for (; (num1 = aSGMLMessage.IndexOf('<', startIndex1)) >= 0; startIndex1 = num2 + 1)
    {
      if (tagInfo1 != null)
      {
        tagInfo1.endPosition = num1;
        tagInfo1 = (OFXReaderBase.TagInfo) null;
      }
      num2 = aSGMLMessage.IndexOf('>', num1);
      if (num2 > 0)
      {
        if (aSGMLMessage[num2 - 1] != '/')
        {
          string str = aSGMLMessage.Substring(num1 + 1, num2 - num1 - 1);
          if (str.Trim().Length > 0)
          {
            if (str[0] != '/')
            {
              if (!dictionary.ContainsKey(str))
                dictionary.Add(str, new Stack<OFXReaderBase.TagInfo>());
              tagInfo1 = new OFXReaderBase.TagInfo(str, num1);
              dictionary[str].Push(tagInfo1);
            }
            else
            {
              string key = str.Substring(1);
              if (dictionary.ContainsKey(key))
              {
                if (dictionary[key].Any<OFXReaderBase.TagInfo>())
                {
                  dictionary[key].Pop();
                  tagInfo1 = (OFXReaderBase.TagInfo) null;
                }
                else
                  throw new PXException("The document has an invalid format - missing open tag: <{0}> at position {1}.", new object[2]
                  {
                    (object) key,
                    (object) num1
                  });
              }
            }
          }
        }
      }
      else
        throw new PXException("Document has invalid format - tag at position {0} is missing closing bracket (>)", new object[1]
        {
          (object) num1
        });
    }
    SortedDictionary<int, OFXReaderBase.TagInfo> sortedDictionary = new SortedDictionary<int, OFXReaderBase.TagInfo>();
    int num3 = 0;
    foreach (KeyValuePair<string, Stack<OFXReaderBase.TagInfo>> keyValuePair in dictionary)
    {
      foreach (OFXReaderBase.TagInfo tagInfo2 in keyValuePair.Value)
      {
        sortedDictionary.Add(tagInfo2.startPosition, tagInfo2);
        num3 += tagInfo2.tagName.Length + 3;
      }
    }
    StringBuilder stringBuilder = new StringBuilder(aSGMLMessage.Length + num3);
    int startIndex2 = 0;
    foreach (KeyValuePair<int, OFXReaderBase.TagInfo> keyValuePair in sortedDictionary)
    {
      OFXReaderBase.TagInfo tagInfo3 = keyValuePair.Value;
      if (!tagInfo3.IsEndPositionValid)
        tagInfo3.endPosition = aSGMLMessage.IndexOf("<", tagInfo3.startPosition + tagInfo3.OpenTag.Length);
      int endPosition = tagInfo3.endPosition;
      if (aSGMLMessage.Substring(endPosition - 2, 2) == "\r\n")
        endPosition -= 2;
      stringBuilder.Append(aSGMLMessage.Substring(startIndex2, endPosition - startIndex2));
      stringBuilder.Append(tagInfo3.CloseTag);
      startIndex2 = endPosition;
    }
    if (startIndex2 < aSGMLMessage.Length)
      stringBuilder.Append(aSGMLMessage.Substring(startIndex2));
    return this.ReplaceSpecialSymbols(stringBuilder.ToString());
  }

  protected virtual string ReplaceSpecialSymbols(string input)
  {
    StringBuilder stringBuilder = new StringBuilder(input.Length);
    int startIndex1 = 0;
    int startIndex2 = 0;
    int startIndex3;
    while ((startIndex3 = input.IndexOf('>', startIndex1)) >= 0)
    {
      stringBuilder.Append(input.Substring(startIndex2, startIndex3 - startIndex2));
      int num = input.IndexOf('<', startIndex3);
      if (num < 0)
      {
        stringBuilder.Append(input.Substring(startIndex3, input.Length - startIndex3));
        break;
      }
      stringBuilder.Append(this.ReplaceSymbolsInValue(input.Substring(startIndex3, num - startIndex3)));
      startIndex1 = num;
      startIndex2 = startIndex1;
    }
    return stringBuilder.ToString();
  }

  protected virtual string ReplaceSymbolsInValue(string input)
  {
    if (input.Contains("&"))
    {
      int startIndex1 = 0;
      int startIndex2;
      while ((startIndex2 = input.IndexOf('&', startIndex1)) >= 0)
      {
        if (input.IndexOf("&amp;", startIndex2) == startIndex2)
          startIndex1 = startIndex2 + "&amp;".Length;
        else if (input.IndexOf("&gt;", startIndex2) == startIndex2)
          startIndex1 = startIndex2 + "&gt;".Length;
        else if (input.IndexOf("&lt;", startIndex2) == startIndex2)
          startIndex1 = startIndex2 + "&lt;".Length;
        else if (input.IndexOf("&quot;", startIndex2) == startIndex2)
          startIndex1 = startIndex2 + "&quot;".Length;
        else if (input.IndexOf("&apos;", startIndex2) == startIndex2)
        {
          startIndex1 = startIndex2 + "&apos;".Length;
        }
        else
        {
          input = input.Remove(startIndex2, 1);
          input = input.Insert(startIndex2, "&amp;");
          startIndex1 = startIndex2 + "&amp;".Length;
        }
      }
    }
    input = input.Replace("'", "&apos;");
    input = input.Replace("\"", "&quot;");
    return input;
  }

  protected virtual bool NeedConvertionToXml() => this.header.MajorVersion < 200;

  protected static bool IsValidInput(string aInput) => aInput.IndexOf("<OFX") > 0;

  protected static void Read(
    XmlReader reader,
    OFXReaderBase.STMTTRNRS aTranList,
    string sectionTag)
  {
    string str = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        str = reader.Name;
        if (reader.Name == "STMTRS" || reader.Name == "CCSTMTRS")
        {
          OFXReaderBase.STMTRS aTranList1 = new OFXReaderBase.STMTRS();
          aTranList.Stmtrs.Add(aTranList1);
          OFXReaderBase.Read(reader, aTranList1, reader.Name);
        }
      }
      if (reader.NodeType == XmlNodeType.Text && str == "TRNUID")
        aTranList.TRNUID = reader.Value.Trim();
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == sectionTag)
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.STMTRS aTranList, string sectionTag)
  {
    foreach (PropertyInfo property in aTranList.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
    {
      string name = property.Name;
      Type propertyType = property.PropertyType;
    }
    string str = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        str = reader.Name;
        if (reader.Name == "BANKACCTFROM")
          OFXReaderBase.Read(reader, aTranList.BANKACCTFROM);
        if (reader.Name == "CCACCTFROM")
          OFXReaderBase.Read(reader, aTranList.CCACCTFROM);
        if (reader.Name == "BANKTRANLIST")
          OFXReaderBase.Read(reader, aTranList.BANKTRANLIST);
        if (str == "LEDGERBAL")
          OFXReaderBase.Read(reader, aTranList.LEDGERBAL, "LEDGERBAL");
        if (str == "AVAILBAL")
          OFXReaderBase.Read(reader, aTranList.AVAILBAL, "AVAILBAL");
      }
      if (reader.NodeType == XmlNodeType.Text && str == "CURDEF")
        aTranList.CURDEF = reader.Value.Trim();
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == sectionTag)
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.BankAcctInfo aInfo)
  {
    string str1 = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
        str1 = reader.Name;
      if (reader.NodeType == XmlNodeType.Text)
      {
        string str2 = reader.Value.Trim();
        switch (str1)
        {
          case "BANKID":
            aInfo.BANKID = str2;
            break;
          case "ACCTID":
            aInfo.ACCTID = str2;
            break;
          case "ACCTTYPE":
            aInfo.ACCTTYPE = str2;
            break;
          case "BRANCHID":
            aInfo.BRANCHID = str2;
            break;
        }
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "BANKACCTFROM")
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.CCAcctInfo aInfo)
  {
    string str1 = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
        str1 = reader.Name;
      if (reader.NodeType == XmlNodeType.Text)
      {
        string str2 = reader.Value.Trim();
        switch (str1)
        {
          case "ACCTID":
            aInfo.ACCTID = str2;
            break;
          case "ACCTKEY":
            aInfo.ACCTKEY = str2;
            break;
        }
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "CCACCTFROM")
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.BankTransList aTransList)
  {
    string str = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        str = reader.Name;
        if (str == "STMTTRN")
        {
          OFXReaderBase.STMTTRN aTran = new OFXReaderBase.STMTTRN();
          aTransList.Trans.Add(aTran);
          OFXReaderBase.Read(reader, aTran);
        }
      }
      if (reader.NodeType == XmlNodeType.Text)
      {
        try
        {
          if (str == "DTSTART")
          {
            string aDateAsString = reader.Value.Trim();
            aTransList.DTSTART = OFXReaderBase.ParseDateTime(aDateAsString);
          }
          if (str == "DTEND")
          {
            string aDateAsString = reader.Value.Trim();
            aTransList.DTEND = OFXReaderBase.ParseDateTime(aDateAsString);
          }
        }
        catch (FormatException ex)
        {
          throw new PXException("The Field {0} has invalid format: {1}", new object[2]
          {
            (object) str,
            (object) ex.Message
          });
        }
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "BANKTRANLIST")
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.STMTTRN aTran)
  {
    string str = string.Empty;
    foreach (PropertyInfo property in aTran.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
    {
      string name = property.Name;
      Type propertyType = property.PropertyType;
    }
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        str = reader.Name;
        if (reader.Name == "PAYEE")
          OFXReaderBase.Read(reader, aTran.PAYEE);
      }
      try
      {
        if (reader.NodeType == XmlNodeType.Text)
        {
          if (str == "DTPOSTED")
          {
            string aDateAsString = reader.Value.Trim();
            aTran.DTPOSTED = OFXReaderBase.ParseDateTime(aDateAsString);
          }
          if (str == "DTUSER")
          {
            string aDateAsString = reader.Value.Trim();
            aTran.DTUSER = OFXReaderBase.ParseDateTime(aDateAsString);
          }
          if (str == "DTAVAIL")
          {
            string aDateAsString = reader.Value.Trim();
            aTran.DTAVAIL = OFXReaderBase.ParseDateTime(aDateAsString);
          }
          if (str == "TRNTYPE")
            aTran.TRNTYPE = reader.Value.Trim();
          if (str == "FITID")
            aTran.FITID = reader.Value.Trim();
          if (str == "NAME")
            aTran.NAME = reader.Value.Trim();
          if (str == "MEMO")
            aTran.MEMO = reader.Value.Trim();
          if (str == "CHECKNUM")
            aTran.CHECKNUM = reader.Value.Trim();
          if (str == "REFNUM")
            aTran.CHECKNUM = reader.Value.Trim();
          if (str == "SIC")
            aTran.CHECKNUM = reader.Value.Trim();
          if (str == "PAYEEID")
            aTran.CHECKNUM = reader.Value.Trim();
          if (str == "TRNAMT")
            aTran.TRNAMT = OFXReaderBase.ParseAmount(reader.Value.Trim());
        }
      }
      catch (FormatException ex)
      {
        throw new PXException("The Value {0} for the Field {1} in the transaction {2} has invalid format: {3}", new object[4]
        {
          (object) reader.Value,
          (object) str,
          (object) aTran.FITID,
          (object) ex.Message
        });
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "STMTTRN")
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.Payee aPayee)
  {
    string str = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
        str = reader.Name;
      if (reader.NodeType == XmlNodeType.Text)
      {
        if (str == "NAME")
          aPayee.NAME = reader.Value.Trim();
        if (str == "ADDR1")
          aPayee.ADDR1 = reader.Value.Trim();
        if (str == "ADDR2")
          aPayee.ADDR2 = reader.Value.Trim();
        if (str == "ADDR3")
          aPayee.ADDR2 = reader.Value.Trim();
        if (str == "CITY")
          aPayee.NAME = reader.Value.Trim();
        if (str == "POSTALCODE")
          aPayee.POSTALCODE = reader.Value.Trim();
        if (str == "COUNTRY")
          aPayee.COUNTRY = reader.Value.Trim();
        if (str == "PHONE")
          aPayee.COUNTRY = reader.Value.Trim();
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "PAYEE")
        break;
    }
  }

  protected static void Read(XmlReader reader, OFXReaderBase.BalanceInfo aInfo, string aEndTag)
  {
    string str1 = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
        str1 = reader.Name;
      if (reader.NodeType == XmlNodeType.Text)
      {
        string str2 = reader.Value.Trim();
        try
        {
          switch (str1)
          {
            case "BALAMT":
              aInfo.BALAMT = OFXReaderBase.ParseAmount(str2);
              break;
            case "DTASOF":
              aInfo.DTASOF = OFXReaderBase.ParseDateTime(str2);
              break;
          }
        }
        catch (FormatException ex)
        {
          throw new PXException("The Field {0} has invalid format: {1}", new object[2]
          {
            (object) str1,
            (object) ex.Message
          });
        }
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == aEndTag)
        break;
    }
  }

  protected static void Read(XmlReader reader, object aTarget, string sectionTag)
  {
    PropertyInfo[] properties = aTarget.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
    Dictionary<string, PropertyInfo> dictionary1 = new Dictionary<string, PropertyInfo>();
    Dictionary<string, PropertyInfo> dictionary2 = new Dictionary<string, PropertyInfo>();
    foreach (PropertyInfo propertyInfo in properties)
    {
      string name = propertyInfo.Name;
      Type propertyType = propertyInfo.PropertyType;
      if (propertyType.IsValueType || propertyType == typeof (string))
        dictionary2.Add(name, propertyInfo);
      else if (propertyType.IsClass)
        dictionary1.Add(name, propertyInfo);
    }
    string str = string.Empty;
    while (reader.Read())
    {
      if (reader.NodeType == XmlNodeType.Element)
      {
        str = reader.Name;
        if (dictionary1.ContainsKey(str))
          OFXReaderBase.Read(reader, dictionary1[str].GetValue(aTarget, (object[]) null), str);
      }
      if (reader.NodeType == XmlNodeType.Text && dictionary2.ContainsKey(str))
      {
        PropertyInfo propertyInfo = dictionary2[str];
        if ((propertyInfo.PropertyType == typeof (DateTime?) ? 1 : (propertyInfo.PropertyType == typeof (DateTime) ? 1 : 0)) != 0)
          propertyInfo.SetValue(aTarget, (object) OFXReaderBase.ParseDateTime(reader.Value), (object[]) null);
        else if (propertyInfo.PropertyType == typeof (string))
        {
          propertyInfo.SetValue(aTarget, (object) reader.Value, (object[]) null);
        }
        else
        {
          MethodInfo method = propertyInfo.PropertyType.GetMethod("Parse", BindingFlags.Static);
          if (method != (MethodInfo) null)
          {
            object[] parameters = new object[1]
            {
              (object) reader.Value
            };
            propertyInfo.SetValue(aTarget, method.Invoke((object) null, parameters), (object[]) null);
          }
        }
      }
      if (reader.NodeType == XmlNodeType.EndElement && reader.Name == sectionTag)
        break;
    }
  }

  protected static DateTime? ParseDateTime(string aDateAsString)
  {
    DateTime? nullable = new DateTime?();
    bool flag1 = false;
    int length = aDateAsString.IndexOf('[');
    int num1 = length > 0 ? 1 : 0;
    TimeZoneInfo sourceTimeZone = (TimeZoneInfo) null;
    string empty = string.Empty;
    string str1;
    if (num1 == 0)
    {
      str1 = aDateAsString;
    }
    else
    {
      str1 = aDateAsString.Substring(0, length);
      int num2 = aDateAsString.IndexOf(']');
      string[] strArray1 = aDateAsString.Substring(length + 1, num2 - length - 1).Split(':');
      string str2 = strArray1[0];
      bool flag2 = true;
      if (flag1 && strArray1.Length > 1)
      {
        flag2 = false;
        string id = strArray1[1];
        try
        {
          sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(id);
        }
        catch (TimeZoneNotFoundException ex)
        {
          flag2 = true;
        }
        catch (InvalidTimeZoneException ex)
        {
          flag2 = true;
        }
      }
      if (flag2)
      {
        string[] strArray2 = str2.Split('.');
        int[] numArray = new int[2];
        for (int index = 0; index < strArray2.Length; ++index)
        {
          if (index < 2)
            numArray[index] = int.Parse(strArray2[index]);
        }
        str1 = $"{str1} {numArray[0]:+00;-00;+00}:{numArray[1]:00}";
      }
    }
    string[] formats1 = new string[3]
    {
      "yyyyMMdd",
      "yyyyMMddHHmmss",
      "yyyyMMddHHmmss.fff"
    };
    if (str1.Length == 8 || str1.Length == 14 || str1.Length == 18)
    {
      nullable = new DateTime?(DateTime.ParseExact(str1, formats1, (IFormatProvider) null, DateTimeStyles.NoCurrentDateDefault));
    }
    else
    {
      string[] formats2 = new string[3]
      {
        "yyyyMMdd zzz",
        "yyyyMMddHHmmss zzz",
        "yyyyMMddHHmmss.fff zzz"
      };
      nullable = new DateTime?(DateTimeOffset.ParseExact(str1, formats2, (IFormatProvider) null, DateTimeStyles.AdjustToUniversal).DateTime);
    }
    return sourceTimeZone != null ? new DateTime?(TimeZoneInfo.ConvertTimeToUtc(nullable.Value, sourceTimeZone)) : nullable;
  }

  protected static Decimal ParseAmount(string aNumber)
  {
    int num = aNumber.IndexOf(".") >= 0 ? 1 : 0;
    aNumber.IndexOf(",");
    Decimal amount;
    if (num != 0)
    {
      amount = Decimal.Parse(aNumber, NumberStyles.Number, (IFormatProvider) CultureInfo.InvariantCulture);
    }
    else
    {
      NumberFormatInfo numberFormat = new CultureInfo("en-US", false).NumberFormat;
      numberFormat.CurrencyDecimalSeparator = ",";
      numberFormat.NumberDecimalSeparator = ",";
      amount = Decimal.Parse(aNumber, NumberStyles.Integer | NumberStyles.AllowTrailingSign | NumberStyles.AllowDecimalPoint, (IFormatProvider) numberFormat);
    }
    return amount;
  }

  public class OFXMessage
  {
    public OFXReaderBase.STMTTRNRS Stmttrnrs;
  }

  public class STMTTRNRS
  {
    private string _TRNUID;
    private List<OFXReaderBase.STMTRS> _Stmtrs;

    public string TRNUID
    {
      get => this._TRNUID;
      set => this._TRNUID = value;
    }

    public List<OFXReaderBase.STMTRS> Stmtrs
    {
      get
      {
        if (this._Stmtrs == null)
          this._Stmtrs = new List<OFXReaderBase.STMTRS>(1);
        return this._Stmtrs;
      }
    }

    public void Clear()
    {
      this.TRNUID = string.Empty;
      if (this._Stmtrs == null)
        return;
      this._Stmtrs.Clear();
    }
  }

  public class STMTRS
  {
    private string _CURDEF;
    private OFXReaderBase.BalanceInfo _LEDGERBAL;
    private OFXReaderBase.BalanceInfo _AVAILBAL;
    private OFXReaderBase.BankAcctInfo _BANKACCTFROM;
    private OFXReaderBase.CCAcctInfo _CCACCTFROM;
    private OFXReaderBase.BankTransList _BANKTRANLIST;

    public STMTRS()
    {
      this._CCACCTFROM = (OFXReaderBase.CCAcctInfo) null;
      this._BANKACCTFROM = (OFXReaderBase.BankAcctInfo) null;
    }

    public string CURDEF
    {
      get => this._CURDEF;
      set => this._CURDEF = value;
    }

    public OFXReaderBase.BankAcctInfo BANKACCTFROM
    {
      get
      {
        if (this._BANKACCTFROM == null)
          this._BANKACCTFROM = new OFXReaderBase.BankAcctInfo();
        return this._BANKACCTFROM;
      }
    }

    public OFXReaderBase.CCAcctInfo CCACCTFROM
    {
      get
      {
        if (this._CCACCTFROM == null)
          this._CCACCTFROM = new OFXReaderBase.CCAcctInfo();
        return this._CCACCTFROM;
      }
    }

    public OFXReaderBase.BankTransList BANKTRANLIST
    {
      get
      {
        if (this._BANKTRANLIST == null)
          this._BANKTRANLIST = new OFXReaderBase.BankTransList();
        return this._BANKTRANLIST;
      }
    }

    public OFXReaderBase.BalanceInfo LEDGERBAL
    {
      get
      {
        if (this._LEDGERBAL == null)
          this._LEDGERBAL = new OFXReaderBase.BalanceInfo();
        return this._LEDGERBAL;
      }
    }

    public OFXReaderBase.BalanceInfo AVAILBAL
    {
      get
      {
        if (this._AVAILBAL == null)
          this._AVAILBAL = new OFXReaderBase.BalanceInfo();
        return this._AVAILBAL;
      }
    }

    public bool IsBankAccount() => this._BANKACCTFROM != null && this._CCACCTFROM == null;

    public bool IsCCAccount() => this._BANKACCTFROM == null && this._CCACCTFROM != null;

    public bool HasAccountInfo() => this.IsBankAccount() || this.IsCCAccount();
  }

  public class BankTransList
  {
    private DateTime? _DTSTART;
    private DateTime? _DTEND;
    private List<OFXReaderBase.STMTTRN> _trans;

    public DateTime? DTSTART
    {
      get => this._DTSTART;
      set => this._DTSTART = value;
    }

    public DateTime? DTEND
    {
      get => this._DTEND;
      set => this._DTEND = value;
    }

    public List<OFXReaderBase.STMTTRN> Trans
    {
      get
      {
        if (this._trans == null)
          this._trans = new List<OFXReaderBase.STMTTRN>();
        return this._trans;
      }
    }
  }

  public class BankAcctInfo
  {
    private string _BANKACCTFROM;
    private string _BANKID;
    private string _BRANCHID;
    private string _ACCTID;
    private string _ACCTTYPE;

    public string BANKACCTFROM
    {
      get => this._BANKACCTFROM;
      set => this._BANKACCTFROM = value;
    }

    public string BANKID
    {
      get => this._BANKID;
      set => this._BANKID = value;
    }

    public string BRANCHID
    {
      get => this._BRANCHID;
      set => this._BRANCHID = value;
    }

    public string ACCTID
    {
      get => this._ACCTID;
      set => this._ACCTID = value;
    }

    public string ACCTTYPE
    {
      get => this._ACCTTYPE;
      set => this._ACCTTYPE = value;
    }
  }

  public class STMTTRN
  {
    private string _TRNTYPE;
    private DateTime? _DTPOSTED;
    private DateTime? _DTUSER;
    private DateTime? _DTAVAIL;
    public Decimal _TRNAMT;
    private string _FITID;
    private string _NAME;
    private string _MEMO;
    private string _CHECKNUM;
    private string _REFNUM;
    private string _SIC;
    private string _PAYEEID;
    private OFXReaderBase.Payee _PAYEE;

    public string TRNTYPE
    {
      get => this._TRNTYPE;
      set => this._TRNTYPE = value;
    }

    public DateTime? DTPOSTED
    {
      get => this._DTPOSTED;
      set => this._DTPOSTED = value;
    }

    public DateTime? DTUSER
    {
      get => this._DTUSER;
      set => this._DTUSER = value;
    }

    public DateTime? DTAVAIL
    {
      get => this._DTAVAIL;
      set => this._DTAVAIL = value;
    }

    public Decimal TRNAMT
    {
      get => this._TRNAMT;
      set => this._TRNAMT = value;
    }

    public string FITID
    {
      get => this._FITID;
      set => this._FITID = value;
    }

    public string NAME
    {
      get => this._NAME;
      set => this._NAME = value;
    }

    public string MEMO
    {
      get => this._MEMO;
      set => this._MEMO = value;
    }

    public string CHECKNUM
    {
      get => this._CHECKNUM;
      set => this._CHECKNUM = value;
    }

    public string REFNUM
    {
      get => this._REFNUM;
      set => this._REFNUM = value;
    }

    public string SIC
    {
      get => this._SIC;
      set => this._SIC = value;
    }

    public string PAYEEID
    {
      get => this._PAYEEID;
      set => this._PAYEEID = value;
    }

    public OFXReaderBase.Payee PAYEE
    {
      get
      {
        if (this._PAYEE == null)
          this._PAYEE = new OFXReaderBase.Payee();
        return this._PAYEE;
      }
      set
      {
      }
    }

    public bool HasPayeeInfo() => this._PAYEE != null;
  }

  public class BalanceInfo
  {
    private Decimal _BALAMT;
    private DateTime? _DTASOF;

    public Decimal BALAMT
    {
      get => this._BALAMT;
      set => this._BALAMT = value;
    }

    public DateTime? DTASOF
    {
      get => this._DTASOF;
      set => this._DTASOF = value;
    }
  }

  public class Payee
  {
    private string _NAME;
    private string _ADDR1;
    private string _ADDR2;
    private string _ADDR3;
    private string _CITY;
    private string _STATE;
    private string _POSTALCODE;
    private string _COUNTRY;
    private string _PHONE;

    public string NAME
    {
      get => this._NAME;
      set => this._NAME = value;
    }

    public string ADDR1
    {
      get => this._ADDR1;
      set => this._ADDR1 = value;
    }

    public string ADDR2
    {
      get => this._ADDR2;
      set => this._ADDR2 = value;
    }

    public string ADDR3
    {
      get => this._ADDR3;
      set => this._ADDR3 = value;
    }

    public string CITY
    {
      get => this._CITY;
      set => this._CITY = value;
    }

    public string STATE
    {
      get => this._STATE;
      set => this._STATE = value;
    }

    public string POSTALCODE
    {
      get => this._POSTALCODE;
      set => this._POSTALCODE = value;
    }

    public string COUNTRY
    {
      get => this._COUNTRY;
      set => this._COUNTRY = value;
    }

    public string PHONE
    {
      get => this._PHONE;
      set => this._PHONE = value;
    }
  }

  public class CCAcctInfo
  {
    private string _ACCTID;
    private string _ACCTKEY;

    public string ACCTID
    {
      get => this._ACCTID;
      set => this._ACCTID = value;
    }

    public string ACCTKEY
    {
      get => this._ACCTKEY;
      set => this._ACCTKEY = value;
    }
  }

  public class OFXHeader
  {
    public string OFXHEADER;
    public string DATA;
    public string VERSION;
    public string CHARSET;
    public string ENCODING;
    public string COMPRESSION;

    public int MajorVersion => int.Parse(this.OFXHEADER);
  }

  protected class TagInfo
  {
    public string tagName;
    public int startPosition;
    public int endPosition;

    public TagInfo(string aName, int aPosition)
    {
      this.tagName = aName;
      this.startPosition = aPosition;
      this.endPosition = this.startPosition;
    }

    public bool IsEndPositionValid
    {
      get => this.endPosition >= this.startPosition + this.tagName.Length + 1;
    }

    public string OpenTag => $"<{this.tagName}>";

    public string CloseTag => $"</{this.tagName}>";
  }
}
