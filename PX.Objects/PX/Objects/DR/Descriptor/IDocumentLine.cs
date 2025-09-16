// Decompiled with JetBrains decompiler
// Type: PX.Objects.DR.Descriptor.IDocumentLine
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.CM;

#nullable disable
namespace PX.Objects.DR.Descriptor;

/// <summary>
/// Represents a line of an AR / AP document,
/// in parts relevant to Deferred Revenue.
/// </summary>
public interface IDocumentLine : IDocumentTran
{
  /// <summary>
  /// The module of the source document
  /// should either be <see cref="F:PX.Objects.GL.BatchModule.AR" />
  /// or <see cref="F:PX.Objects.GL.BatchModule.AP" />.
  /// </summary>
  string Module { get; }

  string DeferredCode { get; }

  int? BranchID { get; }
}
