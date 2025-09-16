// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.BankFeed.DataSync.BAISYProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Api;
using PX.Data;
using PX.DataSync;
using PX.Objects.CM;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

#nullable disable
namespace PX.Objects.CA.BankFeed.DataSync;

/// <summary>
/// Data provider for File Bank Feeds supporting BAI2 and BTRS formats
/// </summary>
public class BAISYProvider : PXSYBaseFileProvider, IPXSYProvider
{
  private const char Delimeter = ',';
  private const char Terminator = '/';
  private long _lineNumber;
  private Dictionary<string, int> CurrencyDictionary = new Dictionary<string, int>();

  public string DefaultFileExtension => this.Extensiton;

  public virtual string ProviderName => PXMessages.Localize("BAI Provider for Bank Feeds");

  public BAISYProvider()
  {
    this._needAttachFile = false;
    this.FillCurrencyDictionary();
  }

  protected virtual void InitialiseSchema()
  {
  }

  protected virtual List<string> InternalGetSchemaObjects()
  {
    return new List<string>() { "BAI Fields" };
  }

  protected virtual List<SchemaFieldInfo> InternalGetSchemaFields(string objectName)
  {
    List<SchemaFieldInfo> schemaFields = new List<SchemaFieldInfo>();
    foreach (KeyValuePair<int, (string Name, Type FieldType)> keyValuePair in BAISchema.Schema)
    {
      SchemaFieldInfo schemaFieldInfo = new SchemaFieldInfo(keyValuePair.Key, keyValuePair.Value.Name, keyValuePair.Value.FieldType);
      schemaFields.Add(schemaFieldInfo);
    }
    return schemaFields;
  }

  protected virtual void InternalImport(PXSYTable table)
  {
    if (!this.ValidateFile())
      this.ThrowWrongFileFormatException();
    List<SchemaFieldInfo> schemaFields = ((PXSYBaseSchemaProvider) this).InternalGetSchemaFields(table.ObjectName);
    using (StreamReader reader = new StreamReader((Stream) new MemoryStream(this._file.BinData), Encoding.UTF8))
    {
      this._lineNumber = 0L;
      if (reader.EndOfStream)
        this.ThrowWrongFileFormatException();
      (int Code, string[] Row) rowFromString1 = this.GetRowFromString(this.ReadNextLine(reader), this._lineNumber);
      this.ValidateFileHeader(rowFromString1.Code, rowFromString1.Row);
      string str1 = rowFromString1.Row[5];
      string str2 = rowFromString1.Row[3];
      BAITransactionHeader transactionHeader = new BAITransactionHeader()
      {
        FileCreationDate = str2,
        FileIdentificationNumber = str1
      };
      string line = this.ProcessGroups(reader, transactionHeader, table, schemaFields);
      if (string.IsNullOrEmpty(line))
        return;
      (int Code, string[] Row) rowFromString2 = this.GetRowFromString(line, this._lineNumber);
      if (rowFromString2.Code != 99)
        throw new FileContentValidationException("Line {0} in the {1} file contains the following unexpected code sequence: {2}.", new object[3]
        {
          (object) this._lineNumber,
          (object) this._file.Name,
          (object) rowFromString2.Row[0]
        });
    }
  }

  protected virtual byte[] InternalExport(string objectName, PXSYTable table)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Reads ISO 4217 currency id's and decimal places from database and fills local dictionary.
  /// </summary>
  private void FillCurrencyDictionary()
  {
    foreach (PXDataRecord pxDataRecord in PXDatabase.SelectMulti<CurrencyList>(new PXDataField[3]
    {
      (PXDataField) new PXDataField<CurrencyList.curyID>(),
      (PXDataField) new PXDataField<CurrencyList.isoDecimalPlaces>(),
      (PXDataField) new PXDataFieldValue<CurrencyList.isoDecimalPlaces>((object) null, (PXComp) 7)
    }))
      this.CurrencyDictionary[pxDataRecord.GetString(0).ToUpper()] = (int) pxDataRecord.GetInt16(1).Value;
  }

  /// <summary>
  /// Search for currency code in ISO 4217 dictionary and return decimal places
  /// </summary>
  /// <param name="currencyCode"></param>
  /// <returns></returns>
  /// <exception cref="T:PX.Objects.CA.BankFeed.DataSync.FileContentValidationException"></exception>
  private int GetDecimalPlacesForCurrency(string currencyCode)
  {
    if (string.IsNullOrWhiteSpace(currencyCode))
      return 2;
    return this.CurrencyDictionary.ContainsKey(currencyCode.ToUpper()) ? this.CurrencyDictionary[currencyCode] : throw new FileContentValidationException("Line {0} in the {1} file contains an unknown code ({2}).", new object[3]
    {
      (object) this._lineNumber,
      (object) this._file.Name,
      (object) currencyCode
    });
  }

  private int GetTranIndex(string tranType)
  {
    switch (tranType)
    {
      case "Z":
      case "0":
      case "1":
      case "2":
        return 4;
      case "S":
        return 7;
      case "V":
        return 6;
      default:
        return -1;
    }
  }

  private (int Code, string[] Row) GetRowFromString(string line, long lineNumber)
  {
    if (line.Trim().Length == 0 || !char.IsDigit(line[0]))
      this.ThrowWrongFileFormatException();
    string[] strArray = line.Split(',');
    int result;
    return int.TryParse(strArray[0], out result) ? (result, strArray) : throw new FileContentValidationException("The system cannot extract the value of the {0} field from the following string: {1} (file: {2}; line number: {3}; position: {4}).", new object[5]
    {
      (object) "Record Code",
      (object) line,
      (object) this._file.Name,
      (object) this._lineNumber,
      (object) 1
    });
  }

  private void ThrowWrongFileFormatException()
  {
    throw new FileContentValidationException("The {0} data provider cannot read the format of the {1} file. Provide a file with the {2} file format.", new object[3]
    {
      (object) nameof (BAISYProvider),
      (object) this._file.Name,
      (object) "BAI2"
    });
  }

  private void ThrowEmptyTranNumberException()
  {
    throw new FileContentValidationException("The Bank Reference Number field in line {0} of the {1} file must not be left empty.", new object[2]
    {
      (object) this._lineNumber,
      (object) this._file.Name
    });
  }

  private void ValidateFileHeader(int code, string[] headerRow)
  {
    if (code == 1 && headerRow.Length >= 6)
      return;
    this.ThrowWrongFileFormatException();
  }

  private DateTime ValidateGroupHeader(int code, string[] headerRow, string line, long lineNumber)
  {
    if (code != 2 || headerRow.Length < 5)
      this.ThrowWrongFileFormatException();
    DateTime result;
    if (!DateTime.TryParseExact(headerRow[4], "yyMMdd", (IFormatProvider) new CultureInfo("en-US"), DateTimeStyles.None, out result))
      throw new FileContentValidationException("The system cannot extract the value of the {0} field from the following string: {1} (file: {2}; line number: {3}; position: {4}).", new object[5]
      {
        (object) "DateTime",
        (object) line,
        (object) this._file.Name,
        (object) lineNumber,
        (object) 5
      });
    return result.Date;
  }

  private void ValidateAccountHeader(int code, string[] accountRow)
  {
    if (code == 3 && accountRow.Length >= 3)
      return;
    this.ThrowWrongFileFormatException();
  }

  private string ReadNextLine(StreamReader reader)
  {
    string str = reader.ReadLine();
    ++this._lineNumber;
    if (this.StartsWithCode(str, 88))
      return str;
    return str.TrimEnd('/');
  }

  private string ReadAdditionalText(StreamReader reader, out string text)
  {
    text = "";
    if (reader.EndOfStream)
      return (string) null;
    string str1;
    for (str1 = this.ReadNextLine(reader); this.StartsWithCode(str1, 88); str1 = this.ReadNextLine(reader))
    {
      if (str1.Length > 3)
      {
        ref string local = ref text;
        string str2 = text;
        string str3 = str1;
        string str4 = str3.Substring(3, str3.Length - 3);
        string str5 = str2 + str4;
        local = str5;
      }
    }
    return str1;
  }

  private Decimal CalculateAmount(Decimal amount, int decimals)
  {
    return amount / (Decimal) Math.Pow(10.0, (double) decimals);
  }

  private string ProcessTransactions(
    StreamReader reader,
    string firstLine,
    BAITransactionHeader transactionHeader,
    PXSYTable table,
    List<SchemaFieldInfo> fields)
  {
    string str = firstLine;
    do
    {
      (int Code, string[] Row) rowFromString = this.GetRowFromString(str, this._lineNumber);
      if (rowFromString.Code != 16 /*0x10*/)
        return str;
      if (rowFromString.Row.Length < 5)
        this.ThrowEmptyTranNumberException();
      string s = rowFromString.Row[1];
      int tranCode;
      if (!int.TryParse(s, out tranCode))
        throw new FileContentValidationException("The system cannot extract the value of the {0} field from the following string: {1} (file: {2}; line number: {3}; position: {4}).", new object[5]
        {
          (object) "Type Code",
          (object) str,
          (object) this._file.Name,
          (object) this._lineNumber,
          (object) 2
        });
      if (!BAISchema.BAICodes.ContainsKey(tranCode))
        throw new FileContentValidationException("Line {0} in the {1} file contains an unknown code ({2}).", new object[3]
        {
          (object) this._lineNumber,
          (object) this._file.Name,
          (object) s
        });
      BAICodeType codeType = BAISchema.BAICodes[tranCode].CodeType;
      string codeDescription = BAISchema.BAICodes[tranCode].Descripton;
      if (codeType == BAICodeType.Skip)
      {
        str = this.ReadAdditionalText(reader, out string _);
        break;
      }
      int debitCreditSign = codeType == BAICodeType.Debit ? 1 : -1;
      string tranType = rowFromString.Row[3];
      int tranIndex = this.GetTranIndex(tranType);
      if (tranIndex == -1)
        throw new FileContentValidationException("Line {0} in the {1} file contains an unknown funds type ({2}).", new object[3]
        {
          (object) this._lineNumber,
          (object) this._file.Name,
          (object) tranType
        });
      if (rowFromString.Row.Length < tranIndex + 2)
        this.ThrowEmptyTranNumberException();
      string tranId = rowFromString.Row[tranIndex];
      if (string.IsNullOrWhiteSpace(tranId))
        this.ThrowEmptyTranNumberException();
      string customTranId = rowFromString.Row[tranIndex + 1];
      Decimal amount;
      if (!Decimal.TryParse(rowFromString.Row[2], out amount))
        throw new FileContentValidationException("The system cannot extract the value of the {0} field from the following string: {1} (file: {2}; line number: {3}; position: {4}).", new object[5]
        {
          (object) "Amount",
          (object) str,
          (object) this._file.Name,
          (object) this._lineNumber,
          (object) 3
        });
      amount = this.CalculateAmount(amount, transactionHeader.DecimalPlaces);
      string text = "";
      if (rowFromString.Row.Length > tranIndex + 2)
        text = rowFromString.Row[tranIndex + 2];
      string additionalText;
      str = this.ReadAdditionalText(reader, out additionalText);
      this.ImportRowFill(table, fields, (Func<int, PXSYItem>) (index =>
      {
        switch (index)
        {
          case 0:
            return new PXSYItem(transactionHeader.CurrentAccount);
          case 1:
            return new PXSYItem(tranId);
          case 2:
            return new PXSYItem(transactionHeader.CurrentDate.ToString());
          case 3:
            return new PXSYItem(((Decimal) debitCreditSign * amount).ToString());
          case 4:
            return new PXSYItem(text);
          case 5:
            return new PXSYItem(customTranId);
          case 6:
            return new PXSYItem(tranCode.ToString());
          case 7:
            return new PXSYItem(transactionHeader.FileIdentificationNumber);
          case 8:
            return new PXSYItem(transactionHeader.AccountCurrencyCode);
          case 9:
            return new PXSYItem(transactionHeader.FileCreationDate);
          case 10:
            return new PXSYItem(additionalText);
          case 11:
            return new PXSYItem(codeDescription);
          default:
            return new PXSYItem("");
        }
      }));
    }
    while (!reader.EndOfStream && this.StartsWithCode(str, 16 /*0x10*/));
    return str;
  }

  private string ProcessAccounts(
    StreamReader reader,
    BAITransactionHeader transactionHeader,
    PXSYTable table,
    List<SchemaFieldInfo> fields)
  {
    string str1 = this.ReadNextLine(reader);
    do
    {
      (int Code, string[] Row) rowFromString1 = this.GetRowFromString(str1, this._lineNumber);
      transactionHeader.CurrentAccount = rowFromString1.Row[1];
      transactionHeader.AccountCurrencyCode = rowFromString1.Row[2];
      transactionHeader.DecimalPlaces = this.GetDecimalPlacesForCurrency(transactionHeader.AccountCurrencyCode);
      string str2 = this.ReadAdditionalText(reader, out string _);
      if (!string.IsNullOrEmpty(str2))
        str2 = this.ProcessTransactions(reader, str2, transactionHeader, table, fields);
      (int Code, string[] Row) rowFromString2 = this.GetRowFromString(str2, this._lineNumber);
      if (rowFromString2.Code != 49)
        throw new FileContentValidationException("Line {0} in the {1} file contains the following unexpected code sequence: {2}.", new object[3]
        {
          (object) this._lineNumber,
          (object) this._file.Name,
          (object) rowFromString2.Row[0]
        });
      str1 = this.ReadNextLine(reader);
    }
    while (!reader.EndOfStream && this.StartsWithCode(str1, 3));
    return str1;
  }

  private string ProcessGroups(
    StreamReader reader,
    BAITransactionHeader transactionHeader,
    PXSYTable table,
    List<SchemaFieldInfo> fields)
  {
    string str = this.ReadNextLine(reader);
    if (!this.StartsWithCode(str, 2))
      return str;
    do
    {
      (int Code, string[] Row) rowFromString1 = this.GetRowFromString(str, this._lineNumber);
      DateTime dateTime = this.ValidateGroupHeader(rowFromString1.Code, rowFromString1.Row, str, this._lineNumber);
      transactionHeader.CurrentDate = dateTime;
      if (reader.EndOfStream)
        this.ThrowWrongFileFormatException();
      (int Code, string[] Row) rowFromString2 = this.GetRowFromString(this.ProcessAccounts(reader, transactionHeader, table, fields), this._lineNumber);
      if (rowFromString2.Code != 98)
        throw new FileContentValidationException("Line {0} in the {1} file contains the following unexpected code sequence: {2}.", new object[3]
        {
          (object) this._lineNumber,
          (object) this._file.Name,
          (object) rowFromString2.Row[0]
        });
      str = this.ReadNextLine(reader);
    }
    while (!reader.EndOfStream && this.StartsWithCode(str, 2));
    return str;
  }

  private bool ValidateFile()
  {
    using (StreamReader reader = new StreamReader((Stream) new MemoryStream(this._file.BinData), Encoding.UTF8))
    {
      this._lineNumber = 0L;
      if (!this.StartsWithCode(this.ReadNextLine(reader), 1))
        return false;
      string str1 = this.ReadNextLine(reader);
      if (this.StartsWithCode(str1, 99))
        return this.ValidateFileTrailer(str1, 0L, this._lineNumber);
      if (!this.StartsWithCode(str1, 2) || !this.StartsWithCode(this.ReadNextLine(reader), 3))
        return false;
      long groupCount = 1;
      string str2;
      do
      {
        str2 = this.ReadNextLine(reader);
        if (this.StartsWithCode(str2, 2))
          ++groupCount;
      }
      while (!reader.EndOfStream);
      return this.ValidateFileTrailer(str2, groupCount, this._lineNumber);
    }
  }

  private bool ValidateFileTrailer(string line, long groupCount, long lineCount)
  {
    (int Code, string[] Row) rowFromString = this.GetRowFromString(line, lineCount);
    long result1;
    if (rowFromString.Code == 99 && rowFromString.Row.Length >= 4 && long.TryParse(rowFromString.Row[2], out result1) && result1 == groupCount)
    {
      long result2;
      if (long.TryParse(rowFromString.Row[3].Trim('/'), out result2))
        return result2 == lineCount;
    }
    return false;
  }

  private bool StartsWithCode(string str, int code)
  {
    return str != null && str.StartsWith(code.ToString("00"));
  }
}
