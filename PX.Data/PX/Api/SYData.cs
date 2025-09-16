// Decompiled with JetBrains decompiler
// Type: PX.Api.SYData
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Data;
using PX.Data.BQL;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  private static 
  #nullable disable
  Lazy<ISqlDialect> _lazySqlDialect = new Lazy<ISqlDialect>((Func<ISqlDialect>) (() => PXDatabase.Provider.SqlDialect), LazyThreadSafetyMode.PublicationOnly);
  internal static char FIELD_SEPARATOR = SYData.SqlDialect.WildcardFieldSeparatorChar;
  internal static char ERROR_SEPARATOR = SYData.SqlDialect.WildcardErrorSeparatorChar;
  internal static char PARAM_SEPARATOR = SYData.SqlDialect.WildcardParamSeparatorChar;
  private const int errorMessageMaxLength = 4000;
  private const string truncatedPostfix = "...";
  private string _errorMessage;

  private static ISqlDialect SqlDialect => SYData._lazySqlDialect.Value;

  [PXDBGuid(false, IsKey = true)]
  [PXDefault(typeof (SYMapping.mappingID))]
  public Guid? MappingID { get; set; }

  [PXDBInt(IsKey = true)]
  [PXDefault]
  [PXLineNbr(typeof (SYMapping.dataCntr))]
  [PXParent(typeof (Select<SYMapping, Where<SYMapping.mappingID, Equal<Current<SYData.mappingID>>>>))]
  [PXUIField(DisplayName = "Number")]
  public virtual int? LineNbr { get; set; }

  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBBool]
  [PXDefault(false)]
  [PXUIField(DisplayName = "Processed")]
  public virtual bool? IsProcessed { get; set; }

  [PXDBString(4000, IsUnicode = true)]
  [PXUIField(DisplayName = "Error")]
  public string ErrorMessage
  {
    get => this._errorMessage;
    set
    {
      this._errorMessage = value == null || value.Length <= 4000 ? value : value.Substring(0, 4000 - "...".Length) + "...";
    }
  }

  [PXDBText(IsUnicode = true)]
  public string FieldErrors { get; set; }

  [PXDBText(IsUnicode = true)]
  public string FieldExceptions { get; set; }

  [PXDBText(IsUnicode = true)]
  public string FieldValues { get; set; }

  [PXDBText(IsUnicode = true)]
  public string Keys { get; set; }

  [PXString(IsUnicode = true)]
  public string ExtRefNbr { get; set; }

  [PXBool]
  public virtual bool CanAddSubstitutions => !Str.IsNullOrEmpty(this.FieldExceptions);

  [PXNote]
  public virtual Guid? NoteID { get; set; }

  [PXDBCreatedByID]
  public virtual Guid? CreatedByID { get; set; }

  [PXDBCreatedByScreenID]
  public virtual string CreatedByScreenID { get; set; }

  [PXDBCreatedDateTime]
  public virtual System.DateTime? CreatedDateTime { get; set; }

  [PXDBLastModifiedByID]
  public virtual Guid? LastModifiedByID { get; set; }

  [PXDBLastModifiedByScreenID]
  public virtual string LastModifiedByScreenID { get; set; }

  [PXDBLastModifiedDateTime]
  public virtual System.DateTime? LastModifiedDateTime { get; set; }

  [PXDBTimestamp(RecordComesFirst = true)]
  public virtual byte[] TStamp { get; set; }

  internal static int GetFieldLength(string field) => field != null ? field.Length : 0;

  private static string JoinValues(char separator, IEnumerable<string> values)
  {
    return string.Join(separator.ToString(), values);
  }

  private static string[] SplitValues(char separator, string values)
  {
    if (string.IsNullOrEmpty(values))
      return new string[0];
    return values.Split(separator);
  }

  public static string JoinFields(IEnumerable<string> fields)
  {
    return SYData.JoinValues(SYData.FIELD_SEPARATOR, fields);
  }

  public static string[] SplitFields(string fields)
  {
    return SYData.SplitValues(SYData.FIELD_SEPARATOR, fields);
  }

  internal static string JoinErrors(IEnumerable<string> errors)
  {
    return SYData.JoinValues(SYData.ERROR_SEPARATOR, errors);
  }

  internal static string[] SplitErrors(string errors)
  {
    return SYData.SplitValues(SYData.ERROR_SEPARATOR, errors);
  }

  public void WriteKeys(IEnumerable<string> keys) => this.Keys = SYData.JoinFields(keys);

  public string[] ReadKeys() => this.Keys == null ? (string[]) null : SYData.SplitFields(this.Keys);

  public abstract class mappingID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYData.mappingID>
  {
  }

  public abstract class lineNbr : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  SYData.lineNbr>
  {
  }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYData.isActive>
  {
  }

  public abstract class isProcessed : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SYData.isProcessed>
  {
  }

  public abstract class errorMessage : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.errorMessage>
  {
  }

  public abstract class fieldErrors : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.fieldErrors>
  {
  }

  public abstract class fieldExceptions : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.fieldExceptions>
  {
  }

  public abstract class fieldValues : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.fieldValues>
  {
  }

  public abstract class keys : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.keys>
  {
  }

  public abstract class extRefNbr : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYData.extRefNbr>
  {
  }

  public abstract class canAddSubstitutions : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    SYData.canAddSubstitutions>
  {
  }

  public abstract class noteID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYData.noteID>
  {
  }

  public abstract class createdByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYData.createdByID>
  {
  }

  public abstract class createdByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYData.createdByScreenID>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYData.createdDateTime>
  {
  }

  public abstract class lastModifiedByID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SYData.lastModifiedByID>
  {
  }

  public abstract class lastModifiedByScreenID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYData.lastModifiedByScreenID>
  {
  }

  public abstract class lastModifiedDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, System.DateTime>.Field<
    #nullable disable
    SYData.lastModifiedDateTime>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYData.tStamp>
  {
  }
}
