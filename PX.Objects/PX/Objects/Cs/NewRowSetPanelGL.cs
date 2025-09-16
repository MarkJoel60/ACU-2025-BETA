// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.NewRowSetPanelGL
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.CS;
using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Objects.CS;

/// <summary>New Row Set panel with default for GL row set</summary>
[PXPrimaryGraph(typeof (RMRowSetMaint))]
[Serializable]
public sealed class NewRowSetPanelGL : PXCacheExtension<
#nullable disable
RMNewRowSetPanel>
{
  public static bool IsActive() => true;

  /// <summary>Type of Row Set</summary>
  [PXString(2, IsFixed = true)]
  [RMType.List]
  [PXDefault("GL")]
  [PXUIField]
  public string Type { get; set; }

  public abstract class type : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  NewRowSetPanelGL.type>
  {
  }
}
