// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.IDocumentAdjustment
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Common;

/// <summary>
/// An abstraction that represents an application
/// of one document to another, exposing the adjusting /
/// adjusted documents' primary keys.
/// </summary>
public interface IDocumentAdjustment
{
  string AdjgDocType { get; set; }

  string AdjgRefNbr { get; set; }

  string AdjdDocType { get; set; }

  string AdjdRefNbr { get; set; }

  string Module { get; }
}
