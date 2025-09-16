// Decompiled with JetBrains decompiler
// Type: PX.Objects.GL.GLHistoryFilter
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.Objects.GL;

[PXHidden]
public class GLHistoryFilter : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PX.Objects.GL.FinPeriodID(null, null, null, null, null, null, true, false, null, null, null, true, true)]
  public virtual 
  #nullable disable
  string FinPeriodID { get; set; }

  public abstract class finPeriodID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  GLHistoryFilter.finPeriodID>
  {
  }
}
