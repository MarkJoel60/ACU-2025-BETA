// Decompiled with JetBrains decompiler
// Type: PX.Objects.CT.CTDataNavigator
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common.Parser;
using PX.Reports.Data;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace PX.Objects.CT;

public class CTDataNavigator : IDataNavigator, ICloneable
{
  protected List<CTFormulaDescriptionContainer> list;
  protected IContractInformation engine;

  public CTDataNavigator(IContractInformation engine, List<CTFormulaDescriptionContainer> list)
  {
    this.engine = engine;
    this.list = list;
  }

  public void Clear()
  {
  }

  public void Refresh()
  {
  }

  public object Current => throw new NotImplementedException();

  public IDataNavigator GetChildNavigator(object record) => (IDataNavigator) null;

  public object GetItem(object dataItem, string dataField) => throw new NotImplementedException();

  public IList GetList() => (IList) this.list;

  public object GetValue(object dataItem, string dataField, ref string format, bool valueOnly = false)
  {
    CTNameNode ctNameNode = new CTNameNode((ExpressionNode) null, dataField, (ParserContext) null);
    return ctNameNode.IsAttribute ? this.engine.Evaluate(ctNameNode.ObjectName, (string) null, ctNameNode.FieldName, (CTFormulaDescriptionContainer) dataItem) : this.engine.Evaluate(ctNameNode.ObjectName, ctNameNode.FieldName, (string) null, (CTFormulaDescriptionContainer) dataItem);
  }

  public bool MoveNext() => throw new NotImplementedException();

  public void Reset() => throw new NotImplementedException();

  public ReportSelectArguments SelectArguments => throw new NotImplementedException();

  public object this[string dataField] => throw new NotImplementedException();

  public string CurrentlyProcessingParam
  {
    get => throw new NotImplementedException();
    set => throw new NotImplementedException();
  }

  public int[] GetFieldSegments(string field) => throw new NotImplementedException();

  public object Clone()
  {
    return (object) new CTDataNavigator(this.engine, this.list == null ? (List<CTFormulaDescriptionContainer>) null : new List<CTFormulaDescriptionContainer>((IEnumerable<CTFormulaDescriptionContainer>) this.list));
  }
}
