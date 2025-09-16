// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.ClockInClockOut.DisplayEntityNameAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Data;
using System.Reflection;
using System.Web.Compilation;

#nullable disable
namespace PX.Objects.EP.ClockInClockOut;

[PXInternalUseOnly]
public class DisplayEntityNameAttribute : 
  PXEventSubscriberAttribute,
  IPXRowSelectingSubscriber,
  IPXRowInsertingSubscriber
{
  private System.Type _entityTypeField;

  public DisplayEntityNameAttribute(System.Type entityTypeField)
  {
    this._entityTypeField = entityTypeField;
  }

  void IPXRowSelectingSubscriber.RowSelecting(PXCache sender, PXRowSelectingEventArgs e)
  {
    this.SetDisplayEntityName(sender, e.Row);
  }

  void IPXRowInsertingSubscriber.RowInserting(PXCache sender, PXRowInsertingEventArgs e)
  {
    this.SetDisplayEntityName(sender, e.Row);
  }

  private void SetDisplayEntityName(PXCache sender, object row)
  {
    if (row == null)
      return;
    string typeName = sender.GetValue(row, this._entityTypeField.Name) as string;
    if (string.IsNullOrEmpty(typeName))
      return;
    System.Type type = PXBuildManager.GetType(typeName, false);
    if (!(type != (System.Type) null))
      return;
    string str = type.GetCustomAttribute<PXCacheNameAttribute>(false)?.Name + ":";
    sender.SetValue(row, this._FieldName, (object) str);
  }
}
