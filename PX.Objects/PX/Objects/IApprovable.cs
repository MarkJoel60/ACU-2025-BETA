// Decompiled with JetBrains decompiler
// Type: PX.Objects.IApprovable
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data.EP;

#nullable disable
namespace PX.Objects;

/// <summary>
/// The interface that specifies that the document is approvable.
/// </summary>
public interface IApprovable : IAssign
{
  bool? Approved { get; set; }

  bool? Rejected { get; set; }

  bool? DontApprove { get; set; }
}
