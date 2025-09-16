// Decompiled with JetBrains decompiler
// Type: PX.Data.Process.Automation.State.ScreenTable
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data.Automation.State;
using System.Web.Compilation;

#nullable disable
namespace PX.Data.Process.Automation.State;

internal sealed class ScreenTable
{
  private System.Type _type;

  public ScreenTable(string tableName)
  {
    this.Fields = new StateMap<ScreenTableField>();
    this.TableName = tableName;
  }

  public string TableName { get; }

  public System.Type CacheType
  {
    get
    {
      if (this._type == (System.Type) null)
        this._type = PXBuildManager.GetType(this.TableName, true, true);
      return this._type;
    }
  }

  public StateMap<ScreenTableField> Fields { get; }
}
