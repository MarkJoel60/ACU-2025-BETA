// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ScreenUntyped
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Api.Models;
using PX.Common;
using System.Web.Services;
using System.Xml.Serialization;

#nullable disable
namespace PX.Api.Soap.Screen;

[WebService(Namespace = "http://www.acumatica.com/untyped/", Name = "Screen")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ScreenUntyped : ScreenGate
{
  [WebMethod(true)]
  public void Clear(string screenID)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    this.Clear();
  }

  [WebMethod(true)]
  public ProcessResult GetProcessStatus(string screenID)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    return this.GetProcessStatus();
  }

  [WebMethod]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (Answer))]
  public override Command[] GetScenario(string scenario) => base.GetScenario(scenario);

  [WebMethod(true)]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  public Content GetSchema(string screenID)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    return this.GetSchema(PXContext.Session.SchemaMode.HasValue ? (SchemaMode) PXContext.Session.SchemaMode.Value : SchemaMode.Basic);
  }

  [WebMethod(true)]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Value))]
  [XmlInclude(typeof (Attachment))]
  public void SetSchema(string screenID, Content schema)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    this.SetSchema(schema);
  }

  [WebMethod(true)]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Value))]
  [XmlInclude(typeof (Attachment))]
  public string[][] Export(
    string screenID,
    Command[] commands,
    Filter[] filters,
    int topCount,
    bool includeHeaders,
    bool breakOnError)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    return this.Export(commands, filters, topCount, includeHeaders, breakOnError);
  }

  [WebMethod(true)]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Value))]
  [XmlInclude(typeof (Attachment))]
  public ImportResult[] Import(
    string screenID,
    Command[] commands,
    Filter[] filters,
    string[][] data,
    bool includedHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    return this.Import(commands, filters, data, includedHeaders, breakOnError, breakOnIncorrectTarget);
  }

  [WebMethod(true)]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Value))]
  [XmlInclude(typeof (Attachment))]
  public Content[] Submit(string screenID, Command[] commands)
  {
    PXContext.SetScreenID(Mask.Format(">CC.CC.CC.CC", screenID));
    return this.Submit(commands);
  }
}
