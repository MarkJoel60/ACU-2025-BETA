// Decompiled with JetBrains decompiler
// Type: PX.Data.IDataScreenFactory
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data;

/// <exclude />
public interface IDataScreenFactory
{
  DataScreenBase CreateDataScreen(string screenID);

  DataScreenBase CreateDataScreen(string screenID, PXGraph graph);
}
