// Decompiled with JetBrains decompiler
// Type: PX.Data.Automation.ExposedActionInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.Automation;

public class ExposedActionInfo : ActionInfo
{
  private string _connotation;
  private bool? _isTopLevel;

  public string Connotation
  {
    get => this.State?.Connotation ?? this._connotation;
    set => this._connotation = value;
  }

  public ActionState State { get; internal set; }

  public bool? IsTopLevel
  {
    get
    {
      ActionState state = this.State;
      return new bool?(state != null ? state.IsTopLevel : this._isTopLevel.GetValueOrDefault());
    }
    set => this._isTopLevel = value;
  }
}
