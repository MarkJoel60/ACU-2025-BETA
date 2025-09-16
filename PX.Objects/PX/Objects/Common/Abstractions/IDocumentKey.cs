// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Abstractions.IDocumentKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

#nullable disable
namespace PX.Objects.Common.Abstractions;

/// <summary>
/// Abstracts an entity identified by the document type and reference number,
/// e.g. an <see cref="T:PX.Objects.AR.ARRegister" /> or <see cref="T:PX.Objects.AP.APRegister" />.
/// </summary>
public interface IDocumentKey
{
  string DocType { get; set; }

  string RefNbr { get; set; }
}
