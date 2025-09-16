// Decompiled with JetBrains decompiler
// Type: PX.SM.PXViewsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using System.Collections.Generic;

#nullable disable
namespace PX.SM;

/// <exclude />
public class PXViewsAttribute : PXStringListAttribute
{
  private readonly System.Type _ScreenID;

  public PXViewsAttribute(System.Type srceenID)
    : base(new string[0], new string[0])
  {
    this._ScreenID = srceenID;
    this._ExclusiveValues = false;
  }

  [InjectDependencyOnTypeLevel]
  private IPXPageIndexingService PageIndexingService { get; set; }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    if (this._AllowedValues.Length == 0)
    {
      PXCache cach = sender.Graph.Caches[this._ScreenID.DeclaringType];
      string screenID;
      if (string.IsNullOrEmpty(screenID = cach.GetValue(cach.Current, this._ScreenID.Name) as string))
        return;
      string graphTypeByScreenId = this.PageIndexingService.GetGraphTypeByScreenID(screenID);
      List<string> stringList1 = new List<string>();
      List<string> stringList2 = new List<string>();
      foreach (PXViewInfo graphView in GraphHelper.GetGraphViews(graphTypeByScreenId, true))
      {
        stringList2.Add(graphView.Name);
        stringList1.Add(graphView.DisplayName);
      }
      PXStringListAttribute.SetList(sender, (object) null, this._FieldName, stringList2.ToArray(), stringList1.ToArray());
    }
    base.FieldSelecting(sender, e);
  }
}
