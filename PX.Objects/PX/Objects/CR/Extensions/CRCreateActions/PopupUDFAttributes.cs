// Decompiled with JetBrains decompiler
// Type: PX.Objects.CR.Extensions.CRCreateActions.PopupUDFAttributes
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Data.BQL;
using PX.Data.MassProcess;
using System;

#nullable enable
namespace PX.Objects.CR.Extensions.CRCreateActions;

/// <exclude />
[PXHidden]
[PXBreakInheritance]
[Serializable]
public class PopupUDFAttributes : FieldValue
{
  [PXString]
  public virtual 
  #nullable disable
  string CacheName { get; set; }

  [PXString(IsKey = true)]
  public virtual string ScreenID { get; set; }

  public abstract class cacheName : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PopupUDFAttributes.cacheName>
  {
  }

  public abstract class screenID : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PopupUDFAttributes.screenID>
  {
  }

  public abstract class displayName : 
    BqlType<
    #nullable enable
    IBqlString, string>.Field<
    #nullable disable
    PopupUDFAttributes.displayName>
  {
  }

  public abstract class value : BqlType<
  #nullable enable
  IBqlString, string>.Field<
  #nullable disable
  PopupUDFAttributes.value>
  {
  }
}
