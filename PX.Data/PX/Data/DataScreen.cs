// Decompiled with JetBrains decompiler
// Type: PX.Data.DataScreen
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api;
using PX.Data.Description;
using PX.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Compilation;

#nullable disable
namespace PX.Data;

/// <exclude />
internal class DataScreen : DataScreenBase
{
  private readonly string _graphName;
  private readonly IEnumerable<InqField> _fields;
  private readonly IEnumerable<InqField> _parameters;

  public DataScreen(string screenID)
    : this(screenID, (PXGraph) null)
  {
  }

  public DataScreen(string screenID, PXGraph graph)
    : base(screenID, graph)
  {
    PXSiteMap.ScreenInfo screenInfo = ScreenUtils.ScreenInfo.TryGet(this.ScreenID);
    if (screenInfo == null)
      throw new PXException("This form cannot be automated.");
    PXViewDescription pxViewDescription1;
    int num1 = !screenInfo.Containers.TryGetValue(screenInfo.PrimaryView, out pxViewDescription1) ? 0 : (pxViewDescription1 != null ? 1 : 0);
    bool flag = num1 == 0;
    IEnumerable<PX.Data.Description.FieldInfo> source1 = Enumerable.Empty<PX.Data.Description.FieldInfo>();
    IEnumerable<PX.Data.Description.FieldInfo> source2 = Enumerable.Empty<PX.Data.Description.FieldInfo>();
    if (num1 != 0)
    {
      int num2 = ((IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription1.AllFields).Any<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => f.IsKey)) ? 1 : 0;
      flag = num2 == 0 || pxViewDescription1.IsGrid;
      if (num2 == 0)
      {
        source2 = (IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription1.AllFields;
        this.ParametersViewName = pxViewDescription1.ViewName;
      }
    }
    if (flag)
    {
      PXViewDescription pxViewDescription2 = screenInfo.Containers.Values.FirstOrDefault<PXViewDescription>((Func<PXViewDescription, bool>) (c => c.IsGrid));
      source1 = pxViewDescription2 != null ? (IEnumerable<PX.Data.Description.FieldInfo>) pxViewDescription2.Fields : throw new PXException("Screen '{0}' is not an inquiry screen.", new object[1]
      {
        (object) screenID
      });
      this.ViewName = pxViewDescription2.ViewName;
      this.DefaultAction = pxViewDescription2.PXGridDefaultAction;
    }
    this._graphName = screenInfo.GraphName;
    this._fields = (IEnumerable<InqField>) source1.Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => !string.Equals("Selected", f.FieldName, StringComparison.OrdinalIgnoreCase))).Select<PX.Data.Description.FieldInfo, InqField>(new Func<PX.Data.Description.FieldInfo, InqField>(this.ConvertField)).ToArray<InqField>();
    this._parameters = (IEnumerable<InqField>) source2.Where<PX.Data.Description.FieldInfo>((Func<PX.Data.Description.FieldInfo, bool>) (f => !f.IsEditorControlDisabled)).Select<PX.Data.Description.FieldInfo, InqField>(new Func<PX.Data.Description.FieldInfo, InqField>(this.ConvertField)).ToArray<InqField>();
  }

  protected override PXGraph InstantiateGraph()
  {
    System.Type type = PXBuildManager.GetType(this._graphName, true);
    using (new PXPreserveScope())
      return PXGraph.CreateInstance(type);
  }

  public override string DefaultAction { get; }

  public override string ViewName { get; }

  public override string ParametersViewName { get; }

  public override IEnumerable<InqField> GetFields() => this._fields;

  public override IEnumerable<InqField> GetParameters() => this._parameters;

  private InqField ConvertField(PX.Data.Description.FieldInfo fieldInfo)
  {
    return new InqField()
    {
      Name = fieldInfo.FieldName,
      DisplayName = fieldInfo.DisplayName,
      FieldType = fieldInfo.FieldType,
      Visible = new bool?(!fieldInfo.Invisible),
      Enabled = new bool?(fieldInfo.IsEnabled),
      LinkCommand = fieldInfo.LinkCommand
    };
  }
}
