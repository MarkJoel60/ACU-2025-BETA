// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.GLHistoryHierDict
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Objects.GL;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CS;

internal class GLHistoryHierDict : 
  NestedDictionary<int, int, (int BranchID, int LedgerID), Dictionary<string, GLHistory>>
{
}
