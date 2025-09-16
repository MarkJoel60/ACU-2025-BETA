// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.NumberingSequenceUsage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CS;

#nullable disable
namespace PX.Objects.CN.Common.Services;

public class NumberingSequenceUsage : INumberingSequenceUsage
{
  public void CheckForNumberingUsage<TSetup, TNumberingId>(
    Numbering numbering,
    PXGraph graph,
    string message)
    where TSetup : class, IBqlTable, new()
  {
    if (numbering == null)
      return;
    PXSelect<TSetup> setupSelect = new PXSelect<TSetup>(graph);
    string name = typeof (TNumberingId).Name;
    if (NumberingSequenceUsage.IsNumberingInUse((PXSelectBase) setupSelect, name, numbering))
    {
      string displayName = PXUIFieldAttribute.GetDisplayName(((PXSelectBase) setupSelect).Cache, name);
      throw new PXException("This numbering sequence cannot be deleted. It is used on the '{0}' form, in the '{1}' box.", new object[2]
      {
        (object) message,
        (object) displayName
      });
    }
  }

  private static bool IsNumberingInUse(
    PXSelectBase setupSelect,
    string numberingType,
    Numbering numbering)
  {
    object obj = setupSelect.View.SelectSingle(new object[1]
    {
      (object) numbering.NumberingID
    });
    return obj != null && (string) setupSelect.Cache.GetValue(obj, numberingType) == numbering.NumberingID;
  }
}
