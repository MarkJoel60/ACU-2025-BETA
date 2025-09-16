// Decompiled with JetBrains decompiler
// Type: PX.Objects.EP.PXExportHandlerEml
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Common;
using PX.Common.Mail;
using PX.Common.MIME;
using PX.Data;
using PX.Export;
using System;
using System.IO;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Objects.EP;

public sealed class PXExportHandlerEml : PXFileExportHandler
{
  public static readonly string _FILENAME_PARAM_NAME = $"_{typeof (PXExportHandlerEml).Name}_FileName";
  public static readonly string _KEY = $"_{typeof (PXExportHandlerEml).Name}_";

  protected virtual string DataSessionKey => PXExportHandlerEml._KEY;

  protected virtual string ContentType => "message/rfc822";

  protected virtual void Write(Stream stream, PXFileExportHandler.ProcessBag bag)
  {
    ((Entity) (bag.Data as Email).With<Email, Message>((Func<Email, Message>) (_ => _.Message))).ToStream(stream);
  }

  protected virtual string GetFileName(PXFileExportHandler.ProcessBag bag)
  {
    string str = (bag.Data as Email).With<Email, Message>((Func<Email, Message>) (_ => _.Message)).With<Message, string>((Func<Message, string>) (_ => _.Subject));
    return (!string.IsNullOrEmpty(str) ? str : base.GetFileName(bag)) + ".eml";
  }

  public static PXExportRedirectException GenerateException(Email mail)
  {
    return (PXExportRedirectException) new PXExportHandlerEml.ExportRedirectException((object) mail);
  }

  private class ExportRedirectException : PXExportRedirectException
  {
    public ExportRedirectException(object data)
      : base("axd", "ExportEmail", PXExportHandlerEml._KEY, data)
    {
    }

    public virtual string Url => $"{((Exception) this).Message}.{this.Extension}";

    public ExportRedirectException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
