// Decompiled with JetBrains decompiler
// Type: PX.SM.Licensing
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.SM;

[Serializable]
public class Licensing : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXUIField(DisplayName = "Installation ID", Enabled = false)]
  [PXDBString(64 /*0x40*/)]
  public virtual 
  #nullable disable
  string InstallationID { get; set; }

  [PXUIField(DisplayName = "Licensing Key")]
  [PXDBString(64 /*0x40*/)]
  public virtual string LicensingKey { get; set; }

  [PXDBString]
  public virtual string Signature { get; set; }

  [PXDBString]
  public virtual string Restriction { get; set; }

  [PXUIField(DisplayName = "Date", Enabled = false)]
  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  public virtual System.DateTime? Date { get; set; }

  [PXDBDate(InputMask = "g", PreserveTime = true, UseTimeZone = true)]
  public virtual System.DateTime? Activity { get; set; }

  [PXDBString(IsUnicode = true)]
  public virtual string Status { get; set; }

  public abstract class installationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Licensing.installationID>
  {
  }

  public abstract class licensingKey : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Licensing.licensingKey>
  {
  }

  public abstract class signature : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Licensing.signature>
  {
  }

  public abstract class restriction : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Licensing.restriction>
  {
  }

  public abstract class date : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Licensing.date>
  {
  }

  public abstract class activity : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Licensing.activity>
  {
  }

  public abstract class status : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Licensing.status>
  {
  }
}
