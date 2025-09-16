// Decompiled with JetBrains decompiler
// Type: PX.Objects.PO.DAC.ReportParameters
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.PO.DAC;

public class ReportParameters : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [POOpenPeriod(typeof (AccessInfo.businessDate), null, null, null, null, null, true, false, null)]
  public 
  #nullable disable
  string FinPeriodID { get; set; }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  ReportParameters.finPeriodID>
  {
  }
}
