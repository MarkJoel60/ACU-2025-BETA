// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.FieldLookup
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using System;

#nullable disable
namespace PX.Objects.CS;

public class FieldLookup
{
  protected Type _type;
  protected object _value;

  public Type Field => this._type;

  public object Value => this._value;

  protected FieldLookup(Type type, object value)
  {
    this._type = type;
    this._value = value;
  }
}
