// Decompiled with JetBrains decompiler
// Type: PX.SM.AUAuditField
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class AUAuditField : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(true)]
  [PXUIField(DisplayName = "Active")]
  public virtual bool? IsActive { get; set; }

  [PXDBString(8, IsFixed = true, InputMask = "CC.CC.CC.CC", IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Screen Name", Visibility = PXUIVisibility.SelectorVisible)]
  [PXParent(typeof (Select<AUAuditTable, Where<AUAuditTable.screenID, Equal<Current<AUAuditField.screenID>>, And<AUAuditTable.tableName, Equal<Current<AUAuditField.tableName>>>>>))]
  public virtual 
  #nullable disable
  string ScreenID { get; set; }

  [PXDBString(100, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Table Name")]
  public virtual string TableName { get; set; }

  [PXDBString(100, IsKey = true)]
  [PXDefault]
  [PXUIField(DisplayName = "Field Name", Visible = false, Enabled = false)]
  public virtual string FieldName { get; set; }

  [PXBool]
  public virtual bool? IsInserted { get; set; }

  [PXInt]
  public virtual int? FieldType { get; set; }

  [PXString]
  [PXUIField(DisplayName = "Field", Enabled = false)]
  public virtual string FieldDisplayName { get; set; }

  public abstract class isActive : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUAuditField.isActive>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditField.screenID>
  {
  }

  public abstract class tableName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditField.tableName>
  {
  }

  public abstract class fieldName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  AUAuditField.fieldName>
  {
  }

  public abstract class isInserted : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  AUAuditField.isInserted>
  {
  }

  public abstract class fieldType : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  AUAuditField.fieldType>
  {
  }

  public abstract class fieldDisplayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    AUAuditField.fieldDisplayName>
  {
  }
}
