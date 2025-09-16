// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.InfoMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public abstract class InfoMessage
{
  protected PXErrorLevel _ErrorLevel;
  protected string _Message;

  public InfoMessage(PXErrorLevel aLevel, string aMessage)
  {
    this._ErrorLevel = aLevel;
    this._Message = aMessage;
  }

  public virtual PXErrorLevel ErrorLevel
  {
    get => this._ErrorLevel;
    set => this._ErrorLevel = value;
  }

  public virtual string Message
  {
    get => this._Message;
    set => this._Message = value;
  }
}
