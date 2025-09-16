// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.CSFilterMaint
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.CS;

public class CSFilterMaint : FilterMaint
{
  [PXDefault(0)]
  [PXDBInt]
  [PXUIField(DisplayName = "Operator")]
  [PXIntList(new int[] {0, 1}, new string[] {"And", "Or"})]
  protected virtual void FilterRow_Operator_CacheAttached(PXCache sender)
  {
  }
}
