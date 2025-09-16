// Decompiled with JetBrains decompiler
// Type: PX.Objects.CA.CAMessage
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;

#nullable disable
namespace PX.Objects.CA;

public class CAMessage : InfoMessage
{
  protected long _Key;

  public CAMessage(long aKey, PXErrorLevel aLevel, string aMessage)
    : base(aLevel, aMessage)
  {
    this._Key = aKey;
  }

  public virtual long Key
  {
    get => this._Key;
    set => this._Key = value;
  }
}
