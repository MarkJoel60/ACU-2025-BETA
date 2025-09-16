// Decompiled with JetBrains decompiler
// Type: PX.SM.PXFieldsAttribute
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.SM;

public class PXFieldsAttribute : PXStringListAttribute
{
  private readonly System.Type _ScreenID;
  private readonly System.Type _View;

  [InjectDependency]
  private IScreenInfoProvider ScreenInfoProvider { get; set; }

  public PXFieldsAttribute(System.Type srceenID, System.Type view)
    : base(new string[0], new string[0])
  {
    this._ScreenID = srceenID;
    this._View = view;
    this._ExclusiveValues = false;
  }

  public override void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    string screenId = this.GetValue(sender.Graph, this._ScreenID) as string;
    string viewName = this.GetValue(sender.Graph, this._View) as string;
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    if (screenId != null)
    {
      stringList1.Add("*");
      stringList2.Add("*");
      PXViewDescription pxViewDescription = this.ScreenInfoProvider.Get(screenId).Containers.Values.FirstOrDefault<PXViewDescription>((Func<PXViewDescription, bool>) (it => it.ViewName.Equals(viewName, StringComparison.OrdinalIgnoreCase)));
      if (pxViewDescription != null)
      {
        foreach (PX.Data.Description.FieldInfo allField in pxViewDescription.AllFields)
        {
          stringList2.Add(allField.FieldName);
          stringList1.Add(allField.DisplayName);
        }
      }
    }
    PXStringListAttribute.SetList(sender, e.Row, this._FieldName, stringList2.ToArray(), stringList1.ToArray());
    base.FieldSelecting(sender, e);
  }

  private object GetValue(PXGraph graph, System.Type type)
  {
    PXCache cach = graph.Caches[type.DeclaringType];
    return cach.Current != null ? cach.GetValue(cach.Current, type.Name) : (object) null;
  }
}
