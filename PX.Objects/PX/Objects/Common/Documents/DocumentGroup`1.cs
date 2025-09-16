// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Documents.DocumentGroup`1
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Objects.Common.Documents;

public class DocumentGroup<TDocument>
{
  public string Module { get; set; }

  public string DocumentType { get; set; }

  public IDictionary<string, TDocument> DocumentsByRefNbr { get; set; }
}
