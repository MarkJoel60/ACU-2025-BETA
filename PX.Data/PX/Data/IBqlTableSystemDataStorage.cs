// Decompiled with JetBrains decompiler
// Type: PX.Data.IBqlTableSystemDataStorage
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System.ComponentModel;

#nullable disable
namespace PX.Data;

/// <summary>Do not implement this interface directly, instead any DAC class should be inherited from PXBqlTable, for example: class MyDac : PXBqlTable, IBqlTable</summary>
[PXInternalUseOnly]
public interface IBqlTableSystemDataStorage
{
  [PXInternalUseOnly]
  [EditorBrowsable(EditorBrowsableState.Never)]
  ref PXBqlTableSystemData GetBqlTableSystemData();
}
