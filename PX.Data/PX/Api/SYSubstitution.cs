// Decompiled with JetBrains decompiler
// Type: PX.Api.SYSubstitution
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

[PXPrimaryGraph(typeof (SYSubstitutionMaint))]
[Serializable]
public class SYSubstitution : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault]
  [PXDBString(25, IsKey = true, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Substitution List")]
  [PXSelector(typeof (SYSubstitution.substitutionID))]
  public 
  #nullable disable
  string SubstitutionID { get; set; }

  [PXDBString(150, IsUnicode = false, InputMask = "")]
  [PXUIField(DisplayName = "Table Name")]
  [PXTablesSelector]
  public virtual string TableName { get; set; }

  [PXDBString(150, IsUnicode = false, InputMask = "")]
  [PXUIField(DisplayName = "Field Name")]
  public virtual string FieldName { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public abstract class substitutionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitution.substitutionID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYSubstitution.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SYSubstitution.fieldName>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYSubstitution.tStamp>
  {
  }
}
