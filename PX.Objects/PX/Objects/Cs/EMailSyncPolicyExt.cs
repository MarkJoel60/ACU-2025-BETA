// Decompiled with JetBrains decompiler
// Type: PX.Objects.CS.EMailSyncPolicyExt
// Assembly: PX.Objects, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF76B6BF-0C8A-413D-8225-C21BEAE6CEEC
// Assembly location: D:\tmp\2025 R2 DLLs\PX.Objects.dll
// XML documentation location: D:\tmp\2025 R2 DLLs\PX.Objects.xml

using PX.Data;
using PX.Objects.CR;
using PX.SM;

#nullable disable
namespace PX.Objects.CS;

public class EMailSyncPolicyExt : PXCacheExtension<EMailSyncPolicy>
{
  [PXDBString(10, IsUnicode = true)]
  [PXUIField(DisplayName = "Contact Class")]
  [PXSelector(typeof (CRContactClass.classID), DescriptionField = typeof (CRContactClass.description), CacheGlobal = true)]
  public virtual string ContactsClass { get; set; }
}
