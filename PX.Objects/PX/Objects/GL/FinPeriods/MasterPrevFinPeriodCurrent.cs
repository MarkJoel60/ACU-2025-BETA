// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.FinPeriods.MasterPrevFinPeriodCurrent
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL.FinPeriods;

[PXProjection(typeof (Select4<MasterFinPeriod, Where<MasterFinPeriod.finPeriodID, Less<CurrentValue<GLHistoryFilter.finPeriodID>>>, Aggregate<Max<MasterFinPeriod.finPeriodID>>>))]
[PXCacheName("Last MasterFinPeriod Current")]
public class MasterPrevFinPeriodCurrent : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(6, IsKey = true, IsFixed = true, BqlField = typeof (MasterFinPeriod.finPeriodID))]
  [FinPeriodIDFormatting]
  public virtual 
  #nullable disable
  string PrevFinPeriodID { get; set; }

  public abstract class prevFinPeriodID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    MasterPrevFinPeriodCurrent.prevFinPeriodID>
  {
  }
}
