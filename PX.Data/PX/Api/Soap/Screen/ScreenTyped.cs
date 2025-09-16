// Decompiled with JetBrains decompiler
// Type: PX.Api.Soap.Screen.ScreenTyped
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

[WebService(Namespace = "http://www.acumatica.com/typed/", Name = "Screen")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ScreenTyped : ScreenGate
{
  [WebMethod(true)]
  public override void Clear() => base.Clear();

  [WebMethod(true)]
  public override ProcessResult GetProcessStatus() => base.GetProcessStatus();

  [WebMethod]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Container))]
  public override Command[] GetScenario(string scenario) => base.GetScenario(scenario);

  [WebMethod(true)]
  [TransformMethod]
  [XmlInclude(typeof (PX.Api.Models.Field))]
  [XmlInclude(typeof (Action))]
  [XmlInclude(typeof (Key))]
  [XmlInclude(typeof (Parameter))]
  [XmlInclude(typeof (NewRow))]
  [XmlInclude(typeof (DeleteRow))]
  [XmlInclude(typeof (RowNumber))]
  [XmlInclude(typeof (EveryValue))]
  [XmlInclude(typeof (Answer))]
  [XmlInclude(typeof (Container))]
  public Content GetSchema()
  {
    return this.GetSchema(PXContext.Session.SchemaMode.HasValue ? (SchemaMode) PXContext.Session.SchemaMode.Value : SchemaMode.Basic);
  }

  [WebMethod(true)]
  [TransformMethodCall]
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
  [XmlInclude(typeof (Container))]
  public override void SetSchema(Content schema) => base.SetSchema(schema);

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
  [XmlInclude(typeof (Container))]
  public override string[][] Export(
    Command[] commands,
    Filter[] filters,
    int topCount,
    bool includeHeaders,
    bool breakOnError)
  {
    return base.Export(commands, filters, topCount, includeHeaders, breakOnError);
  }

  [WebMethod(true)]
  [TransformMethod]
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
  [XmlInclude(typeof (Container))]
  public override ImportResult[] Import(
    Command[] commands,
    Filter[] filters,
    string[][] data,
    bool includedHeaders,
    bool breakOnError,
    bool breakOnIncorrectTarget)
  {
    return base.Import(commands, filters, data, includedHeaders, breakOnError, breakOnIncorrectTarget);
  }

  [WebMethod(true)]
  [TransformMethod]
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
  [XmlInclude(typeof (Container))]
  public override Content[] Submit(Command[] commands) => base.Submit(commands);
}
