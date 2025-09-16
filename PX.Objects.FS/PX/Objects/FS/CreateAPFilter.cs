// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.CreateAPFilter
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.FS;

[Serializable]
public class CreateAPFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXString(40)]
  [PXUIField(DisplayName = "Related Entity Type", Visible = false)]
  public virtual 
  #nullable disable
  string RelatedEntityType { get; set; }

  [PXGuid]
  [PXUIField(DisplayName = "Related Doc. Nbr.", Visible = false)]
  public virtual Guid? RelatedDocNoteID { get; set; }

  [PXDate]
  [PXUIField(DisplayName = "Related Doc. Date", Visible = false, IsReadOnly = true)]
  public virtual DateTime? RelatedDocDate { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Related Doc. Customer", Visible = false, IsReadOnly = true)]
  public virtual int? RelatedDocCustomerID { get; set; }

  [PXInt]
  [PXUIField(DisplayName = "Related Doc. Customer Location", Enabled = false, Visible = false)]
  public virtual int? RelatedDocCustomerLocationID { get; set; }

  [PXInt]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? RelatedDocProjectID { get; set; }

  [PXInt]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? RelatedDocProjectTaskID { get; set; }

  [PXInt]
  [PXUIField(Enabled = false, Visible = false)]
  public virtual int? RelatedDocCostCodeID { get; set; }

  public abstract class relatedEntityType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    CreateAPFilter.relatedEntityType>
  {
  }

  public abstract class relatedDocNoteID : 
    BqlType<
    #nullable enable
    IBqlGuid, Guid>.Field<
    #nullable disable
    CreateAPFilter.relatedDocNoteID>
  {
  }

  public abstract class relatedDocDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    CreateAPFilter.relatedDocDate>
  {
  }

  public abstract class relatedDocCustomerID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateAPFilter.relatedDocCustomerID>
  {
  }

  public abstract class relatedDocCustomerLocationID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateAPFilter.relatedDocCustomerLocationID>
  {
  }

  public abstract class relatedDocProjectID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateAPFilter.relatedDocProjectID>
  {
  }

  public abstract class relatedDocProjectTaskID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateAPFilter.relatedDocProjectTaskID>
  {
  }

  public abstract class relatedDocCostCodeID : 
    BqlType<
    #nullable enable
    IBqlInt, int>.Field<
    #nullable disable
    CreateAPFilter.relatedDocCostCodeID>
  {
  }
}
