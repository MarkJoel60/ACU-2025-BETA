// Decompiled with JetBrains decompiler
// Type: PX.Api.SYSubstitutionValues
// Assembly: PX.Data, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 2A82D21C-DAFC-4371-ACE9-BAD417AC5A62
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Data.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Data.xml

using PX.Data;
using PX.Data.BQL;
using System;

#nullable enable
namespace PX.Api;

[Serializable]
public class SYSubstitutionValues : PXBqlTable, IBqlTable, IBqlTableSystemDataStorage
{
  [PXDefault(typeof (SYSubstitution.substitutionID))]
  [PXParent(typeof (Select<SYSubstitution, Where<SYSubstitution.substitutionID, Equal<Current<SYSubstitutionValues.substitutionID>>>>))]
  [PXDBString(25, IsKey = true, IsUnicode = true, InputMask = "")]
  public 
  #nullable disable
  string SubstitutionID { get; set; }

  [PXDBLongIdentity(IsKey = true)]
  public long? ValueID { get; set; }

  [PXDefault]
  [PXDBString(150, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Original Value")]
  public string OriginalValue { get; set; }

  [PXDefault]
  [PXDBString(150, IsUnicode = true, InputMask = "")]
  [PXUIField(DisplayName = "Substitution Value")]
  public string SubstitutedValue { get; set; }

  [PXDBTimestamp]
  public virtual byte[] TStamp { get; set; }

  public abstract class substitutionID : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionValues.substitutionID>
  {
  }

  public abstract class valueID : BqlType<
  #nullable enable
  IBqlLong, long>.Field<
  #nullable disable
  SYSubstitutionValues.valueID>
  {
  }

  public abstract class originalValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionValues.originalValue>
  {
  }

  public abstract class substitutedValue : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    SYSubstitutionValues.substitutedValue>
  {
  }

  public abstract class tStamp : BqlType<
  #nullable enable
  IBqlByteArray, byte[]>.Field<
  #nullable disable
  SYSubstitutionValues.tStamp>
  {
  }
}
