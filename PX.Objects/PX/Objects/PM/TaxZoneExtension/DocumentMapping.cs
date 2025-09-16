// Decompiled with JetBrains decompiler
// Type: PX.Objects.PM.TaxZoneExtension.DocumentMapping
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using System;

#nullable disable
namespace PX.Objects.PM.TaxZoneExtension;

public class DocumentMapping : IBqlMapping
{
  protected Type _table;
  public Type ProjectID = typeof (Document.projectID);

  public Type Extension => typeof (Document);

  public Type Table => this._table;

  public DocumentMapping(Type table) => this._table = table;
}
