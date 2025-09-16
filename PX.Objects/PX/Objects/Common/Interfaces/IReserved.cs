// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Interfaces.IReserved
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Common.Interfaces;

/// <summary>
/// The interface that is used for documents that can have the Reserved status. The interface is used in the approval process.
/// </summary>
public interface IReserved
{
  bool? Hold { get; set; }

  bool? Released { get; set; }
}
