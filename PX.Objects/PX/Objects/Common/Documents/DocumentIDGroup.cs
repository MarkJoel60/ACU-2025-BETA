// Decompiled with JetBrains decompiler
// Type: PX.Objects.Common.Documents.DocumentIDGroup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Objects.Common.Extensions;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.Common.Documents;

public class DocumentIDGroup
{
  public string Module { get; set; }

  public string DocumentType
  {
    get => this.DocumentTypes.SingleOrDefault<string>();
    set => this.DocumentTypes = value.SingleToList<string>();
  }

  public List<string> DocumentTypes { get; set; }

  public List<string> RefNbrs { get; set; }

  public DocumentIDGroup()
  {
    this.DocumentTypes = new List<string>();
    this.RefNbrs = new List<string>();
  }
}
