// Decompiled with JetBrains decompiler
// Type: PX.Objects.GDPR.SMPersonalData
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.GDPR;

[PXHidden]
[Serializable]
public class SMPersonalData : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXBool]
  [PXDefault(false)]
  [PXUIField]
  public virtual bool? Selected { get; set; }

  [PXDBString(100, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Table name")]
  public virtual 
  #nullable disable
  string Table { get; set; }

  [PXDBString(100, IsKey = true, IsUnicode = true)]
  [PXUIField(DisplayName = "Field name")]
  public virtual string Field { get; set; }

  [PXDBGuid(false, IsKey = true)]
  public virtual Guid? EntityID { get; set; }

  [PXDBGuid(false)]
  public virtual Guid? TopParentNoteID { get; set; }

  [PXDBString(100, IsUnicode = true)]
  [PXUIField(DisplayName = "Value")]
  public virtual string Value { get; set; }

  [PXDBCreatedDateTime]
  [PXUIField(DisplayName = "Created On", Enabled = false, IsReadOnly = true)]
  public virtual DateTime? CreatedDateTime { get; set; }

  public abstract class selected : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  SMPersonalData.selected>
  {
  }

  public abstract class table : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalData.table>
  {
  }

  public abstract class field : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalData.field>
  {
  }

  public abstract class entityID : BqlType<
  #nullable enable
  IBqlGuid, Guid>.Field<
  #nullable disable
  SMPersonalData.entityID>
  {
  }

  public abstract class topParentNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    SMPersonalData.topParentNoteID>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  SMPersonalData.value>
  {
  }

  public abstract class createdDateTime : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    SMPersonalData.createdDateTime>
  {
  }
}
