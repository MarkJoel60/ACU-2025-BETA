// Decompiled with JetBrains decompiler
// Type: PX.Objects.FS.FSEntityIDExpenseSelectorAttribute
// Assembly: PX.Objects.FS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6B78C88F-1039-47BB-84A6-5486C1B99824
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.FS.xml

using PX.Data;
using PX.Objects.CR;
using PX.Objects.PM;

#nullable disable
namespace PX.Objects.FS;

public class FSEntityIDExpenseSelectorAttribute : FSEntityIDSelectorAttribute
{
  private readonly System.Type baccountBqlField;
  private readonly System.Type projectBqlField;
  private readonly System.Type customerLocationIDBqlField;

  public FSEntityIDExpenseSelectorAttribute(
    System.Type typeBqlField,
    System.Type baccountBqlField,
    System.Type projectBqlField,
    System.Type customerLocationIDBqlField)
    : base(typeBqlField)
  {
    this.baccountBqlField = baccountBqlField;
    this.projectBqlField = projectBqlField;
    this.customerLocationIDBqlField = customerLocationIDBqlField;
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
      search = BqlCommand.Compose(new System.Type[41]
      {
        typeof (Search2<,,>),
        typeof (FSServiceOrder.refNbr),
        typeof (LeftJoin<,>),
        typeof (PX.Objects.CT.Contract),
        typeof (On<,>),
        typeof (PX.Objects.CT.Contract.contractID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (Where<,,>),
        typeof (PMProject.restrictProjectSelect),
        typeof (Equal<>),
        typeof (PMRestrictOption.allProjects),
        typeof (Or<,,>),
        typeof (FSServiceOrder.billCustomerID),
        typeof (Equal<>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (And<,,>),
        typeof (FSServiceOrder.projectID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (And<,,>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (IsNotNull),
        typeof (Or<>),
        typeof (Where<,,>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (IsNull),
        typeof (And<,,>),
        typeof (FSServiceOrder.projectID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (And<,>),
        typeof (PX.Objects.CT.Contract.nonProject),
        typeof (Equal<>),
        typeof (True)
      });
    if (itemType == typeof (FSAppointment))
      search = BqlCommand.Compose(new System.Type[51]
      {
        typeof (Search2<,,>),
        typeof (FSAppointment.refNbr),
        typeof (LeftJoin<,,>),
        typeof (FSServiceOrder),
        typeof (On<,,>),
        typeof (FSServiceOrder.srvOrdType),
        typeof (Equal<>),
        typeof (FSAppointment.srvOrdType),
        typeof (And<,>),
        typeof (FSServiceOrder.refNbr),
        typeof (Equal<>),
        typeof (FSAppointment.soRefNbr),
        typeof (LeftJoin<,>),
        typeof (PX.Objects.CT.Contract),
        typeof (On<,>),
        typeof (PX.Objects.CT.Contract.contractID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (Where<,,>),
        typeof (PMProject.restrictProjectSelect),
        typeof (Equal<>),
        typeof (PMRestrictOption.allProjects),
        typeof (Or<,,>),
        typeof (FSServiceOrder.billCustomerID),
        typeof (Equal<>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (And<,,>),
        typeof (FSAppointment.projectID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (And<,,>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (IsNotNull),
        typeof (Or<>),
        typeof (Where<,,>),
        typeof (FSAppointment.projectID),
        typeof (Equal<>),
        typeof (Current<>),
        this.projectBqlField,
        typeof (And<,,>),
        typeof (Current<>),
        this.baccountBqlField,
        typeof (IsNull),
        typeof (And<,>),
        typeof (PX.Objects.CT.Contract.nonProject),
        typeof (Equal<>),
        typeof (True)
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
