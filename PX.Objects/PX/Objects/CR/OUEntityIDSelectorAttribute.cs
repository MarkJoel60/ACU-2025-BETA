// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.OUEntityIDSelectorAttribute
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.EP;
using System;

#nullable disable
namespace PX.Objects.CR;

[Obsolete]
public class OUEntityIDSelectorAttribute : PXEntityIDSelectorAttribute
{
  private readonly System.Type contactBqlField;
  private readonly System.Type baccountBqlField;

  [Obsolete]
  public OUEntityIDSelectorAttribute(
    System.Type typeBqlField,
    System.Type contactBqlField,
    System.Type baccountBqlField)
    : base(typeBqlField)
  {
    this.contactBqlField = contactBqlField;
    this.baccountBqlField = baccountBqlField;
  }

  public virtual void FieldSelecting(PXCache sender, PXFieldSelectingEventArgs e)
  {
    base.FieldSelecting(sender, e);
    if (!(e.ReturnState is PXFieldState returnState))
      return;
    returnState.DescriptionName = "Description";
  }

  protected virtual void CreateSelectorView(
    PXGraph graph,
    System.Type itemType,
    PXNoteAttribute noteAtt,
    out string viewName,
    out string[] fieldList,
    out string[] headerList)
  {
    System.Type type = (System.Type) null;
    if (itemType == typeof (CRCase))
      type = BqlCommand.Compose(new System.Type[12]
      {
        typeof (Search<,>),
        typeof (CRCase.caseCD),
        typeof (Where<,,>),
        typeof (CRCase.contactID),
        typeof (Equal<>),
        typeof (Current<>),
        this.contactBqlField,
        typeof (Or<,>),
        typeof (CRCase.customerID),
        typeof (Equal<>),
        typeof (Current<>),
        this.baccountBqlField
      });
    if (itemType == typeof (CROpportunity))
      type = BqlCommand.Compose(new System.Type[12]
      {
        typeof (Search<,>),
        typeof (CROpportunity.opportunityID),
        typeof (Where<,,>),
        typeof (CROpportunity.contactID),
        typeof (Equal<>),
        typeof (Current<>),
        this.contactBqlField,
        typeof (Or<,>),
        typeof (CROpportunity.bAccountID),
        typeof (Equal<>),
        typeof (Current<>),
        this.baccountBqlField
      });
    if (type != (System.Type) null)
    {
      viewName = PXEntityIDSelectorAttribute.AddSelectorView(graph, type);
      PXFieldState pxFieldState = PXEntityIDSelectorAttribute.AddFieldView(graph, type.GenericTypeArguments[0]);
      fieldList = pxFieldState.FieldList;
      headerList = pxFieldState.HeaderList;
    }
    else
      base.CreateSelectorView(graph, itemType, noteAtt, ref viewName, ref fieldList, ref headerList);
  }
}
