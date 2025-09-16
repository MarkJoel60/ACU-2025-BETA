// Decompiled with JetBrains decompiler
// Type: PX.Data.Api.Export.SyImport.ViewSelectResults
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Data.Api.Export.SyImport;

public class ViewSelectResults : List<NativeRowWrapper>
{
  public readonly string ViewId;
  private int _revision;

  public ViewSelectResults(string viewId) => this.ViewId = viewId;

  public void AddRow(object result) => this.Add(new NativeRowWrapper(this, result));

  public void AddCell(string fieldId, object v)
  {
    this.Last<NativeRowWrapper>().FieldValues.Add(new FieldValue()
    {
      FieldId = fieldId,
      Value = v,
      ViewId = this.ViewId
    });
  }

  public void IncrementRevision()
  {
    ViewSelectResults newSelect = new ViewSelectResults(this.ViewId)
    {
      _revision = this._revision + 1
    };
    foreach (NativeRowWrapper nativeRowWrapper in (List<NativeRowWrapper>) this)
    {
      nativeRowWrapper.ChangeSelect(newSelect);
      newSelect.Add(nativeRowWrapper);
    }
  }
}
