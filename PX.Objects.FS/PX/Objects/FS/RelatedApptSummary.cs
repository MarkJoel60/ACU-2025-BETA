// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.RelatedApptSummary
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.FS;

public class RelatedApptSummary
{
  public RelatedApptSummary(int? soDetID)
  {
    this.SODetID = soDetID;
    this.ApptCntr = 0;
    this.ApptCntrIncludingRequestPO = 0;
    this.ApptEstimatedDuration = 0;
    this.ApptEstimatedQty = 0M;
    this.BaseApptEstimatedQty = 0M;
    this.CuryApptEstimatedTranAmt = 0M;
    this.ApptEstimatedTranAmt = 0M;
    this.ApptActualDuration = 0;
    this.ApptActualQty = 0M;
    this.BaseApptActualQty = 0M;
    this.CuryApptActualTranAmt = 0M;
    this.ApptActualTranAmt = 0M;
    this.ApptEffTranDuration = 0;
    this.ApptEffTranQty = 0M;
    this.BaseApptEffTranQty = 0M;
    this.CuryApptEffTranAmt = 0M;
    this.ApptEffTranAmt = 0M;
  }

  public virtual int? SODetID { get; set; }

  public virtual int ApptCntr { get; set; }

  public virtual int ApptCntrIncludingRequestPO { get; set; }

  public virtual int ApptEstimatedDuration { get; set; }

  public virtual Decimal ApptEstimatedQty { get; set; }

  public virtual Decimal BaseApptEstimatedQty { get; set; }

  public virtual Decimal CuryApptEstimatedTranAmt { get; set; }

  public virtual Decimal ApptEstimatedTranAmt { get; set; }

  public virtual int ApptActualDuration { get; set; }

  public virtual Decimal ApptActualQty { get; set; }

  public virtual Decimal BaseApptActualQty { get; set; }

  public virtual Decimal CuryApptActualTranAmt { get; set; }

  public virtual Decimal ApptActualTranAmt { get; set; }

  public virtual int ApptEffTranDuration { get; set; }

  public virtual Decimal ApptEffTranQty { get; set; }

  public virtual Decimal BaseApptEffTranQty { get; set; }

  public virtual Decimal CuryApptEffTranAmt { get; set; }

  public virtual Decimal ApptEffTranAmt { get; set; }

  public virtual PXException ApptCntr_Exception { get; set; }

  public virtual PXException ApptEstimatedDuration_Exception { get; set; }

  public virtual PXException ApptActualDuration_Exception { get; set; }

  public virtual PXException ApptEffTranQty_Exception { get; set; }

  public virtual PXException CuryApptEffTranAmt_Exception { get; set; }
}
