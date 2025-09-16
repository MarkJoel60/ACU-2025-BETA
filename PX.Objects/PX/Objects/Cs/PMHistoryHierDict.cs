// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.PMHistoryHierDict
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.PM;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

internal class PMHistoryHierDict : 
  NestedDictionary<int, string, int, (int ProjectID, int InventoryID), Dictionary<string, PMHistory>>
{
}
