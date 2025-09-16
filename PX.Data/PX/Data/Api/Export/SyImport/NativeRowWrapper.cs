// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.NativeRowWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

public class NativeRowWrapper
{
  public readonly List<FieldValue> FieldValues = new List<FieldValue>();
  public readonly IBqlTable BqlTable;
  public readonly object Result;
  public string Path;

  public string ViewId => this.Select.ViewId;

  public ViewSelectResults Select { get; private set; }

  public NativeRowWrapper(ViewSelectResults select, object result)
  {
    this.Select = select;
    this.Result = result;
    if (result is PXResult)
      result = (object) (IBqlTable) ((PXResult) result)[0];
    this.BqlTable = (IBqlTable) result;
  }

  public void ChangeSelect(ViewSelectResults newSelect) => this.Select = newSelect;

  public NativeRowWrapper Clone()
  {
    NativeRowWrapper nativeRowWrapper = new NativeRowWrapper(this.Select, this.Result);
    foreach (FieldValue fieldValue in this.FieldValues)
      nativeRowWrapper.FieldValues.Add(fieldValue);
    nativeRowWrapper.Path = this.Path;
    return nativeRowWrapper;
  }
}
