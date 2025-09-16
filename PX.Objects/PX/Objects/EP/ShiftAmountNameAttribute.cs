// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ShiftAmountNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.EP;

public class ShiftAmountNameAttribute : PXUIFieldAttribute
{
  public virtual void CacheAttached(PXCache sender)
  {
    this.DisplayName = PXAccess.FeatureInstalled<FeaturesSet.payrollModule>() ? "Costing Amount" : "Amount";
    base.CacheAttached(sender);
  }
}
