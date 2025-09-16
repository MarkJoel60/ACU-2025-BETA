// Decompiled with JetBrains decompiler
// Type: PX.Data.PXSetupNotEnteredException
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Common;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

#nullable disable
namespace PX.Data;

/// <summary>An exception type that is used to indicate that the required data is not entered on the setup form.</summary>
[Serializable]
public class PXSetupNotEnteredException : PXSetPropertyException
{
  private System.Type _DAC;
  private string _navigateTo;
  private Dictionary<System.Type, object> _keyParams;

  public PXSetupNotEnteredException(string format, System.Type inpDAC, params object[] args)
    : base(format, args)
  {
    this._keyParams = (Dictionary<System.Type, object>) null;
    this._DAC = inpDAC;
    this._navigateTo = args[0].ToString();
  }

  public PXSetupNotEnteredException(
    string format,
    System.Type inpDAC,
    Dictionary<System.Type, object> keyparams,
    params object[] args)
    : base(format, args)
  {
    this._keyParams = keyparams;
    this._DAC = inpDAC;
    this._navigateTo = args[0].ToString();
  }

  public Dictionary<System.Type, object> KeyParams => this._keyParams;

  public System.Type DAC => this._DAC;

  public string NavigateTo => this._navigateTo;

  public PXSetupNotEnteredException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
    ReflectionSerializer.RestoreObjectProps<PXSetupNotEnteredException>(this, info);
  }

  /// <exclude />
  public override void GetObjectData(SerializationInfo info, StreamingContext context)
  {
    ReflectionSerializer.GetObjectData<PXSetupNotEnteredException>(this, info);
    base.GetObjectData(info, context);
  }
}
