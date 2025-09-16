// Decompiled with JetBrains decompiler
// Type: PX.SM.Warden
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;

#nullable enable
namespace PX.SM;

public class Warden : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDBString(IsKey = true)]
  public virtual 
  #nullable disable
  string InstallationID { get; set; }

  [PXDBShort(IsKey = true)]
  [PXIntList(typeof (PXRequestInfoType), false)]
  public virtual short Type { get; set; }

  [PXDBString(IsKey = true)]
  public virtual string Key { get; set; }

  [PXDBString(IsKey = true)]
  public virtual string Sub { get; set; }

  [PXDBBool]
  public virtual bool Expired { get; set; }

  [PXDBDateAndTime(InputMask = "g", PreserveTime = true, UseTimeZone = false)]
  public virtual System.DateTime? LastRequest { get; set; }

  [PXDBString]
  public virtual string Details { get; set; }

  public abstract class installationID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Warden.installationID>
  {
  }

  public abstract class type : BqlType<
  #nullable enable
  IBqlShort, short>.Field<
  #nullable disable
  Warden.type>
  {
  }

  public abstract class key : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Warden.key>
  {
  }

  public abstract class sub : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Warden.sub>
  {
  }

  public abstract class expired : BqlType<
  #nullable enable
  IBqlBool, bool>.Field<
  #nullable disable
  Warden.expired>
  {
  }

  public abstract class lastRequest : BqlType<
  #nullable enable
  IBqlDateTime, System.DateTime>.Field<
  #nullable disable
  Warden.lastRequest>
  {
  }

  public abstract class details : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  Warden.details>
  {
  }
}
