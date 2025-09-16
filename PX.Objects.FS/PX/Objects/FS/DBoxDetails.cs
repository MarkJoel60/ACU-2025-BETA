// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.DBoxDetails
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Objects.CR;
using System;

#nullable disable
namespace PX.Objects.FS;

public class DBoxDetails
{
  public object sourceLine;

  public virtual Guid? SourceNoteID { get; set; }

  public virtual string LineType { get; set; }

  public virtual int? InventoryID { get; set; }

  public virtual string UOM { get; set; }

  public virtual bool? IsFree { get; set; }

  public virtual string BillingRule { get; set; }

  public virtual string TranDesc { get; set; }

  public virtual int? SiteID { get; set; }

  public virtual int? EstimatedDuration { get; set; }

  public virtual Decimal? EstimatedQty { get; set; }

  public virtual Decimal? CuryUnitPrice { get; set; }

  public virtual bool? ManualPrice { get; set; }

  public virtual int? ProjectID { get; set; }

  public virtual int? ProjectTaskID { get; set; }

  public virtual int? CostCodeID { get; set; }

  public virtual Decimal? CuryUnitCost { get; set; }

  public virtual bool? ManualCost { get; set; }

  public virtual bool? EnablePO { get; set; }

  public virtual int? POVendorID { get; set; }

  public virtual int? POVendorLocationID { get; set; }

  public virtual string TaxCategoryID { get; set; }

  public virtual Decimal? DiscPct { get; set; }

  public virtual Decimal? CuryDiscAmt { get; set; }

  public virtual Decimal? CuryBillableExtPrice { get; set; }

  public virtual bool? POCreate { get; set; }

  public static implicit operator DBoxDetails(CROpportunityProducts line)
  {
    if (line == null)
      return (DBoxDetails) null;
    return new DBoxDetails()
    {
      SourceNoteID = line.NoteID,
      LineType = line.LineType,
      InventoryID = line.InventoryID,
      UOM = line.UOM,
      IsFree = line.IsFree,
      TranDesc = line.Descr,
      SiteID = line.SiteID,
      EstimatedQty = line.Qty,
      CuryUnitPrice = line.CuryUnitPrice,
      ManualPrice = line.ManualPrice,
      ProjectID = line.ProjectID,
      ProjectTaskID = line.TaskID,
      CostCodeID = line.CostCodeID,
      CuryUnitCost = line.CuryUnitCost,
      ManualCost = line.POCreate,
      EnablePO = line.POCreate,
      POVendorID = line.VendorID,
      TaxCategoryID = line.TaxCategoryID,
      DiscPct = line.DiscPct,
      CuryDiscAmt = line.CuryDiscAmt,
      CuryBillableExtPrice = line.CuryExtPrice,
      POCreate = line.POCreate,
      sourceLine = (object) line
    };
  }
}
