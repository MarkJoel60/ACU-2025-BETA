// Decompiled with JetBrains decompiler
// Type: PX.Data.SQLTree.MergedItemWrapper
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

#nullable disable
namespace PX.Data.SQLTree;

internal class MergedItemWrapper : PXResult
{
  private System.Type _dacType;

  public MergedItemWrapper(object dataItem, System.Type dacType)
    : this(dataItem, (object) null, dacType)
  {
  }

  public MergedItemWrapper(object dataItem, object dac)
    : this(dataItem, dac, dac.GetType())
  {
  }

  private MergedItemWrapper(object dataItem, object dac, System.Type dacType)
  {
    this._dacType = dacType;
    this.DataItem = dataItem;
    this.Dac = dac;
    this.Items = new object[1]{ dac };
  }

  public object DataItem { get; set; }

  public object Dac { get; set; }

  public override object this[System.Type t] => !(t == this._dacType) ? (object) null : this.Dac;

  public override object this[string s] => !(s == this._dacType?.Name) ? (object) null : this.Dac;

  public override System.Type GetItemType(int i) => i != 0 ? (System.Type) null : this._dacType;

  public override System.Type GetItemType(string s)
  {
    return !(s == this._dacType?.Name) ? (System.Type) null : this._dacType;
  }
}
