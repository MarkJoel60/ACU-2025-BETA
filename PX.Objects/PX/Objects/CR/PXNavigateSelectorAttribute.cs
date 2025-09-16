// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.PXNavigateSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CR;

public class PXNavigateSelectorAttribute : PXSelectorAttribute
{
  public PXNavigateSelectorAttribute(System.Type type)
    : base(type)
  {
  }

  public PXNavigateSelectorAttribute(System.Type type, params System.Type[] fieldList)
    : base(type, fieldList)
  {
  }

  public virtual void FieldVerifying(PXCache sender, PXFieldVerifyingEventArgs e)
  {
  }

  protected virtual bool IsReadDeletedSupported => false;

  protected virtual void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    lock (((ICollection) PXSelectorAttribute._SelectorFields).SyncRoot)
    {
      List<KeyValuePair<string, System.Type>> keyValuePairList;
      if (!PXSelectorAttribute._SelectorFields.TryGetValue(bqlTable, out keyValuePairList) || keyValuePairList.Count <= 0 || !(keyValuePairList[keyValuePairList.Count - 1].Key == ((PXEventSubscriberAttribute) this).FieldName))
        return;
      keyValuePairList.RemoveAt(keyValuePairList.Count - 1);
    }
  }
}
