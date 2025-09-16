// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.GraphExtensions.POOrderEntryExt.PODemandSourceInfo
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.PO.GraphExtensions.POOrderEntryExt;

/// <exclude />
[PXVirtual]
[PXHidden]
public class PODemandSourceInfo : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  public (PXErrorLevel Level, 
  #nullable disable
  string Text) Status { get; set; } = ((PXErrorLevel) 1, string.Empty);

  [PXInt]
  public virtual int? BranchID { get; set; }

  [PXDate]
  public virtual DateTime? PurchDate { get; set; }

  [PXString]
  public virtual string POOrderType { get; set; }

  [PXString]
  public virtual string AlternateID { get; set; }

  [PXInt]
  public virtual int? ProjectID { get; set; }

  [PXInt]
  public virtual int? TaskID { get; set; }

  [PXInt]
  public virtual int? CostCodeID { get; set; }

  public abstract class branchID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PODemandSourceInfo.branchID>
  {
  }

  public abstract class purchDate : 
    BqlType<
    #nullable enable
    IBqlDateTime, DateTime>.Field<
    #nullable disable
    PODemandSourceInfo.purchDate>
  {
  }

  public abstract class pOOrderType : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PODemandSourceInfo.pOOrderType>
  {
  }

  public abstract class alternateID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PODemandSourceInfo.alternateID>
  {
  }

  public abstract class projectID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PODemandSourceInfo.projectID>
  {
  }

  public abstract class taskID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PODemandSourceInfo.taskID>
  {
  }

  public abstract class costCodeID : BqlType<
  #nullable enable
  IBqlInt, int>.Field<
  #nullable disable
  PODemandSourceInfo.costCodeID>
  {
  }
}
