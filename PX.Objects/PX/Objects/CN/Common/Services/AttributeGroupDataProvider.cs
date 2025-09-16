// Decompiled with JetBrains decompiler
// Type: PX.Objects.CN.Common.Services.AttributeGroupDataProvider
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.CS;
using PX.Data;
using PX.Objects.CS;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace PX.Objects.CN.Common.Services;

/// <summary>Adjusts UI controls for common attributes.</summary>
public class AttributeGroupDataProvider
{
  private readonly PXGraph graph;

  public AttributeGroupDataProvider(PXGraph graph) => this.graph = graph;

  /// <summary>
  /// Configures the type of control for default value field on adding an attribute. Should be used on
  /// FieldSelecting event for <see cref="T:PX.Objects.CS.CSAttributeGroup.defaultValue" />.
  /// </summary>
  public PXFieldState GetNewReturnState(object returnState, CSAttributeGroup attributeGroup)
  {
    CSAttribute source = PXResultset<CSAttribute>.op_Implicit(((PXSelectBase<CSAttribute>) new PXSelect<CSAttribute>(this.graph)).Search<CSAttribute.attributeID>((object) attributeGroup.AttributeID, Array.Empty<object>()));
    IEnumerable<CSAttributeDetail> firstTableItems = PXSelectBase<CSAttributeDetail, PXSelect<CSAttributeDetail, Where<CSAttributeDetail.attributeID, Equal<Required<CSAttributeGroup.attributeID>>>, OrderBy<Asc<CSAttributeDetail.sortOrder>>>.Config>.Select(this.graph, new object[1]
    {
      (object) attributeGroup.AttributeID
    }).FirstTableItems;
    return KeyValueHelper.MakeFieldState(source == null ? (KeyValueHelper.Attribute) null : new KeyValueHelper.Attribute(new CSAttribute().PopulateFrom<CSAttribute, CSAttribute>(source), firstTableItems.Select<CSAttributeDetail, CSAttributeDetail>((Func<CSAttributeDetail, CSAttributeDetail>) (o => new CSAttributeDetail().PopulateFrom<CSAttributeDetail, CSAttributeDetail>(o)))), "Default Value", returnState, new int?(attributeGroup.Required.GetValueOrDefault() ? 1 : -1), (string) null, (string) null);
  }
}
