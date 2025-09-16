// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.DAC.ReportParameters.DRReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.DR.DAC.ReportParameters;

public class DRReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBBool]
  [PXDefault(typeof (Search<DRSetup.pendingRevenueValidate>))]
  public virtual bool? PendingRevenueValidate { get; set; }

  [PXDBBool]
  [PXDefault(typeof (Search<DRSetup.pendingExpenseValidate>))]
  public virtual bool? PendingExpenseValidate { get; set; }

  public abstract class pendingRevenueValidate : 
    BqlType<IBqlBool, bool>.Field<
    #nullable disable
    DRReportParameters.pendingRevenueValidate>
  {
  }

  public abstract class pendingExpenseValidate : 
    BqlType<
    #nullable enable
    IBqlBool, bool>.Field<
    #nullable disable
    DRReportParameters.pendingExpenseValidate>
  {
  }
}
