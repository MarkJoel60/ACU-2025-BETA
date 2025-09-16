// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXViewSavedDetailsButtonAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.SM;

#nullable disable
namespace PX.Objects.CR;

public class PXViewSavedDetailsButtonAttribute : PXViewDetailsButtonAttribute
{
  public PXViewSavedDetailsButtonAttribute(System.Type primaryType)
    : base(primaryType)
  {
  }

  public PXViewSavedDetailsButtonAttribute(System.Type primaryType, System.Type select)
    : base(primaryType, select)
  {
  }

  protected virtual void Redirect(PXAdapter adapter, object record)
  {
    PXEntryStatus status = adapter.View.Graph.Caches[record.GetType()].GetStatus(record);
    if (status == 3 || status == 2 || status == 1)
      return;
    base.Redirect(adapter, record);
  }
}
