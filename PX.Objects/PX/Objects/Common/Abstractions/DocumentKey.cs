// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Abstractions.DocumentKey
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.Common.Abstractions;

public class DocumentKey : Tuple<string, string>
{
  public DocumentKey(IDocumentKey record)
    : base(record.DocType, record.RefNbr)
  {
  }

  public DocumentKey(string docType, string refNbr)
    : base(docType, refNbr)
  {
  }

  public string DocType => this.Item1;

  public string RefNbr => this.Item2;
}
