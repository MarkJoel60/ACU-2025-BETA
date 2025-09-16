// Decompiled with JetBrains decompiler
// Type: PX.Common.PXExecutionContext
// Assembly: PX.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 35AC74DC-4190-4222-F56C-8CF32A9F1C93
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Common.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Common.xml

using PX.Common.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;

#nullable disable
namespace PX.Common;

public sealed class PXExecutionContext
{
  private PXExecutionContext.RequestInfo \u0002;
  private PXTimeInfo \u000E;
  private IDictionary<string, object> \u0006;

  public PXExecutionContext()
  {
  }

  public PXExecutionContext(PXExecutionContext source)
  {
    this.Request = new PXExecutionContext.RequestInfo(source.Request);
    this.Time = new PXTimeInfo(source.Time);
    foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) source.Bag)
      this.Bag.Add(keyValuePair);
  }

  public static PXExecutionContext Current
  {
    get => PXExecutionContext.Get(SlotStore.Instance, HttpContext.Current);
    set => PXExecutionContext.\u0002(SlotStore.Instance, value);
  }

  internal static PXExecutionContext Get(ISlotStore _param0, HttpContext _param1)
  {
    PXExecutionContext executionContext1 = _param0.Get<PXExecutionContext>("PXExecutionContext_KEY");
    if (executionContext1 != null)
      return executionContext1;
    PXExecutionContext executionContext2 = new PXExecutionContext();
    PXExecutionContext.RequestInfo requestInfo = (PXExecutionContext.RequestInfo) null;
    try
    {
      if (_param1 != null)
      {
        HttpRequest request = _param1.Request;
        requestInfo = new PXExecutionContext.RequestInfo(request.ApplicationPath, request.Url.Authority, request.Url.Scheme);
      }
    }
    catch (StackOverflowException ex)
    {
    }
    catch (OutOfMemoryException ex)
    {
    }
    catch
    {
    }
    if (requestInfo != null)
      executionContext2.Request = requestInfo;
    PXTimeZoneInfo timeZone = LocaleInfo.GetTimeZone();
    if (timeZone != null)
      executionContext2.Time = new PXTimeInfo(DateTime.UtcNow, timeZone);
    PXExecutionContext.\u0002(_param0, executionContext2);
    return executionContext2;
  }

  private static void \u0002(ISlotStore _param0, PXExecutionContext _param1)
  {
    _param0.Set("PXExecutionContext_KEY", (object) _param1);
  }

  public void Restore(PXExecutionContext source)
  {
    if (source == null)
      return;
    this.Request = new PXExecutionContext.RequestInfo(source.Request);
    this.Time = new PXTimeInfo(source.Time);
    LocaleInfo.SetTimeZone(this.Time.Info);
    this.\u0006 = source.\u0006;
  }

  public PXExecutionContext.RequestInfo Request
  {
    get => this.\u0002 ?? PXExecutionContext.RequestInfo.Empty;
    set => this.\u0002 = value;
  }

  public PXTimeInfo Time
  {
    get => this.\u000E ?? PXTimeInfo.Empty;
    set => this.\u000E = value;
  }

  public IDictionary<string, object> Bag
  {
    get
    {
      return this.\u0006 ?? (this.\u0006 = (IDictionary<string, object>) new Dictionary<string, object>());
    }
  }

  public byte[] ToBytes()
  {
    PXExecutionContext.RequestInfo request = this.Request;
    if (request == PXExecutionContext.RequestInfo.Empty)
      return new byte[0];
    StringBuilder output = new StringBuilder();
    using (XmlWriter xmlWriter = XmlWriter.Create(output))
    {
      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("context");
      xmlWriter.WriteStartElement("request");
      xmlWriter.WriteAttributeString("path", request.ApplicationPath);
      xmlWriter.WriteAttributeString("authority", request.Authority);
      xmlWriter.WriteAttributeString("scheme", request.Scheme);
      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
    }
    return Encoding.UTF8.GetBytes(output.ToString());
  }

  public static PXExecutionContext FromBytes(byte[] data)
  {
    if (data == null)
      throw new ArgumentNullException(nameof (data));
    string applicationPath = (string) null;
    string authority = (string) null;
    string scheme = (string) null;
    using (StringReader stringReader = new StringReader(Encoding.UTF8.GetString(data)))
    {
      FlexibleXmlReader flexibleXmlReader = FlexibleXmlReader.Create((TextReader) stringReader, false);
      bool flag1 = false;
      bool flag2 = false;
      while (flexibleXmlReader.Read())
      {
        switch (flexibleXmlReader.NodeType)
        {
          case XmlNodeType.Element:
            switch (flexibleXmlReader.Name.ToLower())
            {
              case "context":
                flag1 = true;
                continue;
              case "request":
                if (flag1)
                {
                  flag2 = true;
                  continue;
                }
                continue;
              default:
                continue;
            }
          case XmlNodeType.Attribute:
            if (flag2)
            {
              switch (flexibleXmlReader.Name)
              {
                case "path":
                  applicationPath = flexibleXmlReader.Value;
                  continue;
                case "authority":
                  authority = flexibleXmlReader.Value;
                  continue;
                case "scheme":
                  scheme = flexibleXmlReader.Value;
                  continue;
                default:
                  continue;
              }
            }
            else
              continue;
          case XmlNodeType.EndElement:
            switch (flexibleXmlReader.Name.ToLower())
            {
              case "context":
                flag1 = false;
                flag2 = false;
                continue;
              case "request":
                if (flag1)
                {
                  flag2 = false;
                  continue;
                }
                continue;
              default:
                continue;
            }
          default:
            continue;
        }
      }
    }
    PXExecutionContext.RequestInfo requestInfo = new PXExecutionContext.RequestInfo(applicationPath, authority, scheme);
    return new PXExecutionContext()
    {
      Request = requestInfo
    };
  }

  public sealed class RequestInfo
  {
    private readonly string \u0002;
    private readonly string \u000E;
    private readonly string \u0006;
    public static readonly PXExecutionContext.RequestInfo Empty = new PXExecutionContext.RequestInfo((string) null, (string) null, (string) null);

    public RequestInfo(PXExecutionContext.RequestInfo source)
      : this(source.With<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u000E ?? (PXExecutionContext.RequestInfo.\u0002.\u000E = new Func<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u0002.\u0002))), source.With<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u0006 ?? (PXExecutionContext.RequestInfo.\u0002.\u0006 = new Func<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u0002.\u000E))), source.With<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u0008 ?? (PXExecutionContext.RequestInfo.\u0002.\u0008 = new Func<PXExecutionContext.RequestInfo, string>(PXExecutionContext.RequestInfo.\u0002.\u0002.\u0006))))
    {
    }

    public RequestInfo(HttpRequest req)
      : this(req.ApplicationPath, req.GetWebsiteAuthority().Authority, req.GetWebsiteAuthority().Scheme)
    {
    }

    public RequestInfo(string applicationPath, string authority, string scheme)
    {
      this.\u0002 = applicationPath;
      this.\u0006 = authority;
      this.\u000E = scheme;
    }

    public string ApplicationPath => this.\u0002;

    public string Scheme => this.\u000E;

    public string Authority => this.\u0006;

    public bool Equals(PXExecutionContext.RequestInfo other)
    {
      if (other == null)
        return false;
      if (this == other)
        return true;
      return object.Equals((object) other.\u0002, (object) this.\u0002) && object.Equals((object) other.\u000E, (object) this.\u000E) && object.Equals((object) other.\u0006, (object) this.\u0006);
    }

    public override bool Equals(object obj)
    {
      if (obj == null)
        return false;
      if (this == obj)
        return true;
      return !(obj.GetType() != typeof (PXExecutionContext.RequestInfo)) && this.Equals((PXExecutionContext.RequestInfo) obj);
    }

    public override int GetHashCode()
    {
      return ((this.\u0002 != null ? this.\u0002.GetHashCode() : 0) * 397 ^ (this.\u000E != null ? this.\u000E.GetHashCode() : 0)) * 397 ^ (this.\u0006 != null ? this.\u0006.GetHashCode() : 0);
    }

    [Serializable]
    private sealed class \u0002
    {
      public static readonly PXExecutionContext.RequestInfo.\u0002 \u0002 = new PXExecutionContext.RequestInfo.\u0002();
      public static Func<PXExecutionContext.RequestInfo, string> \u000E;
      public static Func<PXExecutionContext.RequestInfo, string> \u0006;
      public static Func<PXExecutionContext.RequestInfo, string> \u0008;

      internal string \u0002(PXExecutionContext.RequestInfo _param1) => _param1.\u0002;

      internal string \u000E(PXExecutionContext.RequestInfo _param1) => _param1.\u0006;

      internal string \u0006(PXExecutionContext.RequestInfo _param1) => _param1.\u000E;
    }
  }

  public sealed class Scope : IDisposable
  {
    private readonly PXExecutionContext \u0002;
    private readonly PXExecutionContext \u000E;
    private bool \u0006;

    private Scope(PXExecutionContext _param1)
    {
      this.\u0002 = _param1 != null ? new PXExecutionContext(_param1) : throw new ArgumentNullException("cntx");
      this.\u000E = new PXExecutionContext(PXExecutionContext.Current);
    }

    public static PXExecutionContext.Scope Instantiate(PXExecutionContext cntx)
    {
      PXExecutionContext.Scope scope = new PXExecutionContext.Scope(cntx);
      scope.\u0002();
      return scope;
    }

    public void Dispose()
    {
      if (!this.\u0006)
        PXExecutionContext.Current.Restore(this.\u000E);
      this.\u0006 = true;
    }

    private void \u0002() => PXExecutionContext.Current.Restore(this.\u0002);
  }
}
