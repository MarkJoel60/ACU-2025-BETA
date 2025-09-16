// Decompiled with JetBrains decompiler
// Type: PX.Api.SYSubstitutionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.Maintenance.GI;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYSubstitutionInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXString(25, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Substitution List", Enabled = false)]
  public 
  #nullable disable
  string SubstitutionID { get; set; }

  [PXString(150, IsKey = true, IsUnicode = false, InputMask = "")]
  [PXUIField(DisplayName = "Table Name")]
  [PXTablesSelector]
  public virtual string TableName { get; set; }

  [PXString(150, IsKey = true, IsUnicode = false, InputMask = "")]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDefault]
  [PXString(150, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Original Value", Enabled = false)]
  public string OriginalValue { get; set; }

  [PXDefault]
  [PXString(150, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Substitution Value")]
  public string SubstitutedValue { get; set; }

  public string Serialize()
  {
    return string.Join(SYData.PARAM_SEPARATOR.ToString(), this.SubstitutionID, this.TableName, this.FieldName, this.OriginalValue, this.SubstitutedValue);
  }

  public override string ToString() => this.Serialize();

  public static SYSubstitutionInfo Deserialize(string serialized)
  {
    string[] strArray = serialized.Split(SYData.PARAM_SEPARATOR);
    return new SYSubstitutionInfo()
    {
      SubstitutionID = strArray[0],
      TableName = strArray[1],
      FieldName = strArray[2],
      OriginalValue = strArray[3],
      SubstitutedValue = SYSubstitutionInfo.Nullify(strArray[4])
    };
  }

  private static string Nullify(string str) => !(str == string.Empty) ? str : (string) null;

  public abstract class substitutionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionInfo.substitutionID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYSubstitutionInfo.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYSubstitutionInfo.fieldName>
  {
  }

  public abstract class originalValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionInfo.originalValue>
  {
  }

  public abstract class substitutedValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionInfo.substitutedValue>
  {
  }
}
