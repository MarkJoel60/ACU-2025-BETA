// Decompiled with JetBrains decompiler
// Type: PX.Data.IPXResultset
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.Collections;

#nullable disable
namespace PX.Data;

[PXInternalUseOnly]
public interface IPXResultset : IPXResultsetBase, IEnumerable
{
  System.Type GetItemType(int i);

  object GetItem(int rowNbr, int i);

  int GetTableCount();

  int GetRowCount();

  System.Type GetCollectionType();
}
