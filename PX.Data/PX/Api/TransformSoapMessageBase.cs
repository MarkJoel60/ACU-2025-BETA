// Decompiled with JetBrains decompiler
// Type: PX.Api.TransformSoapMessageBase
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using System;
using System.IO;
using System.Text;
using System.Web.Services.Protocols;
using System.Xml;

#nullable disable
namespace PX.Api;

public class TransformSoapMessageBase : SoapExtension
{
  private Stream _oldStream;
  private Stream _newStream;

  public override object GetInitializer(Type serviceType) => (object) null;

  public override object GetInitializer(
    LogicalMethodInfo methodInfo,
    SoapExtensionAttribute attribute)
  {
    return (object) null;
  }

  public override void Initialize(object initializer)
  {
  }

  public override void ProcessMessage(SoapMessage message)
  {
    switch (message.Stage)
    {
      case SoapMessageStage.AfterSerialize:
        this._newStream.Position = 0L;
        XmlDocument document1 = new XmlDocument()
        {
          XmlResolver = (XmlResolver) null
        };
        document1.XmlResolver = (XmlResolver) null;
        document1.Load(this._newStream);
        this.TransformResultMessage(document1);
        document1.PreserveWhitespace = true;
        XmlTextWriter w = new XmlTextWriter(this._oldStream, (Encoding) new UTF8Encoding(false));
        document1.Save((XmlWriter) w);
        w.Flush();
        this._oldStream.Flush();
        break;
      case SoapMessageStage.BeforeDeserialize:
        XmlDocument document2 = new XmlDocument()
        {
          XmlResolver = (XmlResolver) null
        };
        document2.XmlResolver = (XmlResolver) null;
        document2.Load(this._oldStream);
        this.TransformInputMessage(document2);
        document2.Save(this._newStream);
        this._newStream.Position = 0L;
        break;
    }
  }

  protected virtual void TransformResultMessage(XmlDocument document)
  {
  }

  protected virtual void TransformInputMessage(XmlDocument document)
  {
  }

  public override Stream ChainStream(Stream stream)
  {
    this._oldStream = stream;
    return this._newStream = (Stream) new MemoryStream();
  }
}
