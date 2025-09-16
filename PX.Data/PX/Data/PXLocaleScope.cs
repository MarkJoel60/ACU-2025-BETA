// Decompiled with JetBrains decompiler
// Type: PX.Data.PXLocaleScope
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using PX.Translation;
using System.Globalization;

#nullable disable
namespace PX.Data;

/// <exclude />
public class PXLocaleScope : PXCultureScope
{
  public PXLocaleScope(string localeName)
  {
    if (string.IsNullOrEmpty(localeName))
      return;
    this.PutCulture(new CultureInfo(localeName));
    PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
  }

  public override void Dispose()
  {
    base.Dispose();
    if (this.previousThreadCulture == null)
      return;
    PXContext.SetSlot<PXDictionaryManager>((PXDictionaryManager) null);
  }
}
