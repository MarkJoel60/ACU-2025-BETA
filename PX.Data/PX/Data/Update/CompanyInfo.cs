// Decompiled with JetBrains decompiler
// Type: PX.Data.Update.CompanyInfo
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.BulkInsert.Installer.DatabaseSetup;
using System;

#nullable disable
namespace PX.Data.Update;

public class CompanyInfo : IComparable, IComparable<CompanyInfo>
{
  public const int RootCompany = -1;

  public int CompanyID { get; internal set; }

  public int ParentID { get; internal set; }

  public bool Joined { get; internal set; }

  public string LoginName { get; internal set; }

  public DataTypeInfo DataType { get; internal set; }

  public int? Sequence { get; internal set; }

  public long? Size { get; internal set; }

  public bool Exist { get; internal set; }

  public bool Hidden { get; internal set; }

  public bool UpdateableTemplate { get; internal set; }

  public string CompanyCD { get; internal set; }

  private DataTypeInfo OrigDataType { get; set; }

  public DataTypeInfo CurrentDataType => this.DataType != null ? this.DataType : this.OrigDataType;

  public bool System => this.DataType != null ? this.DataType.Hidden : this.OrigDataType.Hidden;

  public bool Installing
  {
    get => this.DataType != null && !this.DataType.Empty && (this.DataType.Execution & 1) == 1;
  }

  public bool Updating => (this.CurrentDataType.Execution & 2) == 2;

  public bool PendingData => !string.IsNullOrEmpty(this.PendingDataName);

  public string PendingDataName
  {
    get
    {
      return this.DataType == null || this.DataType.Empty || this.DataType.Hidden ? (string) null : this.DataType.Name;
    }
  }

  public CompanyInfo() => this.Initialise();

  public CompanyInfo(DataTypeInfo dataType, int companyID)
  {
    this.Initialise();
    this.CompanyID = companyID;
    this.CompanyCD = companyID.ToString();
    this.OrigDataType = dataType;
  }

  private void Initialise()
  {
    this.CompanyID = 1;
    this.ParentID = -1;
  }

  public CompanyInfo Clone() => (CompanyInfo) this.MemberwiseClone();

  public override bool Equals(object obj) => this.CompareTo(obj) == 0;

  public int CompareTo(object ob)
  {
    return ob.GetType() != this.GetType() ? -1 : this.CompareTo((CompanyInfo) ob);
  }

  public int CompareTo(CompanyInfo ob) => this.Compare(this, ob);

  public int Compare(CompanyInfo op1, CompanyInfo op2) => CompanyInfo.Compare_(op1, op2);

  private static int Compare_(CompanyInfo op1, CompanyInfo op2)
  {
    return op1.CompanyID == op2.CompanyID ? 0 : -1;
  }

  public override int GetHashCode() => this.CompanyID.GetHashCode();

  public override string ToString() => this.CompanyID.ToString();
}
