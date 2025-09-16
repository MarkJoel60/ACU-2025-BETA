// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEntityIDAPInvoiceSelectorAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;

#nullable disable
namespace PX.Objects.FS;

public class FSEntityIDAPInvoiceSelectorAttribute(System.Type typeBqlField) : 
  FSEntityIDSelectorAttribute(typeBqlField)
{
  public override void FieldUpdating(PXCache sender, PXFieldUpdatingEventArgs e)
  {
    if (sender.Graph.Accessinfo.ScreenID != null && sender.Graph.Accessinfo.ScreenID.Substring(0, 2) == "FS")
      return;
    base.FieldUpdating(sender, e);
  }

  protected override void CreateSelectorView(
    PXGraph graph,
    System.Type itemType,
    PXNoteAttribute noteAtt,
    out string viewName,
    out string[] fieldList,
    out string[] headerList)
  {
    System.Type search = (System.Type) null;
    if (itemType == typeof (FSServiceOrder))
      search = BqlCommand.Compose(new System.Type[11]
      {
        typeof (Search<,>),
        typeof (FSServiceOrder.refNbr),
        typeof (Where<,,>),
        typeof (FSServiceOrder.quote),
        typeof (Equal<False>),
        typeof (And<,,>),
        typeof (FSServiceOrder.hold),
        typeof (Equal<False>),
        typeof (And<,>),
        typeof (FSServiceOrder.canceled),
        typeof (Equal<False>)
      });
    if (itemType == typeof (FSAppointment))
      search = BqlCommand.Compose(new System.Type[8]
      {
        typeof (Search<,>),
        typeof (FSAppointment.refNbr),
        typeof (Where<,,>),
        typeof (FSAppointment.hold),
        typeof (Equal<False>),
        typeof (And<,>),
        typeof (FSAppointment.canceled),
        typeof (Equal<False>)
      });
    if (search != (System.Type) null)
    {
      viewName = EntityIDSelectorAttribute.AddSelectorView(graph, search);
      EntityIDSelectorAttribute.AddFieldView(graph, search.GenericTypeArguments[0]);
      fieldList = (string[]) null;
      headerList = (string[]) null;
    }
    else
      base.CreateSelectorView(graph, itemType, noteAtt, out viewName, out fieldList, out headerList);
  }
}
