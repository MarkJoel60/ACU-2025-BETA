// Decompiled with JetBrains decompiler
// Type: PX.Objects.IN.Turnover.TurnoverCalculationArgs
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.IN.Turnover;

public class TurnoverCalculationArgs
{
  public virtual Guid? NoteID { get; set; }

  public virtual int? BranchID { get; set; }

  public virtual string FromPeriodID { get; set; }

  public virtual string ToPeriodID { get; set; }

  public virtual int? SiteID { get; set; }

  public virtual int? ItemClassID { get; set; }

  public virtual int?[] Inventories { get; set; }

  public virtual bool IsFullCalc
  {
    get
    {
      if (this.SiteID.HasValue || this.ItemClassID.HasValue)
        return false;
      int?[] inventories = this.Inventories;
      return (inventories != null ? inventories.Length : 0) == 0;
    }
  }

  public virtual int NumberOfDays { get; set; }
}
