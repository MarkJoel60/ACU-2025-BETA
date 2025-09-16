// Decompiled with JetBrains decompiler
// Type: PX.Data.PXUserAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Metadata;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Data;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Parameter)]
public class PXUserAttribute : PXEventSubscriberAttribute
{
  public static Dictionary<System.Type, List<string>> FieldList = new Dictionary<System.Type, List<string>>();

  protected internal override void SetBqlTable(System.Type bqlTable)
  {
    base.SetBqlTable(bqlTable);
    lock (((ICollection) PXUserAttribute.FieldList).SyncRoot)
    {
      List<string> stringList;
      if (!PXUserAttribute.FieldList.TryGetValue(bqlTable, out stringList))
        PXUserAttribute.FieldList[bqlTable] = stringList = new List<string>();
      if (stringList.Contains(this.FieldName))
        return;
      stringList.Add(this.FieldName);
    }
  }

  /// <exclude />
  public static List<string> GetFields(System.Type table)
  {
    DacMetadata.InitializationCompleted.Wait();
    List<string> stringList;
    return PXUserAttribute.FieldList.TryGetValue(table, out stringList) ? stringList : new List<string>();
  }
}
