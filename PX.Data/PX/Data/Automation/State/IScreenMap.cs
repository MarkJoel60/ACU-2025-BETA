// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.State.IScreenMap
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Process.Automation.State;
using System.Collections.Generic;

#nullable disable
namespace PX.Data.Automation.State;

internal interface IScreenMap
{
  Screen GetByScreen(string screenID);

  Screen GetByGraph(string graphName);

  bool ContainsGraph(string graphName);

  IEnumerable<ScreenTableField> GetAllScreenFields(string tableName, string field);
}
